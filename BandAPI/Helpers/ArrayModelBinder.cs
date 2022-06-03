using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace BandAPI.Helpers
{
    public class ArrayModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            //make sure that modelbinder works only for IEnumerable types
            if (!bindingContext.ModelMetadata.IsEnumerableType)
            {
                bindingContext.Result = ModelBindingResult.Failed();
                return Task.CompletedTask;
            }
            //get value from input
            var value=bindingContext.ValueProvider.GetValue(bindingContext.ModelName).ToString();

            //check if its null
            if (string.IsNullOrWhiteSpace(value))
            {
                //null is not the value we can process, however, it is not a failed result; it can still be ienumerable type
                //action will return bad request
                bindingContext.Result = ModelBindingResult.Success(null);
                return Task.CompletedTask;
            }

            //get the type
            var elementType = bindingContext.ModelType.GetTypeInfo().GenericTypeArguments[0];

            var converter=TypeDescriptor.GetConverter(elementType);

            var values = value.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => converter.ConvertFromString(x.Trim())).ToArray();

            var typeValues = Array.CreateInstance(elementType, values.Length);
            values.CopyTo(typeValues, 0);
            bindingContext.Model = typeValues;

            bindingContext.Result = ModelBindingResult.Success(bindingContext.Model);
            return Task.CompletedTask;

        }
    }
}
