// <copyright file="GraphSearchRequest.cs" company="Lavanya.">
// </copyright>

namespace Microsoft.Movie.Store.Models
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Graph search request.
    /// </summary>
    public class GraphSearchRequest
    {
        /// <summary>
        /// Gets the count of the search resykts.
        /// </summary>
        [JsonPropertyName("count")]
        public bool Count { get; init; }

        /// <summary>
        /// Gets the facets.
        /// </summary>
        [JsonPropertyName("facets")]
        public IEnumerable<string> Facets { get; init; }

        /// <summary>
        /// Gets the filter expression.
        /// </summary>
        [JsonPropertyName("filterExpression")]
        public string FilterExpression { get; init; }

        /// <summary>
        /// Gets the order by.
        /// </summary>
        [JsonPropertyName("orderBy")]
        public string OrderBy { get; init; }

        /// <summary>
        /// Gets the search query.
        /// </summary>
        [JsonPropertyName("search")]
        public string Search { get; init; }

        /// <summary>
        /// Gets the search fields.
        /// </summary>
        [JsonPropertyName("searchFields")]
        public IEnumerable<string> SearchFields { get; init; }

        /// <summary>
        /// Gets the select query.
        /// </summary>
        [JsonPropertyName("select")]
        public IEnumerable<string> Select { get; init; }
    }
}
