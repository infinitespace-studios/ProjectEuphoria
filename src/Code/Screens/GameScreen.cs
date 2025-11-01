using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Core.Screens.Transitions;

namespace Core.Screens;

/// <summary>
/// The main game screen where gameplay occurs.
/// </summary>
public class GameScreen : Screen
{
    private KeyboardState _previousKeyboardState;

    public override void Update(GameTime gameTime)
    {
        // Don't process input if we're transitioning off
        if (TransitionState == Transitions.TransitionState.TransitionOff)
            return;

        var keyboardState = Keyboard.GetState();

        // Press M to return to menu with crossfade
        if (keyboardState.IsKeyDown(Keys.M) && !_previousKeyboardState.IsKeyDown(Keys.M))
        {
            var menuScreen = new MenuScreen { Transition = new CrossFadeTransition(0.75f) };
            ScreenManager.TransitionTo(menuScreen);
        }

        _previousKeyboardState = keyboardState;
    }

    public override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        SpriteBatch.Begin();
        // Apply transition alpha
        var tintColor = Color.White * TransitionAlpha;
        // Game rendering would go here with proper alpha
        SpriteBatch.End();
    }
}
