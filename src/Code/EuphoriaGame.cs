using Core.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Core;

public class EuphoriaGame : Game
{
    private readonly GraphicsDeviceManager _graphics;
    private ScreenManager _screenManager;

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

        base.Initialize();
    }

    protected override void LoadContent()
    {
        // Add an initial screen - start with MenuScreen to demonstrate transitions
        _screenManager.AddScreen(new MenuScreen());
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // Update the screen manager
        _screenManager?.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        // Draw the screen manager (screens handle their own clearing)
        _screenManager?.Draw(gameTime);

        base.Draw(gameTime);
    }
}
