using ApiAggregation.Models.Spotify;
using Newtonsoft.Json;

namespace ApiAggregation.Models.Github
{
    /// <summary>
    /// Represents the response from the GitHub API containing a list of repositories.
    /// </summary>
    public class GithubResponse
    {
        /// <summary>
        /// The list of repositories.
        /// </summary>
        public List<Repository> Repositories { get; set; } = new List<Repository>();
    }

    /// <summary>
    /// Represents a GitHub repository.
    /// </summary>
    public class Repository
    {
        /// <summary>
        /// The repository ID.
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; set; }

        /// <summary>
        /// The node ID of the repository.
        /// </summary>
        [JsonProperty("node_id")]
        public string? NodeId { get; set; }

        /// <summary>
        /// The name of the repository.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// The full name.
        /// </summary>
        [JsonProperty("full_name")]
        public string? FullName { get; set; }

        /// <summary>
        /// The owner of the repository.
        /// </summary>
        [JsonProperty("owner")]
        public GitHubUser Owner { get; set; } = new GitHubUser();

        /// <summary>
        /// Indicates whether the repository is private.
        /// </summary>
        [JsonProperty("private")]
        public bool Private { get; set; }

        /// <summary>
        /// The HTML URL of the repository.
        /// </summary>
        [JsonProperty("html_url")]
        public string? HtmlUrl { get; set; }

        /// <summary>
        /// The description of the repository.
        /// </summary>
        [JsonProperty("description")]
        public string? Description { get; set; }

        /// <summary>
        /// Indicates whether the repository is a fork.
        /// </summary>
        [JsonProperty("fork")]
        public bool Fork { get; set; }

        /// <summary>
        /// The API URL of the repository.
        /// </summary>
        [JsonProperty("url")]
        public string? Url { get; set; }

        [JsonProperty("archive_url")]
        public string? ArchiveUrl { get; set; }

        [JsonProperty("assignees_url")]
        public string? AssigneesUrl { get; set; }

        [JsonProperty("blobs_url")]
        public string? BlobsUrl { get; set; }


        [JsonProperty("branches_url")]
        public string? BranchesUrl { get; set; }

        [JsonProperty("collaborators_url")]
        public string? CollaboratorsUrl { get; set; }

        [JsonProperty("comments_url")]
        public string? CommentsUrl { get; set; }

        [JsonProperty("commits_url")]
        public string? CommitsUrl { get; set; }

        [JsonProperty("compare_url")]
        public string? CompareUrl { get; set; }

        [JsonProperty("contents_url")]
        public string? ContentsUrl { get; set; }

        [JsonProperty("contributors_url")]
        public string? ContributorsUrl { get; set; }

        [JsonProperty("deployments_url")]
        public string? DeploymentsUrl { get; set; }

        [JsonProperty("downloads_url")]
        public string? DownloadsUrl { get; set; }

        [JsonProperty("events_url")]
        public string? EventsUrl { get; set; }

        [JsonProperty("forks_url")]
        public string? ForksUrl { get; set; }

        [JsonProperty("git_commits_url")]
        public string? GitCommitsUrl { get; set; }

        [JsonProperty("git_refs_url")]
        public string? GitRefsUrl { get; set; }

        [JsonProperty("git_tags_url")]
        public string? GitTagsUrl { get; set; }

        [JsonProperty("git_url")]
        public string? GitUrl { get; set; }

        [JsonProperty("issue_comment_url")]
        public string? IssueCommentUrl { get; set; }

        [JsonProperty("issue_events_url")]
        public string? IssueEventsUrl { get; set; }

        [JsonProperty("issues_url")]
        public string? IssuesUrl { get; set; }

        [JsonProperty("keys_url")]
        public string? KeysUrl { get; set; }

        [JsonProperty("labels_url")]
        public string? LabelsUrl { get; set; }

        [JsonProperty("languages_url")]
        public string? LanguagesUrl { get; set; }

        [JsonProperty("merges_url")]
        public string? MergesUrl { get; set; }

        [JsonProperty("milestones_url")]
        public string? MilestonesUrl { get; set; }

        [JsonProperty("notifications_url")]
        public string? NotificationsUrl { get; set; }

        [JsonProperty("pulls_url")]
        public string? PullsUrl { get; set; }

        [JsonProperty("releases_url")]
        public string? ReleasesUrl { get; set; }

        [JsonProperty("ssh_url")]
        public string? SshUrl { get; set; }

        [JsonProperty("stargazers_url")]
        public string? StargazersUrl { get; set; }

        [JsonProperty("statuses_url")]
        public string? StatusesUrl { get; set; }

        [JsonProperty("subscribers_url")]
        public string? SubscribersUrl { get; set; }

        [JsonProperty("subscription_url")]
        public string? SubscriptionUrl { get; set; }

        [JsonProperty("tags_url")]
        public string? TagsUrl { get; set; }

        [JsonProperty("teams_url")]
        public string? TeamsUrl { get; set; }

        [JsonProperty("trees_url")]
        public string? TreesUrl { get; set; }

