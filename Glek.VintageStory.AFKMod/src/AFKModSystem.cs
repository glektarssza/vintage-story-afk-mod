namespace Glek.VintageStory.AFKMod;

using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.Config;
using Vintagestory.API.Server;

/// <summary>
/// The mod system.
/// </summary>
public class AFKModSystem : ModSystem {
    /// <summary>
    /// The startup method, called on both the server and client.
    /// </summary>
    /// <param name="api">
    /// The core API object.
    /// </param>
    public override void Start(ICoreAPI api) =>
        this.Mod.Logger.Notification("Hello from template mod: " + api.Side);

    /// <summary>
    /// The startup method, called on the server only.
    /// </summary>
    /// <param name="api">
    /// The core API object.
    /// </param>
    public override void StartServerSide(ICoreServerAPI api) =>
        this.Mod.Logger.Notification("Hello from template mod server side: " + Lang.Get("gleksafkmod:hello"));

    /// <summary>
    /// The startup method, called on the client only.
    /// </summary>
    /// <param name="api">
    /// The core API object.
    /// </param>
    public override void StartClientSide(ICoreClientAPI api) =>
        this.Mod.Logger.Notification("Hello from template mod client side: " + Lang.Get("gleksafkmod:hello"));
}
