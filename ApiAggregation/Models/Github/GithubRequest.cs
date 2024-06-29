namespace ApiAggregation.Models.Github
{
    /// <summary>
    /// Represents a request to get repositories from the GitHub API for a specific user.
    /// </summary>
    public class GithubRequest
    {
        /// <summary>
        /// The handle for the GitHub user account.
        /// </summary>
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// The type of repositories to list.
        /// </summary>
        public string Type { get; set; } = "owner"; 

        /// <summary>
        /// The property to sort the results by.
        /// </summary>
        public string Sort { get; set; } = "full_name"; 

        /// <summary>
        /// The order to sort the results by.
        /// </summary>
        public string Direction { get; set; } = "asc"; 

        /// <summary>
        /// The number of results per page (max 100).
        /// </summary>
        public int PerPage { get; set; } = 30; 

        /// <summary>
        /// The page number of the results to fetch.
        /// </summary>
        public int Page { get; set; } = 1; 

        /// <summary>
        /// The GitHub API key for authenticating the request.
        /// </summary>
        public string ApiKey { get; set; } = string.Empty;
    }
}