using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using Peyton.Core.Common;
using Peyton.Core.Repository;

namespace Peyton.Core.Messages
{
    public class ServiceResponse : ServiceMessage, IServiceResponse
    {
        #region Constructors
        public ServiceResponse() { }

        public ServiceResponse(ServiceRequest request) : this()
        {
            RequestId = request.TransactionId;
            RequestTime = request.RequestTime;
            AddErrors(request.Validate());
        }

        #endregion

        #region Properties

        public string ServiceCode { get; set; }
        public Guid RequestId { get; set; }
        public DateTime RequestTime { get; set; }
        public TimeSpan ResponseTime { get; set; }
        public List<ValidationModel> Errors { get; set; }

        private string _message;
        public bool HasError { get { return Errors.Any(); } }
        public string Message
        {
            get
            {
                if (HasError)
                    return Errors.Select(i => i.Message).Combine("<br />");
                return _message;
            }
            set
            {
                _message = value;
            }
        }
        public ServiceResult Result { get; set; }
        #endregion

        #region Methods
        public void AddError(string message, string member)
        {
            member = StringExt.Get(member);
            if(!string.IsNullOrWhiteSpace(message))
                Errors.Add(message, member);
        }

        public void AddErrors(List<ValidationResult> errors)
        {
            Errors.AddRange(errors);
        }

        public static implicit operator JsonResult(ServiceResponse response)
        {
            response._message = response._message.HtmlResolve();
            return new JsonResult { Data = response, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public override void Init()
        {
            base.Init();
            _message = string.Empty;
            Errors = new List<ValidationModel>();
        }

        public static T Factory<T, TH>(TH request, Func<DbContext, T, TH, ServiceResult> myFunc, string serviceCode = "")
            where T : ServiceResponse
            where TH : ServiceRequest
        {
            var response = (T)Activator.CreateInstance(typeof(T), request);
            try
            {
                if (!response.HasError)
                    using (var ctx = new DbContext())
                    {
                        response.Result = myFunc(ctx, response, request);
                    }
            }
            catch (Exception ex)
            {
                response.AddError(ex.Message, "Unknown");
            }
            response.ServiceCode = serviceCode;
            response.ResponseTime = new TimeSpan();
            return response;
        }

        #endregion
    }
}
