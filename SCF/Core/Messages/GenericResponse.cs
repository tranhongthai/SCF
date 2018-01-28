using System;
using System.Collections.Generic;
using Peyton.Core.Common;

namespace Peyton.Core.Messages
{
    public class GenericResponse<T> : ServiceResponse
    {
        public T Data { get; set; }

        public GenericResponse() { }
        public GenericResponse(ServiceRequest request) : base(request) { }

        public GenericResponse<T> SetData(T data)
        {
            Data = HasError ? Activator.CreateInstance<T>() : data;
            return this;
        }

        public GenericResponse<T> SetData(T successData, T failData)
        {
            Data = HasError ? failData : successData;
            return this;
        }

        public GenericResponse<T> SetData(List<ValidationModel> errors, T data)
        {
            Errors = errors ?? new List<ValidationModel>();
            SetData(data);
            return this;
        }

        public override void Init()
        {
            base.Init();
            Data = (T)Activator.CreateInstance(typeof(T));
        }
    }
}