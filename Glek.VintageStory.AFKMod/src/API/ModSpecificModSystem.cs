namespace Glek.VintageStory.AFKMod.API;

using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.Server;

/// <summary>
/// An interface which encapsulates a mod-specific mod system.
/// </summary>
public abstract class ModSpecificModSystem : ModSystem {
    #region Public Properties

    /// <summary>
    /// The common Vintage Story API.
    /// </summary>
    /// <remarks>
    /// This property may be <c>null</c> if the mod has not yet been initialized.
    /// </remarks>
    public ICoreAPI? API {
        get;
        private set;
    }

    /// <summary>
    /// The client-side Vintage Story API.
    /// </summary>
    /// <remarks>
    /// This property may be <c>null</c> if the mod has not yet been initialized
    /// in a client environment.
    /// </remarks>
    public ICoreClientAPI? ClientAPI {
        get;
        private set;
    }

    /// <summary>
    /// The server-side Vintage Story API.
    /// </summary>
    /// <remarks>
    /// This property may be <c>null</c> if the mod has not yet been initialized
    /// in a server environment.
    /// </remarks>
    public ICoreServerAPI? ServerAPI {
        get;
        private set;
    }

    /// <summary>
    /// The side (server or client) the mod is running on.
    /// </summary>
    /// <remarks>
    /// This property may be <c>null</c> if the mod has not yet been initialized.
    /// </remarks>
    public EnumAppSide? Side => this.API?.Side;

    /// <summary>
    /// The mod-specific logger.
    /// </summary>
    /// <remarks>
    /// This property may be <c>null</c> if the mod has not yet been initialized.
    /// </remarks>
    public ILogger? Logger => this.API?.Logger;

    #endregion

    #region Constructors/Finalizer

    /// <summary>
    /// Create a new instance.
    /// </summary>
    protected ModSpecificModSystem() {
        // -- Does nothing.
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Handle pre-startup code.
    /// </summary>
    /// <param name="api">
    /// The Vintage Story API.
    /// </param>
    public override void StartPre(ICoreAPI api) {
        this.API = api;
        base.StartPre(api);
    }

    /// <summary>
    /// Handle startup code.
    /// </summary>
    /// <param name="api">
    /// The Vintage Story API.
    /// </param>
    public override void Start(ICoreAPI api) {
        this.API = api;
        base.Start(api);
    }

    /// <summary>
    /// Handle startup code.
    /// </summary>
    /// <param name="api">
    /// The Vintage Story API.
    /// </param>
    public override void StartClientSide(ICoreClientAPI api) {
        this.ClientAPI = api;
        base.Start(api);
    }

    /// <summary>
    /// Handle startup code.
    /// </summary>
    /// <param name="api">
    /// The Vintage Story API.
    /// </param>
    public override void StartServerSide(ICoreServerAPI api) {
        this.ServerAPI = api;
        base.Start(api);
    }

    #endregion
}
