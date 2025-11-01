using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Core.Screens;

/// <summary>
/// A splash screen that displays at game startup.
/// </summary>
public class SplashScreen : Screen
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
        // Add splash screen logic here (e.g., timer to transition to menu)
    }

    public override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.White);

        _spriteBatch.Begin();
        // Add splash screen visuals here
        _spriteBatch.End();
    }
}
