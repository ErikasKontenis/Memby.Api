using Memby.Core.Resources;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;

namespace Memby.Models
{
    public class ModelStateResult
    {
        public ModelStateResult(ModelStateDictionary modelState)
        {
            Message = Messages.SimpleValidationMessage;
            MessageCode = nameof(Messages.SimpleValidationMessage);
            Errors = modelState.Keys
                    .SelectMany(key => modelState[key].Errors.Select(x => new ValidationError(key, x.ErrorMessage)))
                    .ToList();
        }

        public string Message { get; }

        public string MessageCode { get; }

        public List<ValidationError> Errors { get; }
    }
}
