namespace GlekTarssza.VintageStory.AFKMod;

using Vintagestory.API;
using Vintagestory.API.Common.Entities;

/// <summary>
/// An entity behavior that allows players to go AFK.
/// </summary>
[DocumentAsJson]
public class PlayerAFKBehavior : EntityBehavior {
    #region Constructors/Finalizer

    /// <summary>
    /// Create a new instance.
    /// </summary>
    /// <param name="entity">
    /// The entity the new instance will be attached to.
    /// </param>
    public PlayerAFKBehavior(Entity entity) : base(entity) {
        // -- Does nothing
    }

    #endregion

    #region Public Methods

    /// <inheritdoc />
    public override string PropertyName() => "afk";

    #endregion
}
