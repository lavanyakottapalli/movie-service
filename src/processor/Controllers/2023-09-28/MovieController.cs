// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Microsoft.Movie.Store.Controllers.V1
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Movie.Store.Models;
    using Microsoft.Movie.Store.Workflow;
    using Swashbuckle.AspNetCore.Filters;
    using System.ComponentModel.DataAnnotations;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Controller to handle requests for movies.
    /// </summary>
    [Route("movies")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private ISearchWorkflow<MovieIndexRecord> searchWorkflow;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="searchWorkflow">searchWorkflow.</param>
        public MovieController(ISearchWorkflow<MovieIndexRecord> searchWorkflow)
        {
            this.searchWorkflow = searchWorkflow;
        }

        /// <summary>
        /// Gets all assets.
        /// </summary>
        /// <param name="httpRequest">The graph search request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task.</returns>
        [HttpPost]
        [Route("search")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(List<MovieIndexRecord>), 200)]
        [SwaggerResponseHeader(new int[] { 200, 201, 500 }, "x-ms-request-tracking-id", "string", "Client request tracking Id.")]
        [SwaggerResponseHeader(new int[] { 200, 201, 500 }, "tenant-name", "string", "Tenant Name of UDR Instance.")]
        public async Task<ActionResult<List<MovieIndexRecord>>> SearchMoviesAsync([Required][NotNull][FromBody] GraphSearchRequest httpRequest, [NotNull] CancellationToken cancellationToken = default)
        {
            List<MovieIndexRecord> moviesGraphRecord = await this.searchWorkflow.SearchDocumentsAsync(httpRequest, cancellationToken).ConfigureAwait(false);

            return this.Ok(moviesGraphRecord.Where((x) => x.Thumbnail != null).ToList());
        }
    }
}
