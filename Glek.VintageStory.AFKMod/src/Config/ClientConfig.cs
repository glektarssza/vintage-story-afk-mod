namespace Glek.VintageStory.AFKMod.Config;

using Vintagestory.API.Common;

/// <summary>
/// The client-side mod configuration.
/// </summary>
public class ClientConfig : CommonConfig<AFKModSystem, ClientConfig> {
    #region Constructors/Finalizer

    /// <summary>
    /// Create a new instance.
    /// </summary>
    /// <param name="modSystem">
    /// The mod system the new instance will belong to.
    /// </param>
    public ClientConfig(AFKModSystem modSystem) : base(modSystem) {
        // -- Does nothing
    }

    #endregion
}
