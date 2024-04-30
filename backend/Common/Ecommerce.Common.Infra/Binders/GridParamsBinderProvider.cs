using Ecommerce.Common.Infra.Representation.Grid;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace Ecommerce.Common.Infra.Binders;

/// <summary>
/// Provider for binding GridParams objects as strings from the request.
/// </summary>
public class GridParamsBinderProvider : IModelBinderProvider
{
    /// <summary>
    /// Gets a binder for the specified context.
    /// </summary>
    /// <param name="context">The context for which to create a binder.</param>
    /// <returns>A binder for the specified context, or null if a binder cannot be created.</returns>
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
