using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Core.Screens;

/// <summary>
/// An options/settings screen for configuring game settings.
/// </summary>
public class OptionsScreen : Screen
{
    private KeyboardState _previousKeyboardState;

    public override void Update(GameTime gameTime)
    {
        // Don't process input if we're transitioning off
        if (TransitionState == Transitions.TransitionState.TransitionOff)
            return;

        var keyboardState = Keyboard.GetState();

        // Press Escape or O to close options screen
        if ((keyboardState.IsKeyDown(Keys.Escape) && !_previousKeyboardState.IsKeyDown(Keys.Escape)) ||
            (keyboardState.IsKeyDown(Keys.O) && !_previousKeyboardState.IsKeyDown(Keys.O)))
        {
            ScreenManager.RemoveScreen(this);
        }

        _previousKeyboardState = keyboardState;
    }

    public override void Draw(GameTime gameTime)
    {
        // Don't clear when this is a popup - let the underlying screen show through
        if (!IsPopup)
        {
            GraphicsDevice.Clear(Color.DarkGray);
        }

        SpriteBatch.Begin();
        
        // Draw a semi-transparent dialog box in the center
        var viewport = GraphicsDevice.Viewport;
        int dialogWidth = 400;
        int dialogHeight = 300;
        int dialogX = (viewport.Width - dialogWidth) / 2;
        int dialogY = (viewport.Height - dialogHeight) / 2;
        
        // Apply transition alpha to the dialog
        var dialogColor = new Color(60, 60, 60) * TransitionAlpha;
        
        // Draw dialog background
        var blankTexture = ScreenManager?.BlankTexture;
        if (blankTexture != null)
        {
            SpriteBatch.Draw(blankTexture, 
                new Rectangle(dialogX, dialogY, dialogWidth, dialogHeight), 
                dialogColor);
        }
        
        SpriteBatch.End();
    }
}
