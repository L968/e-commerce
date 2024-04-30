using Ecommerce.Common.Infra.Extensions;
using Ecommerce.Common.Infra.Representation.Grid;
using Ecommerce.Common.Infra.Validators;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace Ecommerce.Common.Infra.Binders;

/// <summary>
/// Model binder for binding GridParams objects from JSON in the request.
/// </summary>
public class GridParamsModelBinder : IModelBinder
{
    /// <summary>
    /// Binds the GridParams object from JSON string in the request.
    /// </summary>
    /// <param name="bindingContext">The binding context.</param>
    /// <returns>A task representing the asynchronous operation of binding the model.</returns>
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        string modelName = bindingContext.ModelName;
        ValueProviderResult value = bindingContext.ValueProvider.GetValue(modelName);
        if (value == ValueProviderResult.None)
        {
            bindingContext.Result = ModelBindingResult.Success(new GridParams());
            return Task.CompletedTask;
        }

        bindingContext.ModelState.SetModelValue(modelName, value);
        string firstValue = value.FirstValue!;

        if (string.IsNullOrEmpty(firstValue))
        {
            bindingContext.Result = ModelBindingResult.Failed();
            bindingContext.ModelState.AddModelError(modelName, "Value cannot be empty");
            return Task.CompletedTask;
        }

        if (!firstValue.IsValidJson())
        {
            bindingContext.Result = ModelBindingResult.Failed();
            bindingContext.ModelState.AddModelError(modelName, "Invalid JSON format");
            return Task.CompletedTask;
        }

        GridParams model;

        try
        {
            model = JsonConvert.DeserializeObject<GridParams>(firstValue)!;
        }
        catch (JsonReaderException ex)
        {
            bindingContext.Result = ModelBindingResult.Failed();
            bindingContext.ModelState.AddModelError(modelName, ex.Message);
            return Task.CompletedTask;
        }

        var validator = new GridParamsValidator();
        var validatorResult = validator.Validate(model);

        if (!validatorResult.IsValid)
        {
            bindingContext.Result = ModelBindingResult.Failed();

            foreach (ValidationFailure error in validatorResult.Errors)
            {
                bindingContext.ModelState.AddModelError(modelName, error.ErrorMessage);
            }

            return Task.CompletedTask;
        }

        bindingContext.Result = ModelBindingResult.Success(model);
        return Task.CompletedTask;
    }
}
