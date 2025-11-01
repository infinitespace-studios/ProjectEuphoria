using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Core.Screens;

/// <summary>
/// An options/settings screen for configuring game settings.
/// </summary>
public class OptionsScreen : Screen
{
    private SpriteBatch _spriteBatch;

    public override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        base.LoadContent();
    }

    public override void UnloadContent()
    {
        _spriteBatch?.Dispose();
        base.UnloadContent();
    }

    public override void Update(GameTime gameTime)
    {
        // Add options screen input handling here
    }

    public override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.DarkGray);

        _spriteBatch.Begin();
        // Add options screen visuals here
        _spriteBatch.End();
    }
}
