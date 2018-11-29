using Alcadia.Sena.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using System;
using System.Collections.Generic;

namespace Alcadia.Sena.Api.Binders
{
    public class OrderByBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (context.Metadata.ModelType == typeof(IEnumerable<OrderType>))
            {
                return new BinderTypeModelBinder(typeof(OrderByBinder));
            }

            return null;
        }
    }
}
