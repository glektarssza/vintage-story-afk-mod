namespace Glek.VintageStory.AFKMod.Config;

using System.Collections.Generic;

using Glek.VintageStory.AFKMod.API;

using Vintagestory.API.Common;
using Vintagestory.API.Datastructures;
using Vintagestory.API.Util;

/// <summary>
/// A configuration manager.
/// </summary>
/// <typeparam name="TModSystem">
/// The type of the mod-specific mod system.
/// </typeparam>
public sealed class ConfigManager<TModSystem> where TModSystem : ModSpecificModSystem {
    #region Private Properties

    /// <summary>
    /// The mod system for this instance.
    /// </summary>
    public Dictionary<EnumAppSide, string> RegisteredConfigFileNames {
        get;
    }

    #endregion

    #region Public Properties

    /// <summary>
    /// The mod system for this instance.
    /// </summary>
    public TModSystem ModSystem {
        get;
    }

    #endregion

    #region Constructors/Finalizer

    /// <summary>
    /// Create a new instance.
    /// </summary>
    /// <param name="modSystem">
    /// The mod system that will own the new instance.
    /// </param>
    public ConfigManager(TModSystem modSystem) {
        this.ModSystem = modSystem;
        this.RegisteredConfigFileNames = [];
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Register a sided configuration file.
    /// </summary>
    /// <param name="side">
    /// The side (client or server) for which to register the configuration file.
    /// </param>
    /// <param name="fileName">
    /// The file name to attach to the sided configuration.
    /// </param>
    public void RegisterSidedConfig(EnumAppSide side, string fileName) =>
        this.RegisteredConfigFileNames.Add(side, fileName);

    /// <summary>
    /// Load a configuration file by name.
    /// </summary>
    /// <param name="fileName">
    /// The file name to load.
    /// </param>
    /// <returns>
    /// The loaded JSON object or <c>null</c> if the file does not exist.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if the <see cref="this.ModSystem.API" /> property is <c>null</c>.
    /// </exception>
    public JsonObject? LoadConfigFile(string fileName) =>
        this.ModSystem.API == null
            ? throw new InvalidOperationException("Common mod API is a null object")
            : this.ModSystem.API.LoadModConfig(fileName);

    /// <summary>
    /// Load a configuration file by name.
    /// </summary>
    /// <typeparam name="TConfig">
    /// The type of the configuration object to load.
    /// </typeparam>
    /// <param name="fileName">
    /// The file name to load.
    /// </param>
    /// <returns>
    /// The loaded configuration object or <c>null</c> if the file does not exist.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if the <see cref="this.ModSystem.API" /> property is <c>null</c>.
    /// </exception>
    public TConfig? LoadConfigFile<TConfig>(string fileName) =>
        this.ModSystem.API == null
            ? throw new InvalidOperationException("Common mod API is a null object")
            : this.ModSystem.API.LoadModConfig<TConfig>(fileName);

    /// <summary>
    /// Load a configuration file by side (client or server).
    /// </summary>
    /// <param name="side">
    /// The sided configuration to load.
    /// </param>
    /// <returns>
    /// The loaded JSON object or <c>null</c> if the configuration does not exist.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if the <see cref="this.ModSystem.API" /> property is <c>null</c>.
    /// </exception>
    public JsonObject? LoadSidedConfig(EnumAppSide side) =>
        this.RegisteredConfigFileNames.TryGetValue(side, out var fileName)
            ? this.LoadConfigFile(fileName)
            : throw new KeyNotFoundException($"No registered configuration file for app side '{side}'");

    /// <summary>
    /// Load a configuration file by side (client or server).
    /// </summary>
    /// <typeparam name="TConfig">
    /// The type of the configuration object to load.
    /// </typeparam>
    /// <param name="side">
    /// The sided configuration to load.
    /// </param>
    /// <returns>
    /// The loaded configuration object or <c>null</c> if the file does not exist.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if the <see cref="this.ModSystem.API" /> property is <c>null</c>.
    /// </exception>
    public TConfig? LoadSidedConfig<TConfig>(EnumAppSide side) =>
        this.RegisteredConfigFileNames.TryGetValue(side, out var fileName)
            ? this.LoadConfigFile<TConfig>(fileName)
            : throw new KeyNotFoundException($"No registered configuration file for app side '{side}'");

    #endregion
}
