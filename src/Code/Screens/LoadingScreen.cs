using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Core.Screens;

/// <summary>
/// A loading screen that displays while content is being loaded.
/// </summary>
public class LoadingScreen : Screen
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
        // Add loading logic here
    }

    public override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        _spriteBatch.Begin();
        // Add loading screen visuals here
        _spriteBatch.End();
    }
}
