using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Core.Screens;

/// <summary>
/// An options/settings screen for configuring game settings.
/// </summary>
public class OptionsScreen : Screen
{
    public override void Update(GameTime gameTime)
    {
        // Add options screen input handling here
    }

    public override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.DarkGray);

        SpriteBatch.Begin();
        // Add options screen visuals here
        SpriteBatch.End();
    }
}
