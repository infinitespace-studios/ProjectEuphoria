using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Core.Screens;

/// <summary>
/// Manages game screens and handles transitions between them.
/// </summary>
public class ScreenManager
{
    private readonly List<Screen> _screens = new();
    private readonly List<Screen> _screensToAdd = new();
    private readonly List<Screen> _screensToRemove = new();
    private readonly GraphicsDevice _graphicsDevice;
    private readonly ContentManager _content;

    /// <summary>
    /// Initializes a new instance of the ScreenManager class.
    /// </summary>
    /// <param name="graphicsDevice">The graphics device for rendering.</param>
    /// <param name="content">The content manager for loading assets.</param>
    public ScreenManager(GraphicsDevice graphicsDevice, ContentManager content)
    {
        _graphicsDevice = graphicsDevice;
        _content = content;
    }

    /// <summary>
    /// Adds a screen to the screen manager.
    /// </summary>
    /// <param name="screen">The screen to add.</param>
    public void AddScreen(Screen screen)
    {
        screen.ScreenManager = this;
        screen.Initialize(_graphicsDevice, _content);
        screen.LoadContent();
        _screensToAdd.Add(screen);
    }

    /// <summary>
    /// Removes a screen from the screen manager.
    /// </summary>
    /// <param name="screen">The screen to remove.</param>
    public void RemoveScreen(Screen screen)
    {
        screen.UnloadContent();
        _screensToRemove.Add(screen);
    }

    /// <summary>
    /// Removes all screens from the screen manager.
    /// </summary>
    public void RemoveAllScreens()
    {
        foreach (var screen in _screens)
        {
            screen.UnloadContent();
        }
        _screensToRemove.AddRange(_screens);
    }

    /// <summary>
    /// Transitions to a new screen, removing all existing screens.
    /// </summary>
    /// <param name="screen">The screen to transition to.</param>
    public void TransitionTo(Screen screen)
    {
        RemoveAllScreens();
        AddScreen(screen);
    }

    /// <summary>
    /// Updates all active screens.
    /// </summary>
    /// <param name="gameTime">Provides a snapshot of timing values.</param>
    public void Update(GameTime gameTime)
    {
        // Process pending additions and removals
        foreach (var screen in _screensToRemove)
        {
            _screens.Remove(screen);
        }
        _screensToRemove.Clear();

        foreach (var screen in _screensToAdd)
        {
            _screens.Add(screen);
        }
        _screensToAdd.Clear();

        // Update all active screens
        for (int i = _screens.Count - 1; i >= 0; i--)
        {
            if (_screens[i].IsActive)
            {
                _screens[i].Update(gameTime);
            }
        }
    }

    /// <summary>
    /// Draws all active screens.
    /// </summary>
    /// <param name="gameTime">Provides a snapshot of timing values.</param>
    public void Draw(GameTime gameTime)
    {
        foreach (var screen in _screens)
        {
            if (screen.IsActive)
            {
                screen.Draw(gameTime);
            }
        }
    }

    /// <summary>
    /// Gets the currently active screens.
    /// </summary>
    public IReadOnlyList<Screen> Screens => _screens.AsReadOnly();
}
