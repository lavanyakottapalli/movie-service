// <copyright file="MovieId.cs" company="Lavanya.">
// </copyright>

using System.Text.Json.Serialization;

namespace Microsoft.Movie.Store.Models
{
    /// <summary>
    /// Movie identifier
    /// </summary>
    public class MovieId
    {
        /// <summary>
        /// Gets or sets the movide id.
        /// </summary>
        [JsonPropertyName("id")]
        public int Id { get; set; }
    }
}
