using Core.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Core;

public class EuphoriaGame : Game
{
    private readonly GraphicsDeviceManager _graphics;
    private ScreenManager _screenManager;
    private RenderTarget2D _renderTarget;

    public EuphoriaGame()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // Initialize the screen manager
        _screenManager = new ScreenManager(GraphicsDevice, Content);

        _renderTarget = new RenderTarget2D(GraphicsDevice, 800, 600);

        base.Initialize();
    }

    protected override void LoadContent()
    {
        // Add an initial screen (e.g., GameScreen)
        _screenManager.AddScreen(new GameScreen());
    }

    protected override void Update(GameTime gameTime)
    {
        var kb = Keyboard.GetState();
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || kb.IsKeyDown(Keys.Escape))
            Exit();

        if (kb.IsKeyDown(Keys.P))
        {
            using (var stream = System.IO.File.Create("screenshot.png"))
            {
                _renderTarget.SaveAsPng(stream, _renderTarget.Width, _renderTarget.Height);
            }
        }

        // Update the screen manager
        _screenManager?.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.SetRenderTarget(_renderTarget);
        GraphicsDevice.Clear(Color.CornflowerBlue);
        // Draw the screen manager (screens handle their own clearing)
        _screenManager?.Draw(gameTime);

        GraphicsDevice.SetRenderTarget(null);
        _screenManager.SpriteBatch.Begin();
        _screenManager.SpriteBatch.Draw(_renderTarget, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), Color.White);
        _screenManager.SpriteBatch.End();

        base.Draw(gameTime);
    }
}
