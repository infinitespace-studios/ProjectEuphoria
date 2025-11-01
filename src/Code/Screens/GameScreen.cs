using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Core.Screens;

/// <summary>
/// The main game screen where gameplay occurs.
/// </summary>
public class GameScreen : Screen
{
    public override void Update(GameTime gameTime)
    {
        // Add game logic here
    }

    public override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        SpriteBatch.Begin();
        // Add game rendering here
        SpriteBatch.End();
    }
}
