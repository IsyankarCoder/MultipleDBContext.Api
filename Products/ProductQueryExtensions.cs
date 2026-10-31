using System.Linq.Expressions;

namespace MultipleDBContext.Api.Products
{
    public static class ProductQueryExtensions
    {
        public static IOrderedQueryable<Product> ApplySort(
            this IQueryable<Product> query,
            IEnumerable<string>? sortFields)
        {
            IOrderedQueryable<Product>? orderedQuery = null;

            foreach (var requestedSort in sortFields ?? [])
            {
                if (string.IsNullOrWhiteSpace(requestedSort))
                {
                    throw new ArgumentException("Sıralama alanı boş olamaz.", nameof(sortFields));
                }

                var descending = requestedSort.StartsWith("-", StringComparison.Ordinal);
                var propertyName = descending ? requestedSort[1..] : requestedSort;

                orderedQuery = propertyName.Trim().ToLowerInvariant() switch
                {
                    "name" => AddSort(query, orderedQuery, product => product.Name, descending),
                    "sku" => AddSort(query, orderedQuery, product => product.Sku, descending),
                    "price" => AddSort(query, orderedQuery, product => product.Price.Amount, descending),
                    "currencycode" => AddSort(query, orderedQuery, product => product.Price.CurrencyCode, descending),
                    "id" => AddSort(query, orderedQuery, product => product.Id, descending),
                    _ => throw new ArgumentException(
                        $"Geçersiz sıralama alanı: '{propertyName}'.",
                        nameof(sortFields))
                };
            }

            return orderedQuery is null
                ? query.OrderBy(product => product.Id)
                : orderedQuery.ThenBy(product => product.Id);
        }

        private static IOrderedQueryable<Product> AddSort<TKey>(
            IQueryable<Product> query,
            IOrderedQueryable<Product>? orderedQuery,
            Expression<Func<Product, TKey>> keySelector,
            bool descending)
        {
            if (orderedQuery is null)
            {
                return descending
                    ? query.OrderByDescending(keySelector)
                    : query.OrderBy(keySelector);
            }

            return descending
                ? orderedQuery.ThenByDescending(keySelector)
                : orderedQuery.ThenBy(keySelector);
        }
    }
}
