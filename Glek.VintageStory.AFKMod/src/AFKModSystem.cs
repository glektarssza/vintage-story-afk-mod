namespace Glek.VintageStory.AFKMod;

using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.Config;
using Vintagestory.API.Server;

public class AFKModSystem : ModSystem {
    // Called on server and client
    // Useful for registering block/entity classes on both sides
    public override void Start(ICoreAPI api) =>
        this.Mod.Logger.Notification("Hello from template mod: " + api.Side);

    public override void StartServerSide(ICoreServerAPI api) =>
        this.Mod.Logger.Notification("Hello from template mod server side: " + Lang.Get("gleksafkmod:hello"));

    public override void StartClientSide(ICoreClientAPI api) =>
        this.Mod.Logger.Notification("Hello from template mod client side: " + Lang.Get("gleksafkmod:hello"));
}
