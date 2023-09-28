// <copyright file="PopulateMovieStoreWorkflow.cs" company="Lavanya.">
// </copyright>

namespace Microsoft.Movie.Store.Workflow
{
    using Azure.Search.Documents;
    using Azure.Search.Documents.Models;
    using Microsoft.Extensions.Azure;
    using Microsoft.Movie.Store.Models;
    using RestSharp;
    using System;
    using System.Collections.Generic;
    using System.Text.Json;
    using System.Threading.Tasks;

    /// <summary>
    /// Workflow for the Continous SLNM Kpi.
    /// </summary>
    public class PopulateMovieStoreWorkflow : IProcessorWorkflow
    {
        private readonly SearchClient searchClient;

        private readonly RestClient restClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="PopulateMovieStoreWorkflow"/> class.
        /// </summary>
        public PopulateMovieStoreWorkflow(RestClient restClient, SearchClient searchClient)
        {
            this.restClient = restClient;
            this.searchClient = searchClient;
        }

        /// <summary>
        /// Task to populate the Continous Slnm Kpi Table.
        /// </summary>
        /// <returns>Task.</returns>
        public async Task ProcessAsync()
        {
            int page = 1, totalPages = 1;
            bool isFirstTime = true;

            do
            {
                var requestMovieChanges = new RestRequest($"https://api.themoviedb.org/3/movie/changes?page={page}");
                requestMovieChanges.AddHeader("accept", "application/json");
                requestMovieChanges.AddHeader("Authorization", "Bearer ");
                RestResponse response = await this.restClient.GetAsync(requestMovieChanges).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    MovieIdsResponse movieIdsResponse = JsonSerializer.Deserialize<MovieIdsResponse>(response.Content);

                    if (isFirstTime)
                    {
                        totalPages = movieIdsResponse.TotalPages;
                        isFirstTime = false;
                    }

                    List<Movie> movies = new List<Movie>();
                    foreach (var movieId in movieIdsResponse.MovieIds)
                    {
                        var movieRequest = new RestRequest($"https://api.themoviedb.org/3/movie/{movieId}");
                        movieRequest.AddHeader("accept", "application/json");
                        movieRequest.AddHeader("Authorization", "Bearer ");

                        RestResponse movieDetails = await this.restClient.GetAsync(movieRequest).ConfigureAwait(false);
                        Movie movie = JsonSerializer.Deserialize<Movie>(movieDetails.Content);

                        movies.Add(movie);
                    }

                    await this.searchClient.MergeOrUploadDocumentsAsync(movies).ConfigureAwait(false);
                }

                page++;
            } while (page < totalPages);
        }
    }
}
