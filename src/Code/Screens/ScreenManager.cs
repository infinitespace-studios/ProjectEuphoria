using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Core.Screens.Transitions;

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
    private readonly SpriteBatch _spriteBatch;
    private readonly Texture2D _blankTexture;

    /// <summary>
    /// Initializes a new instance of the ScreenManager class.
    /// </summary>
    /// <param name="graphicsDevice">The graphics device for rendering.</param>
    /// <param name="content">The content manager for loading assets.</param>
    public ScreenManager(GraphicsDevice graphicsDevice, ContentManager content)
    {
        _graphicsDevice = graphicsDevice;
        _content = content;
        _spriteBatch = new SpriteBatch(graphicsDevice);
        
        // Create a 1x1 white texture for tinting and overlays
        _blankTexture = new Texture2D(graphicsDevice, 1, 1);
        _blankTexture.SetData(new[] { Color.White });
    }

    /// <summary>
    /// Gets the shared SpriteBatch for all screens.
    /// </summary>
    public SpriteBatch SpriteBatch => _spriteBatch;

    /// <summary>
    /// Gets a 1x1 white texture that can be used for drawing rectangles and overlays.
    /// </summary>
    public Texture2D BlankTexture => _blankTexture;

    /// <summary>
    /// Adds a screen to the screen manager.
    /// </summary>
    /// <param name="screen">The screen to add.</param>
    public void AddScreen(Screen screen)
    {
        screen.ScreenManager = this;
        screen.Initialize(_graphicsDevice, _content);
        screen.LoadContent();
        
        // Start transition if one is set
        if (screen.Transition != null)
        {
            screen.TransitionState = TransitionState.TransitionOn;
            screen.Transition.Start(true);
        }
        else
        {
            screen.TransitionState = TransitionState.Active;
        }
        
        _screensToAdd.Add(screen);
    }

    /// <summary>
    /// Removes a screen from the screen manager.
    /// </summary>
    /// <param name="screen">The screen to remove.</param>
    public void RemoveScreen(Screen screen)
    {
        // Start transition off if one is set
        if (screen.Transition != null && screen.TransitionState != TransitionState.Hidden)
        {
            screen.TransitionState = TransitionState.TransitionOff;
            screen.Transition.Start(false);
        }
        else
        {
            screen.UnloadContent();
            _screensToRemove.Add(screen);
        }
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
        // Transition out all existing screens
        foreach (var existingScreen in _screens)
        {
            RemoveScreen(existingScreen);
        }
        
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

        // Update all active screens and their transitions
        // Use ToList() to avoid modifying collection during iteration
        foreach (var screen in _screens.ToList())
        {
            // Update transitions
            if (screen.Transition != null)
            {
                screen.Transition.Update(gameTime);
                
                // Update transition state
                if (screen.TransitionState == TransitionState.TransitionOn && screen.Transition.IsComplete)
                {
                    screen.TransitionState = TransitionState.Active;
                }
                else if (screen.TransitionState == TransitionState.TransitionOff && screen.Transition.Position <= 0f)
                {
                    screen.TransitionState = TransitionState.Hidden;
                    screen.UnloadContent();
                    _screensToRemove.Add(screen);
                    continue;
                }
            }
            
            if (screen.IsActive)
            {
                screen.Update(gameTime);
            }
        }
    }

    /// <summary>
    /// Draws all active screens.
    /// </summary>
    /// <param name="gameTime">Provides a snapshot of timing values.</param>
    public void Draw(GameTime gameTime)
    {
        bool hasPopup = false;
        
        // Check if there's a popup screen
        foreach (var screen in _screens)
        {
            if (screen.IsPopup && screen.IsActive)
            {
                hasPopup = true;
                break;
            }
        }
        
        // Draw all screens
        for (int i = 0; i < _screens.Count; i++)
        {
            var screen = _screens[i];
            
            if (screen.IsActive || screen.TransitionState == TransitionState.TransitionOff)
            {
                screen.Draw(gameTime);
            }
        }
        
        // Draw all overlays in a single batch to reduce draw calls
        var viewport = _graphicsDevice.Viewport;
        var fullScreenRect = new Rectangle(0, 0, viewport.Width, viewport.Height);
        bool hasOverlays = false;
        
        // Check if we need to draw any overlays
        if (hasPopup)
        {
            hasOverlays = true;
        }
        else
        {
            foreach (var screen in _screens)
            {
                if (screen.Transition is FadeToBlackTransition fadeToBlack && fadeToBlack.GetBlackAlpha() > 0f)
                {
                    hasOverlays = true;
                    break;
                }
            }
        }
        
        // Only draw overlays if needed
        if (hasOverlays)
        {
            _spriteBatch.Begin();
            
            // Apply tint overlay if there's a popup
            if (hasPopup)
            {
                _spriteBatch.Draw(_blankTexture, fullScreenRect, Color.Black * 0.5f);
            }
            
            // Draw black overlay for FadeToBlack transitions
            foreach (var screen in _screens)
            {
                if (screen.Transition is FadeToBlackTransition fadeToBlack)
                {
                    float blackAlpha = fadeToBlack.GetBlackAlpha();
                    if (blackAlpha > 0f)
                    {
                        _spriteBatch.Draw(_blankTexture, fullScreenRect, Color.Black * blackAlpha);
                    }
                }
            }
            
            _spriteBatch.End();
        }
    }

    /// <summary>
    /// Gets the currently active screens.
    /// </summary>
    public IReadOnlyList<Screen> Screens => _screens.AsReadOnly();
}
