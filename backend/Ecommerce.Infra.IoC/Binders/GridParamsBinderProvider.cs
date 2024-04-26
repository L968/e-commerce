using Ecommerce.Domain.Entities.Grid;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Ecommerce.Infra.IoC.Binders;

public class GridParamsBinderProvider : IModelBinderProvider
{
    public IModelBinder GetBinder(ModelBinderProviderContext context)
    {
        ArgumentNullException.ThrowIfNull(context);

        if (context.Metadata.ModelType == typeof(GridParams))
        {
            return new BinderTypeModelBinder(typeof(GridParamsModelBinder));
        }

        return null;
    }
}
