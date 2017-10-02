using Peyton.Core.Messages;
using System.Linq;

namespace System.Web.Mvc
{
    public class JsonResponse<T> : ServiceResponse<T>
    {
        public string Redirect { get; set; }
        public bool Refresh { get; set; }
        public bool Success { get { return !HasError; } }
        public JsonResponse()
        {
            try
            {
                Data = (T)Activator.CreateInstance(typeof(T));
            }
            catch
            {
                Data = default(T);
            }
            Redirect = string.Empty;
            Refresh = false;
        }
        public JsonResponse(ModelStateDictionary modelState, params string[] fields)
            : this()
        {
            var keys = modelState.Keys.ToArray();
            if (fields.Any())
                keys = keys.Where(i => !fields.Contains(i)).ToArray();
            foreach (var item in keys)
                if (!modelState.IsValidField(item))
                    AddError(modelState[item].Errors[0].ErrorMessage, item);
        }
        public JsonResponse<T> SetData(ServiceResponse<T> response)
        {
            SetData(response.Errors, response.Data);
            return this;
        }
        public bool AddErrors(ServiceResponse data)
        {
            Errors.AddRange(data.Errors);
            return HasError;
        }
    }

    public class JsonResponse : JsonResponse<object>
    {
        public new string Redirect { get; set; }
        public new bool Success { get { return !HasError; } }

        public JsonResponse()
        {
            Data = new object();
            Redirect = string.Empty;
        }
    }
}
