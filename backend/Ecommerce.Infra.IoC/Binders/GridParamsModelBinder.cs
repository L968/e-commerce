using Ecommerce.Application.Common.Validators;
using Ecommerce.Domain.Entities.Grid;
using Ecommerce.Infra.IoC.Utils;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace Ecommerce.Infra.IoC.Binders;

public class GridParamsModelBinder : IModelBinder
{
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
