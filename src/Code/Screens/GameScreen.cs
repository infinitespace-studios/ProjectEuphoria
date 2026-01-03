using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Core.Screens;

/// <summary>
/// The main game screen where gameplay occurs.
/// </summary>
public class GameScreen : Screen
{
    private Model _shipModel;
    public override void LoadContent()
    {
        _shipModel = Content.Load<Model>("Models/ship-small");
    }

    public override void Update(GameTime gameTime)
    {
        // Add game logic here
    }

    public override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _shipModel.Draw(Matrix.CreateRotationY((float)gameTime.TotalGameTime.TotalSeconds), Matrix.CreateLookAt(new Vector3(0, 10, 50), Vector3.Zero, Vector3.Up), Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45f), GraphicsDevice.Viewport.AspectRatio, 1f, 1000f));
    }
}