namespace GlekTarssza.VintageStory.AFKMod;

using Vintagestory.API.Common;

/// <summary>
/// The main mod system integration class.
/// </summary>
public class AFKModSystem : ModSystem {
    /// <inheritdoc />
    public override void StartPre(ICoreAPI api) {
        base.StartPre(api);
        api.RegisterEntityBehaviorClass("afk", typeof(PlayerAFKBehavior));
    }
}
