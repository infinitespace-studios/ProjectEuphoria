using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Core.Screens;

/// <summary>
/// A menu screen that displays the main menu of the game.
/// </summary>
public class MenuScreen : Screen
{
    public override void Update(GameTime gameTime)
    {
        // Add menu input handling here
    }

    public override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.DarkBlue);

        SpriteBatch.Begin();
        // Add menu visuals here
        SpriteBatch.End();
    }
}
