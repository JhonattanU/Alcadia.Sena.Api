using Alcadia.Sena.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alcadia.Sena.Api.Binders
{
    public class OrderByBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null) throw new ArgumentNullException(nameof(bindingContext));
            if (bindingContext.ModelType != typeof(IEnumerable<OrderType>)) return Task.CompletedTask;

            // Specify a default argument name if none is set by ModelBinderAttribute
            var modelName = bindingContext.ModelName;

            // Try to fetch the value of the argument by name
            var valueProviderResult = bindingContext.ValueProvider.GetValue(modelName);

            if (valueProviderResult == ValueProviderResult.None) return Task.CompletedTask;

            bindingContext.ModelState.SetModelValue(modelName, valueProviderResult);

            var modelList = new List<OrderType>();

            foreach (var value in valueProviderResult)
            {
                // Check if the argument value is null or empty 
                if (string.IsNullOrWhiteSpace(value)) continue;

                // Split the values
                var model = value.Split(',');

                // Check if contains any value afther splitting
                if (!model.Any()) continue;

                //Get the column name from index 0
                string columnName = string.Empty;
                string sort = string.Empty;

                try
                {
                    columnName = model[0].Trim();
                    sort = model[1].Trim();
                }
                catch (Exception)
                {
                    continue;
                }

                // Check if column is not null
                if (string.IsNullOrWhiteSpace(columnName)) continue;


                Enum.TryParse(sort, true, out SortType sortType);
                if (!Enum.IsDefined(typeof(SortType), sortType)) sortType = SortType.asc;


                modelList.Add(new OrderType { ColumnName = $"[{columnName}]", SortType = sortType });
            }

            bindingContext.Result = ModelBindingResult.Success(modelList);

            return Task.CompletedTask;

        }
    }
}
