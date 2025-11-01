using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Core.Screens;

/// <summary>
/// Base class for all game screens. Each screen represents a state in the game
/// such as Loading, Menu, Splash, Game, or Options.
/// </summary>
public abstract class Screen
{
    /// <summary>
    /// Gets the GraphicsDevice used for rendering.
    /// </summary>
    protected GraphicsDevice GraphicsDevice { get; private set; }

    /// <summary>
    /// Gets the ContentManager used for loading assets.
    /// </summary>
    protected ContentManager Content { get; private set; }

    /// <summary>
    /// Gets the shared SpriteBatch for rendering.
    /// </summary>
    protected SpriteBatch SpriteBatch => ScreenManager?.SpriteBatch;

    /// <summary>
    /// Gets or sets the ScreenManager that manages this screen.
    /// </summary>
    public ScreenManager ScreenManager { get; set; }

    /// <summary>
    /// Gets or sets whether this screen is active and should receive update and draw calls.
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Initializes the screen with required services.
    /// </summary>
    /// <param name="graphicsDevice">The graphics device for rendering.</param>
    /// <param name="content">The content manager for loading assets.</param>
    public virtual void Initialize(GraphicsDevice graphicsDevice, ContentManager content)
    {
        GraphicsDevice = graphicsDevice;
        Content = content;
    }

    /// <summary>
    /// Allows the screen to load content and initialize.
    /// </summary>
    public virtual void LoadContent()
    {
    }

    /// <summary>
    /// Allows the screen to unload content and cleanup.
    /// </summary>
    public virtual void UnloadContent()
    {
    }

    /// <summary>
    /// Updates the screen logic.
    /// </summary>
    /// <param name="gameTime">Provides a snapshot of timing values.</param>
    public abstract void Update(GameTime gameTime);

    /// <summary>
    /// Draws the screen.
    /// </summary>
    /// <param name="gameTime">Provides a snapshot of timing values.</param>
    public abstract void Draw(GameTime gameTime);
}