        [JsonProperty("clone_url")]
        public string? CloneUrl { get; set; }

        [JsonProperty("mirror_url")]
        public string? MirrorUrl { get; set; }

        [JsonProperty("hooks_url")]
        public string? HooksUrl { get; set; }

        [JsonProperty("svn_url")]
        public string? SvnUrl { get; set; }

        [JsonProperty("homepage")]
        public string? Homepage { get; set; }

        [JsonProperty("language")]
        public string? Language { get; set; }

        [JsonProperty("forks_count")]
        public int ForksCount { get; set; }

        [JsonProperty("stargazers_count")]
        public int StargazersCount { get; set; }

        [JsonProperty("watchers_count")]
        public int WatchersCount { get; set; }

        [JsonProperty("size")]
        public int Size { get; set; }

        [JsonProperty("default_branch")]
        public string DefaultBranch { get; set; } = string.Empty;

        [JsonProperty("open_issues_count")]
        public int OpenIssuesCount { get; set; }

        [JsonProperty("is_template")]
        public bool IsTemplate { get; set; }

        [JsonProperty("topics")]
        public List<string> Topics { get; set; } = new List<string>();

        [JsonProperty("has_issues")]
        public bool HasIssues { get; set; }

        [JsonProperty("has_projects")]
        public bool HasProjects { get; set; }

        [JsonProperty("has_wiki")]
        public bool HasWiki { get; set; }

        [JsonProperty("has_pages")]
        public bool HasPages { get; set; }

        [JsonProperty("has_downloads")]
        public bool HasDownloads { get; set; }

        [JsonProperty("has_discussions")]
        public bool HasDiscussions { get; set; }

        [JsonProperty("archived")]
        public bool Archived { get; set; }

        [JsonProperty("disabled")]
        public bool Disabled { get; set; }

        [JsonProperty("visibility")]
        public string Visibility { get; set; } = string.Empty;

        [JsonProperty("pushed_at")]
        public DateTime? PushedAt { get; set; }

        [JsonProperty("created_at")]
        public DateTime? CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        [JsonProperty("permissions")]
        public GitHubPermissions Permissions { get; set; } = new GitHubPermissions();

        [JsonProperty("security_and_analysis")]
        public GitHubSecurityAndAnalysis SecurityAndAnalysis { get; set; } = new GitHubSecurityAndAnalysis();
    }

    public class GitHubUser
    {
        [JsonProperty("login")]
        public string? Login { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("node_id")]
        public string? NodeId { get; set; }

        [JsonProperty("avatar_url")]
        public string? AvatarUrl { get; set; }

        [JsonProperty("gravatar_id")]
        public string? GravatarId { get; set; }

        [JsonProperty("url")]
        public string? Url { get; set; }

        [JsonProperty("html_url")]
        public string? HtmlUrl { get; set; }

        [JsonProperty("followers_url")]
        public string? FollowersUrl { get; set; }

        [JsonProperty("following_url")]
        public string? FollowingUrl { get; set; }

        [JsonProperty("gists_url")]
        public string? GistsUrl { get; set; }

        [JsonProperty("starred_url")]
        public string? StarredUrl { get; set; }

        [JsonProperty("subscriptions_url")]
        public string? SubscriptionsUrl { get; set; }

        [JsonProperty("organizations_url")]
        public string? OrganizationsUrl { get; set; }

        [JsonProperty("repos_url")]
        public string? ReposUrl { get; set; }

        [JsonProperty("events_url")]
        public string? EventsUrl { get; set; }

        [JsonProperty("received_events_url")]
        public string? ReceivedEventsUrl { get; set; }

        [JsonProperty("type")]
        public string? Type { get; set; }

        [JsonProperty("site_admin")]
        public bool SiteAdmin { get; set; }
    }

    public class GitHubPermissions
    {
        [JsonProperty("admin")]
        public bool Admin { get; set; }

        [JsonProperty("push")]
        public bool Push { get; set; }

        [JsonProperty("pull")]
        public bool Pull { get; set; }
    }

    public class GitHubSecurityAndAnalysis
    {
        [JsonProperty("advanced_security")]
        public GitHubAdvancedSecurity AdvancedSecurity { get; set; } = new GitHubAdvancedSecurity();

        [JsonProperty("secret_scanning")]
        public GitHubSecretScanning SecretScanning { get; set; } = new GitHubSecretScanning();

        [JsonProperty("secret_scanning_push_protection")]
        public GitHubSecretScanningPushProtection SecretScanningPushProtection { get; set; } = new GitHubSecretScanningPushProtection();
    }

    public class GitHubAdvancedSecurity
    {
        [JsonProperty("status")]
        public string Status { get; set; } = string.Empty;
    }

    public class GitHubSecretScanning
    {
        [JsonProperty("status")]
        public string Status { get; set; } = string.Empty;
    }

    public class GitHubSecretScanningPushProtection
    {
        [JsonProperty("status")]
        public string Status { get; set; } = string.Empty;
    }
}
