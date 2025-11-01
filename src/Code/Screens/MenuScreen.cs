using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Core.Screens.Transitions;

namespace Core.Screens;

/// <summary>
/// A menu screen that displays the main menu of the game.
/// </summary>
public class MenuScreen : Screen
{
    private KeyboardState _previousKeyboardState;

    public override void LoadContent()
    {
        base.LoadContent();
        // Note: Font would need to be created in Content pipeline
        // For now, we'll work without it
    }

    public override void Update(GameTime gameTime)
    {
        var keyboardState = Keyboard.GetState();

        // Press 1 for instant transition to GameScreen
        if (keyboardState.IsKeyDown(Keys.D1) && !_previousKeyboardState.IsKeyDown(Keys.D1))
        {
            var gameScreen = new GameScreen { Transition = new InstantTransition() };
            ScreenManager.TransitionTo(gameScreen);
        }

        // Press 2 for fade transition to GameScreen
        if (keyboardState.IsKeyDown(Keys.D2) && !_previousKeyboardState.IsKeyDown(Keys.D2))
        {
            var gameScreen = new GameScreen { Transition = new FadeTransition(0.5f) };
            ScreenManager.TransitionTo(gameScreen);
        }

        // Press 3 for crossfade transition to GameScreen
        if (keyboardState.IsKeyDown(Keys.D3) && !_previousKeyboardState.IsKeyDown(Keys.D3))
        {
            var gameScreen = new GameScreen { Transition = new CrossFadeTransition(0.75f) };
            ScreenManager.TransitionTo(gameScreen);
        }

        // Press 4 for fade-to-black transition to GameScreen
        if (keyboardState.IsKeyDown(Keys.D4) && !_previousKeyboardState.IsKeyDown(Keys.D4))
        {
            var gameScreen = new GameScreen { Transition = new FadeToBlackTransition(1.0f) };
            ScreenManager.TransitionTo(gameScreen);
        }

        // Press 5 to show Options screen as popup overlay
        if (keyboardState.IsKeyDown(Keys.D5) && !_previousKeyboardState.IsKeyDown(Keys.D5))
        {
            var optionsScreen = new OptionsScreen 
            { 
                IsPopup = true,
                Transition = new FadeTransition(0.3f)
            };
            ScreenManager.AddScreen(optionsScreen);
        }

        _previousKeyboardState = keyboardState;
    }

    public override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.DarkBlue);

        SpriteBatch.Begin();
        // Apply transition alpha to the screen rendering
        var tintColor = Color.White * TransitionAlpha;
        
        // Draw simple colored rectangles as menu items (since we don't have fonts loaded)
        var viewport = GraphicsDevice.Viewport;
        // Menu items would be drawn here with proper alpha
        SpriteBatch.End();
    }
}
