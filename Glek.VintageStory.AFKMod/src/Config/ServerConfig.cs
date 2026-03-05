namespace Glek.VintageStory.AFKMod.Config;

/// <summary>
/// The server-side mod configuration.
/// </summary>
public class ServerConfig : CommonConfig<AFKModSystem, ServerConfig> {
    #region Public Constants

    /// <summary>
    /// The name of the file name to load this configuration from.
    /// </summary>
    public const string FILE_NAME = "%s-server.json";

    #endregion

    #region Constructors/Finalizer

    /// <summary>
    /// Create a new instance.
    /// </summary>
    /// <param name="modSystem">
    /// The mod system the new instance will belong to.
    /// </param>
    public ServerConfig(AFKModSystem modSystem) : base(modSystem) {
        // -- Does nothing
    }

    #endregion
}
