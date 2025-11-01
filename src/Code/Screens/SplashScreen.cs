using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Core.Screens;

/// <summary>
/// A splash screen that displays at game startup.
/// </summary>
public class SplashScreen : Screen
{
    public override void Update(GameTime gameTime)
    {
        // Add splash screen logic here (e.g., timer to transition to menu)
    }

    public override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.White);

        SpriteBatch.Begin();
        // Add splash screen visuals here
        SpriteBatch.End();
    }
}
