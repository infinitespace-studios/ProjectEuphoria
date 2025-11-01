using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Core.Screens;

/// <summary>
/// A menu screen that displays the main menu of the game.
/// </summary>
public class MenuScreen : Screen
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
        // Add menu input handling here
    }

    public override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.DarkBlue);

        _spriteBatch.Begin();
        // Add menu visuals here
        _spriteBatch.End();
    }
}
