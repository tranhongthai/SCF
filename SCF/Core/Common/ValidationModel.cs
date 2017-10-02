using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Peyton.Core.Common
{
    public class ValidationModel
    {
        public bool Result { get; set; }
        public string ResultCode { get; set; }
        public string Message { get; set; }
        public List<string> MemberNames { get; set; }
        public ValidationModel()
        {
            MemberNames = new List<string>();
            Message = "";
        }

        public ValidationModel(string message, params string[] memberNames)
            : this()
        {
            Message = message;
            MemberNames.AddRange(memberNames);
        }

        public static implicit operator ValidationResult(ValidationModel obj)
        {
            return new ValidationResult(obj.Message, obj.MemberNames);
        }

        public static implicit operator ValidationModel(ValidationResult obj)
        {
            return new ValidationModel(obj.ErrorMessage, obj.MemberNames.ToArray());
        }
    }
}
