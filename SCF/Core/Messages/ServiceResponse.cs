using Peyton.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web.Mvc;

namespace Peyton.Core.Messages
{
    [DataContract]
    public class ServiceResponse<T> : ServiceResponse
    {
        [DataMember]
        public T Data { get; set; }

        public ServiceResponse() { }

        public ServiceResponse<T> SetData(T data)
        {
            Data = HasError ? default(T) : data;
            return this;
        }

        public ServiceResponse<T> SetData(T successData, T failData)
        {
            Data = HasError ? failData : successData;
            return this;
        }

        public ServiceResponse<T> SetData(List<ValidationModel> errors, T data)
        {
            Errors.AddRange(errors);
            SetData(data);
            return this;
        }
    }

    [DataContract]
    public class ServiceResponse : ServiceMessage
    {
        #region Constructors

        public ServiceResponse()
        {
            Message = string.Empty;
            Errors = new List<ValidationModel>();
        }

        #endregion

        #region Properties
        [DataMember]
        public List<ValidationModel> Errors { get; set; }

        private string _message { get; set; }

        public bool HasError { get { return Errors.Any(); } }

        public string Message
        {
            get
            {
                if (HasError)
                    return string.Join("<br />", Errors.Select(i => i.Message).ToList());
                return _message;
            }
            set
            {
                _message = value;
            }
        }

        #endregion

        #region Methods
        public void AddError(string message, string member)
        {
            
            if(!string.IsNullOrWhiteSpace(message))
                Errors.Add(message, member);
        }

        public void AddError(Exception ex)
        {

            Errors.Add(ex.Details(true), "");
        }

        public void AddError(string message, string member, object o)
        {
            if (o == null)
                if (!string.IsNullOrWhiteSpace(message))
                    Errors.Add(message, member);
        }

        public void AddError(string message, string member, bool o)
        {
            if (o == false)
                if (!string.IsNullOrWhiteSpace(message))
                    Errors.Add(message, member);
        }

        public void AddError(string message, string member, string s)
        {
            if (string.IsNullOrEmpty(s))
                if (!string.IsNullOrWhiteSpace(message))
                    Errors.Add(message, member);
        }

        public void AddErrors(List<ValidationModel> errors)
        {
            Errors.AddRange(errors);
        }

        public static implicit operator JsonResult(ServiceResponse response)
        {
            return new JsonResult() { Data = response, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        #endregion
    }

    [DataContract]
    public class ServiceMessage
    {
        [DataMember]
        public Guid TransactionID { get; set; }

        public DateTime TransactionTime { get; set; }

        public ServiceMessage()
        {
            TransactionID = Guid.NewGuid();
            TransactionTime = DateTime.Now;
        }
    }
}
