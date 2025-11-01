using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Core.Screens;

/// <summary>
/// A loading screen that displays while content is being loaded.
/// </summary>
public class LoadingScreen : Screen
{
    public override void Update(GameTime gameTime)
    {
        // Add loading logic here
    }

    public override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        SpriteBatch.Begin();
        // Add loading screen visuals here
        SpriteBatch.End();
    }
}
