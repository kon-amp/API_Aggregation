using System.ComponentModel.DataAnnotations;

namespace ApiAggregation.Models.Spotify
{
    /// <summary>
    /// Represents a request to get information about multiple artists from the Spotify API.
    /// </summary>
    public class SpotifyArtistsRequest
    {
        // Maximum: 50 IDs.
        // Each ID is 22 characters.
        // The maximum length of the string is calculated as follows:
        // 50 IDs * 22 characters per ID + 49 commas (maximum) = 1149 characters.
        /// <summary>
        /// A comma-separated list of the Spotify IDs for the artists. 
        /// Maximum: 50 IDs.
        /// Each ID is 22 characters.
        /// The maximum length of the string is calculated as follows:
        /// 50 IDs * 22 characters per ID + 49 commas (maximum) = 1149 characters.
        /// </summary>
        [MaxLength(1149, ErrorMessage = "The 'Ids' field cannot exceed 1149 characters (50 IDs with comma separators).")]
        public string Ids { get; set; } = "2CIMQHirSU0MQqyYHq0eOx,57dN52uHvrHOxijzpIgu3E,1vCWHaC5f2uS3yhpwWbIA";
    }
}
