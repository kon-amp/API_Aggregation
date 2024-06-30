using System.ComponentModel.DataAnnotations;

namespace ApiAggregation.Models.User
{
    /// <summary>
    /// Represents the login credentials of a user.
    /// </summary>
    public class LoginModel
    {
        /// <summary>
        /// The username for login.
        /// </summary>
        [Required]
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// The password for login.
        /// </summary>
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
