using System.Collections.Generic;
using System.Linq;

// ReSharper disable once CheckNamespace

namespace System.ComponentModel.DataAnnotations
{
    public static class ListValidationResultExt
    {
        public static List<ValidationResult> Add(this List<ValidationResult> obj, string message, string memberNames)
        {
            if (obj.Any(i => i.MemberNames.Any(j => j == memberNames)))
                return obj;

            if (!string.IsNullOrWhiteSpace(message))
                obj.Add(new ValidationResult(message, memberNames.Split(";")));
            return obj;
        }

        public static List<ValidationResult> Add(this List<ValidationResult> obj, string message, string memberNames,
            string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                obj.Add(message, memberNames);
            return obj;
        }

        public static List<ValidationResult> Add(this List<ValidationResult> obj, string message, string memberNames,
            bool value)
        {
            if (value)
                obj.Add(message, memberNames);
            return obj;
        }

        public static List<ValidationResult> Add(this List<ValidationResult> obj, string message, string memberNames,
            long value)
        {
            if (value == 0)
                obj.Add(message, memberNames);
            return obj;
        }

        public static List<ValidationResult> Add(this List<ValidationResult> obj, string message, string memberNames,
            long? value)
        {
            if (!value.HasValue)
                obj.Add(message, memberNames);
            return obj;
        }

        public static List<ValidationResult> Add(this List<ValidationResult> obj, string message,
            params string[] memberNames)
        {
            if (!string.IsNullOrWhiteSpace(message))
                obj.Add(new ValidationResult(message, memberNames));
            return obj;
        }

        public static List<ValidationResult> Add(this List<ValidationResult> obj, Enum message,
            params string[] memberNames)
        {
            obj.Add(new ValidationResult(message.DisplayName(), memberNames));
            return obj;
        }

        public static List<ValidationResult> Add(this List<ValidationResult> obj, string message, string memberName,
            DateTime? val)
        {
            if (!val.HasValue || val == DateTime.MinValue || val == DateTime.MaxValue)
                obj.Add(message, memberName);
            return obj;
        }

        public static List<ValidationResult> Add(this List<ValidationResult> obj, string message, string memberName,
            DateTime val)
        {
            if (val == DateTime.MinValue || val == DateTime.MaxValue)
                obj.Add(message, memberName);
            return obj;
        }

        public static List<ValidationResult> Add<T>(this List<ValidationResult> obj, string message, string memberName,
            T val)
        {
            if (val == null)
                obj.Add(message, memberName);
            return obj;
        }
    }
}