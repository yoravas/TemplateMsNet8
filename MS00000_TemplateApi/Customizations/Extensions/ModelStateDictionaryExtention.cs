using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MS00000_TemplateApi.Customizations.Extensions;
public static class ModelStateDictionaryExtention
{
    const string DefaultError = "DefaultErrorModelState";
    public static Dictionary<string, object> SerializeErrors(this ModelStateDictionary modelState)
    {
        Dictionary<string, object> keyValuePairs = new Dictionary<string, object>();

        if (modelState == null)
        {
            throw new ArgumentNullException(nameof(modelState));
        }

        if (modelState.IsValid)
        {
            return keyValuePairs;
        }

        foreach (KeyValuePair<string, ModelStateEntry> keyModelStatePairs in modelState)
        {
            string key = keyModelStatePairs.Key;
            ModelErrorCollection errors = keyModelStatePairs.Value.Errors;

            if (errors != null && errors.Count > 0)
            {
                string[] errorMessages = errors.Select(error =>
                {
                    return string.IsNullOrEmpty(error.ErrorMessage) ?
                        DefaultError : error.ErrorMessage;
                }).ToArray();
                keyValuePairs.Add(key, errorMessages);
            }
        }

        return keyValuePairs;
    }
}