// <copyright file="SearchWorkflow.cs" company="Lavanya.">
// </copyright>

namespace Microsoft.Movie.Store.Workflow
{
    using Azure.Search.Documents.Models;
    using Azure.Search.Documents;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System;
    using Microsoft.Movie.Store.Models;
    using System.ComponentModel.DataAnnotations;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Threading;

    /// <summary>
    /// Class to synthesize different search workflows.
    /// </summary>
    public class SearchWorkflow<T> : ISearchWorkflow<T>
        where T : class
    {
        private readonly SearchClient searchClient;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="searchClient">Search Client.</param>
        public SearchWorkflow(SearchClient searchClient)
        {
            this.searchClient = searchClient;
        }

        /// <inheritdoc/>
        public async Task<List<T>> SearchDocumentsAsync(
           [Required][NotNull] GraphSearchRequest graphSearchRequest,
           [NotNull] CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ArgumentNullException.ThrowIfNull(graphSearchRequest, nameof(graphSearchRequest));
            ArgumentNullException.ThrowIfNull(cancellationToken);

            List<T> results = new();
            string filterExpression = $"";
            if (graphSearchRequest.FilterExpression != null)
            {
                filterExpression += $" and {graphSearchRequest.FilterExpression}";
            }

            SearchOptions options = new()
            {
                Size = 30,
                Filter = filterExpression,
            };
            if (graphSearchRequest.Facets != null)
            {
                foreach (string each in graphSearchRequest.Facets.ToList())
                {
                    options.Facets.Add(each);
                }
            }

            SearchResults<T> response;
            long totalCount = 0;
            string search = string.IsNullOrEmpty(graphSearchRequest.Search) ? "*" : graphSearchRequest.Search;
#pragma warning disable CA1031 // Do not catch general exception types
            try
            {
                response = await this.searchClient.SearchAsync<T>(search, options, cancellationToken).ConfigureAwait(false);

                totalCount = response.TotalCount.GetValueOrDefault(0);
                await foreach (SearchResult<T> result in response.GetResultsAsync())
                {
                    results.Add(result.Document);
                }
            }

            // TODO: Fix Exception Handling
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
#pragma warning restore CA1031 // Do not catch general exception types

            return results;
        }
    }
}
