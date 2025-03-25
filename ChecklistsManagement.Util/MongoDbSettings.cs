namespace ChecklistsManagement.Util
{
    /// <summary>
    /// Represents the configuration settings for connecting to a MongoDB database.
    /// </summary>
    public class MongoDbSettings
    {
        /// <summary>
        /// Gets or sets the MongoDB connection string.
        /// </summary>
        public string ConnectionString { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the name of the MongoDB database.
        /// </summary>
        public string DatabaseName { get; set; } = string.Empty;
    }
}
