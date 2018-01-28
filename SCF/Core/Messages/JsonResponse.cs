using System.Linq;
using System.Web.Mvc;

namespace Peyton.Core.Messages
{
    public class JsonResponse<T> : GenericResponse<T>
    {
        public string Redirect { get; set; }
        public bool Refresh { get; set; }
        public bool Success { get { return !HasError; } }

        public JsonResponse() { }

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

        public JsonResponse<T> SetData(GenericResponse<T> response)
        {
            SetData(response.Errors, response.Data);
            return this;
        }

        public bool AddErrors(ServiceResponse data)
        {
            Errors.AddRange(data.Errors);
            return HasError;
        }

        public override void Init()
        {
            base.Init();
            Redirect = "";
            Refresh = false;
        }
    }

    public class JsonResponse : JsonResponse<object>
    {
        public JsonResponse() { }
        public JsonResponse(ModelStateDictionary modelState, params string[] fields) : base(modelState, fields) { }
    }
}
