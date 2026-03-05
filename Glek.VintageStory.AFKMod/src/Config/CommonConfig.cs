namespace Glek.VintageStory.AFKMod.Config;

using Glek.VintageStory.AFKMod.API;

using Newtonsoft.Json;

using Vintagestory.API.Common;

/// <summary>
/// The common mod configuration.
/// </summary>
public abstract class CommonConfig<TModSystem, TConfig> where TConfig : CommonConfig<TModSystem, TConfig> where TModSystem : ModSpecificModSystem {
    #region Public Properties

    /// <summary>
    /// The mod system this configuration belongs to.
    /// </summary>
    [JsonIgnore]
    public TModSystem ModSystem {
        get;
    }

    #endregion

    #region Constructors/Finalizer

    /// <summary>
    /// Create a new instance.
    /// </summary>
    /// <param name="modSystem">
    /// The mod-specific mod system the new instance will belong to.
    /// </param>
    protected CommonConfig(TModSystem modSystem) =>
        this.ModSystem = modSystem;

    #endregion

    #region Public Methods

    /// <summary>
    /// Load the configuration from the given file name.
    /// </summary>
    /// <param name="fileName"></param>
    public void LoadFromFile(string fileName) =>
        this.ModSystem.API?.LoadModConfig<TConfig>(fileName);

    #endregion
}
