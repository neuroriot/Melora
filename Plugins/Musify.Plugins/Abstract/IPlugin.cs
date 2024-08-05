﻿namespace Musify.Plugins.Abstract;

/// <summary>
/// Represents a plugin.
/// </summary>
public interface IPlugin
{
    /// <summary>
    /// The name the plugin.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// The path date for the plugin icon.
    /// </summary>
    string IconPathData { get; }


    /// <summary>
    /// The config for the plugin.
    /// </summary>
    IPluginConfig Config { get; }
}