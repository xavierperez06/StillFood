using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace StillFood.WEB.Utils
{
    /// <summary>
    /// Fix the binding decimal for CurrentCulture used
    /// </summary>
    public class DecimalModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {

            ValueProviderResult valueResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName + ".ModelType");

            if (valueResult == null)
            {
                var enumerable = (IEnumerable)bindingContext.ValueProvider;

                var formValueProvider = enumerable.OfType<FormValueProvider>().Single();

                var field = typeof(NameValueCollectionValueProvider).GetField(
                                "_values",
                                BindingFlags.NonPublic | BindingFlags.Instance
                            );

                var dictionary = (IDictionary)field.GetValue(formValueProvider);

                var keys = (IEnumerable<string>)dictionary.Keys;

                var key = keys.FirstOrDefault(x => x.EndsWith(bindingContext.ModelName + ".Value"));

                valueResult = bindingContext.ValueProvider.GetValue(key);
            }

            ModelState modelState = new ModelState { Value = valueResult };

            object actualValue = null;
            try
            {
                //Check if this is a nullable decimal and a null or empty string has been passed
                var isNullableAndNull = (bindingContext.ModelMetadata.IsNullableValueType &&
                                         string.IsNullOrEmpty(valueResult.AttemptedValue));

                //If not nullable and null then we should try and parse the decimal
                if (!isNullableAndNull)
                {
                    actualValue = decimal.Parse(valueResult.AttemptedValue, NumberStyles.Any, CultureInfo.CurrentCulture);
                }
            }
            catch (FormatException e)
            {
                modelState.Errors.Add(e);
            }

            bindingContext.ModelState.Add(bindingContext.ModelName, modelState);
            return actualValue;
        }
    }
}