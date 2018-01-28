using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using PayPal.Payments.Common.Utility;
using PayPal.Payments.DataObjects;
using PayPal.Payments.Transactions;
using Peyton.Core;
using Peyton.Core.Common;
using SCF.ServiceAdaptors.Payment.Messages;
using Peyton.Core.Repository;

namespace SCF.ServiceAdaptors.Payment
{
    public class PayPalAdaptor : IPaymentService
    {
        public string User = "youidev";
        public string Password = "ThaiAn2009";
        public string Partner ="PayPal";
        public string Vendor = "tranhongthai";
        public string Url = "pilot-payflowpro.paypal.com";
        public string Application = "Service Consumer Framework";
        public string Version = "1.0";
        public int Attemps = 3;
        public int Delay = 5000;
        public bool ChangeAmount1 { get; set; }
        public bool ChangeAmount2 { get; set; }

        public PayPalAdaptor()
        {
        }

        private SaleTransaction ProcessPayRequest(PaymentRequest request)
        {
            var invoice = new Invoice();
            invoice.Amt = new Currency(new decimal(request.Amount), request.Currency);
            invoice.CustRef = request.UserId.ToString();

            var cardDetails = new CreditCard(request.Card.CardNumber, request.Card.ExpiryDate);
            cardDetails.Cvv2 = request.Card.CCV.ToString("000");
            cardDetails.Name = request.Card.HolderName;

            var card = new CardTender(cardDetails); // credit card
            var userInfo = new UserInfo(User, Vendor, Partner, Password);
            var connection = new PayflowConnectionData(Url, 443, 45, "", 0, "", "");
            

            var transaction = new SaleTransaction(userInfo, connection, invoice, card, PayflowUtility.RequestId);
            transaction.ClientInfo = new ClientInfo {IntegrationProduct = Application, IntegrationVersion = Version};
            
            transaction.Verbosity = "LOW";
            

            return transaction;
        }
        private PaymentResponse ProcessPayResponse(Response parResponse)
        {
            var error = new StringBuilder();
            var response = new PaymentResponse();
            response.Result = ServiceResult.Invalid;
            response.ServiceCode = "PayPal";
            var result = parResponse.TransactionResponse;
            if (result == null)
            {
                response.Result = ServiceResult.Error;
                error.AppendLine("Unknown Error: Response is null");
                return response;
            }
            if (result.Duplicate == "1")
            {
                error.AppendLine("Duplicate Response: Duplicate Transaction");
                response.Result = ServiceResult.Invalid;
            }

            // Evaluate Result Code
            if (result.Result < 0)
            {
                // Transaction failed.
                error.AppendLine("System Error: There was an error processing your transaction. Please contact Customer Service.");
                error.AppendLine("Error: " + result.Result);
                response.Result = ServiceResult.Error;
            }
            else if (result.Result == 0)
            {
                response.Errors.Clear();
                response.Result = ServiceResult.Success;
            }
            else if (result.Result == 13)
            {
                response.Result = ServiceResult.Invalid;
                error.AppendLine("Your Transaction is pending. Contact Customer Service to complete your order.");
            }
            else if ((result.Result == 23 || result.Result == 24))
            {
                response.Result = ServiceResult.Invalid;
                error.AppendLine("Invalid credit card information. Please re-enter.");
            }
            else if (result.Result == 126)
            {

                if (result.AVSAddr != "Y" || result.AVSZip != "Y")
                {
                    response.Result = ServiceResult.Invalid;
                    error.AppendLine("Your billing information does not match.  Please re-enter.");
                }
                else
                {
                    response.Result = ServiceResult.Invalid;
                    error.AppendLine("Your Transaction is Under Review. We will notify you via e-mail if accepted.");
                }
            }
            else if (result.Result == 127)
            {
                response.Result = ServiceResult.Invalid;
                error.AppendLine("Your Transaction is Under Review. We will notify you via e-mail if accepted.");
            }
            else
            {
                // Error occurred, display normalized message returned.
                response.Result = ServiceResult.Error;
                error.AppendLine("Error Code: " + result.Result + " - Error Message: " +result.RespMsg);
            }
            
            // Get the Transaction Context and check for any contained SDK specific errors (optional code).
            // This is not normally used in production.
            var transCtx = parResponse.TransactionContext;
            if (transCtx != null && transCtx.getErrorCount() > 0)
            {
                response.Result = ServiceResult.Error;
                error.AppendLine("Transaction Context Errors: " + transCtx);
            }

            if (response.Result != ServiceResult.Success)
                response.Errors.Add(error.ToString());

            return response;
        }

        public PaymentResponse Pay(PaymentRequest request)
        {
            using (var context = new DbContext())
            {
                var response = new PaymentResponse();
                var adaptorLog = new ServiceLog(request);
                adaptorLog.Name = "PayPal";
                adaptorLog.ServiceCode = "PayPal";
                
                adaptorLog.Delay = Delay;
                adaptorLog.Type = ServiceLogType.Adaptor;
                adaptorLog.Location = request.Location;
                adaptorLog.ApplicationCode = request.ApplicationCode;

                var saleTransaction = ProcessPayRequest(request);
                
                var n = 1;
                adaptorLog.ResponseTime.Start = DateTime.Now;
                var times = new List<DateTime>();
                while (n <= Attemps)
                {
                    var log = new ServiceLog(request);
                    log.ServiceCode = "PayPal";
                    log.Name = "PayPal";
                    log.Type = ServiceLogType.CloudService;
                    log.Attemps = n;
                    adaptorLog.Attemps = n++;
                    log.ApplicationCode = request.ApplicationCode;
                    log.Location = request.Location;
                    try
                    {
                        log.ResponseTime.Start = DateTime.Now;
                        times.Add(DateTime.Now);
                        var result = saleTransaction.SubmitTransaction();
                        log.ResponseTime.End = DateTime.Now;
                        adaptorLog.ResponseTime.End = DateTime.Now;
                        if (result != null)
                            response = ProcessPayResponse(result);
                        else
                        {
                            response.Result = ServiceResult.Error;
                            response.Errors.Add("Connection Error: Cannot connect the service.");
                        }
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
                    {
                        request.Amount = request.Amount2.Value;
                        saleTransaction = ProcessPayRequest(request);
                    }

                    if (n == 3 && request.Amount3.HasValue)
                    {
                        request.Amount = request.Amount3.Value;
                        saleTransaction = ProcessPayRequest(request);
                    }

                    Thread.Sleep(Delay); // let's wait 5 seconds to see if this is a temporary network issue.
                }
                adaptorLog.ResponseTime.Start = times[0];
                adaptorLog.Result = response.Result.ToString();
                adaptorLog.Message = response.Message;
                context.Set<Log>().Add(adaptorLog);
                context.SaveChanges();

                return response;
            }
        }

        public RefundResponse Refund(RefundRequest request)
        {
            throw new NotImplementedException();
        }

        public AccountResponse Check(AccountRequest request)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
        }
    }
}
