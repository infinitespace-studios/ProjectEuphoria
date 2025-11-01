using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Core.Screens;

/// <summary>
/// The main game screen where gameplay occurs.
/// </summary>
public class GameScreen : Screen
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
        // Add game logic here
    }

    public override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();
        // Add game rendering here
        _spriteBatch.End();
    }
}
