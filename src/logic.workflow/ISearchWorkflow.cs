// <copyright file="ISearchWorkflow.cs" company="Lavanya.">
// </copyright>

namespace Microsoft.Movie.Store.Workflow
{
    using Microsoft.Movie.Store.Models;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Workflow Processor.
    /// </summary>
    public interface ISearchWorkflow<T> where T : class
    {
        /// <summary>
        /// Processor that can be used to build business logic
        /// that can execute in the background.
        /// </summary>
        /// <returns>Task.</returns>
        public Task<List<T>> SearchDocumentsAsync([Required][NotNull] GraphSearchRequest graphSearchRequest, CancellationToken cancellationToken = default);
    }
}
