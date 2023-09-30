// <copyright file="MovieIndexRecord.cs" company="Lavanya.">
// </copyright>

namespace Microsoft.Movie.Store.Models
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Movie details.
    /// </summary>
    public class MovieIndexRecord
    {
        /// <summary>
        /// Gets or sets the Movie identifier.
        /// </summary>
        [JsonIgnore]
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the movie title.
        /// </summary>
        [JsonPropertyName("title")]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the movie year.
        /// </summary>
        [JsonPropertyName("year")]
        public int Year { get; set; }

        /// <summary>
        /// Gets or sets the Genres.
        /// </summary>
        [JsonPropertyName("genres")]
        public IEnumerable<string> Genre { get; set; }

        /// <summary>
        /// Gets or sets the thumbnail.
        /// </summary>
        [JsonPropertyName("thumbnail")]
        public string Thumbnail { get; set; }

        /// <summary>
        /// Gets or sets the summary.
        /// </summary>
        [JsonPropertyName("extract")]
        public string Summary { get; set; }
    }
}
