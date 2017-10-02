using System;
using System.Linq;
using System.Text;
using System.Threading;
using eWAY.Rapid;
using eWAY.Rapid.Enums;
using eWAY.Rapid.Models;
using Peyton.Core;
using Peyton.Core.Common;
using System.Collections.Generic;
using SCF.ServiceAdaptors.Payment.Messages;
using Peyton.Core.Web;

namespace SCF.ServiceAdaptors.Payment
{
    public class eWayAdaptor : IPaymentService
    {
        private string _apiKey { get; set; } 
        private string _password { get; set; } 
        private string _rapidEndpoint = "https://api.sandbox.ewaypayments.com/";

        public int Attemps { get; set; } 
        public int Delay { get; set; }

        public eWayAdaptor()
        {
            Attemps = 3;
            Delay = 5000;
            _apiKey =  SettingManager.Get<string>("eWay");
            _password = SettingManager.Get<string>("eWayPassword");
        }

        private Transaction ProcessPayRequest(PaymentRequest request)
        {
            var transaction = new Transaction();
            transaction.TransactionType = TransactionTypes.Purchase;

            transaction.Customer = new Customer();
            transaction.Customer.CardDetails = new CardDetails();
            transaction.Customer.CardDetails.Name = request.Card.HolderName;
            transaction.Customer.CardDetails.Number = request.Card.CardNumber;
            transaction.Customer.CardDetails.ExpiryMonth = request.Card.ExpiryMonth.ToString("00");
            transaction.Customer.CardDetails.ExpiryYear = request.Card.ExpiryYear.ToString("00");
            transaction.Customer.CardDetails.CVN = request.Card.CCV.ToString("000");

            transaction.PaymentDetails = new PaymentDetails();
            transaction.PaymentDetails.TotalAmount = (int)(request.Amount * 100);
            transaction.PaymentDetails.CurrencyCode = "AUD";
            transaction.PaymentDetails.InvoiceNumber = request.InvoiceId;
            transaction.PaymentDetails.InvoiceReference = request.TransactionID.ToString();
            transaction.PaymentDetails.InvoiceDescription = request.Comment;
            return transaction;
        }

        private PaymentResponse ProcessPayResponse(CreateTransactionResponse parResponse)
        {
            var response = new PaymentResponse();
            if (parResponse.TransactionStatus.Status.Value)
                response.Result = ServiceResult.Success;
            else
            {
                var errorCodes = new string[] { "S5000", "S5085", "S5086", "S5087", "S5099", "F9023", "F7000", "D4403", "D4406", "D4459", "D4496", "S9996", "S9902", "S9992" };
                response.Result = ServiceResult.Error;
                var codes = parResponse.TransactionStatus.ProcessingDetails.ResponseMessage.Split(new[] { ", " }, StringSplitOptions.None);
                foreach (var code in codes)
                {
                    if (errorCodes.Contains(code))
                        response.Result = ServiceResult.Error;
                    response.Errors.Add(RapidClientFactory.UserDisplayMessage(code, "EN"));
                }
            }
            return response;
        }

        public PaymentResponse Pay(PaymentRequest request)
        {
            using (var context = new Context())
            {
                var response = new PaymentResponse();
                var adaptorLog = new ServiceLog(request);
                adaptorLog.ServiceCode = "eWay";
                adaptorLog.Name = "eWay";
                adaptorLog.Type = ServiceLogType.Adaptor;
                
                adaptorLog.Delay = Delay;
                adaptorLog.Location = request.Location;

                var ewayClient = RapidClientFactory.NewRapidClient(_apiKey, _password, _rapidEndpoint);
                var transaction = ProcessPayRequest(request);

                var n = 1;

                adaptorLog.ResponseTime.Start = DateTime.Now;
                var times = new List<DateTime>();
                while (n <= Attemps)
                {
                    var log = new ServiceLog(request);
                    log.ServiceCode = "eWay";
                    log.Name = "eWay";
                    log.Type = ServiceLogType.CloudService;
                    log.Attemps = n;
                    adaptorLog.Attemps = n++;
                    log.ResponseTime.Start = DateTime.Now;
                    log.Location = request.Location;
                    try
                    {
                        times.Add(DateTime.Now);
                        log.ResponseTime.Start = DateTime.Now;
                        var result = ewayClient.Create(PaymentMethod.Direct, transaction);
                        log.ResponseTime.End = DateTime.Now;
                        adaptorLog.ResponseTime.End = DateTime.Now;
                        response = ProcessPayResponse(result);
                    }
                    catch (Exception ex)
                    {
                        log.ResponseTime.End = DateTime.Now;
                        adaptorLog.ResponseTime.End = DateTime.Now;
                        response.Result = ServiceResult.Error;
                        response.Errors.Add(ex.Message);
                    }

                    log.Message = response.Message;
                    log.Result = response.Result.ToString();
                    //context.Set<ServiceLog>().Add(log);

                    if (response.Result != ServiceResult.Error)
                        break;

                    if (n > Attemps)
                        break;

                    if (n == 2 && request.Amount2.HasValue)
                        transaction.PaymentDetails.TotalAmount = (int)(request.Amount2.Value * 100);
                    if (n == 3 && request.Amount3.HasValue)
                        transaction.PaymentDetails.TotalAmount = (int)(request.Amount3.Value * 100);
                    // let's wait 5 seconds to see if this is a temporary network issue.

                    Thread.Sleep(Delay); 
                }
                adaptorLog.ResponseTime.Start = times[0];
                adaptorLog.Result = response.Result.ToString();
                adaptorLog.Message = response.Message;
                context.Set<Log>().Add(adaptorLog);
                context.SaveChanges();

                return response;
            }
        }

        public Messages.RefundResponse Refund(RefundRequest request)
        {
            throw new NotImplementedException();
        }

        public AccountResponse Check(AccountRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
