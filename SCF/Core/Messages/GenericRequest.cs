using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Peyton.Core.Messages
{
    public class GenericRequest<T> : ServiceRequest where T : IValidatableObject
    {
        public T Data { get; set; }

        public GenericRequest() { }

        public GenericRequest(T data) : this()
        {
            Data = data;
        }

        public override List<ValidationResult> Validate()
        {
            var result = base.Validate();
            Data = Data == null ? (T)Activator.CreateInstance(typeof(T)) : Data;
            result.AddRange(Data.Validate(null));
            return result;
        }

        public override void Init()
        {
            base.Init();
            Data = (T)Activator.CreateInstance(typeof(T));
        }
    }
}