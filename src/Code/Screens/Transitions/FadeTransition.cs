using Microsoft.Xna.Framework;

namespace Core.Screens.Transitions;

/// <summary>
/// A simple fade transition that fades the screen in or out.
/// </summary>
public class FadeTransition : ScreenTransition
{
    /// <summary>
    /// Initializes a new instance of the FadeTransition class.
    /// </summary>
    /// <param name="duration">Duration of the fade in seconds (default: 0.5).</param>
    public FadeTransition(float duration = 0.5f) : base(duration)
    {
    }

    /// <summary>
    /// Gets the alpha value for the fade effect.
    /// </summary>
    public override float GetAlpha()
    {
        // When transitioning on (Direction = 1), alpha goes from 0 to 1
        // When transitioning off (Direction = -1), alpha goes from 1 to 0
        return Direction > 0 ? Position : 1f - Position;
    }
}
