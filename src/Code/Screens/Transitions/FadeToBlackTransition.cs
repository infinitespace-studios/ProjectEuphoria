using Microsoft.Xna.Framework;

namespace Core.Screens.Transitions;

/// <summary>
/// A transition that fades to black before fading in the new screen.
/// This is a two-stage transition.
/// </summary>
public class FadeToBlackTransition : ScreenTransition
{
    /// <summary>
    /// Initializes a new instance of the FadeToBlackTransition class.
    /// </summary>
    /// <param name="duration">Total duration of the transition in seconds (default: 1.0).</param>
    public FadeToBlackTransition(float duration = 1.0f) : base(duration)
    {
    }

    /// <summary>
    /// Gets the alpha value for the fade to black effect.
    /// First half fades out to black, second half fades in from black.
    /// </summary>
    public override float GetAlpha()
    {
        if (Direction > 0)
        {
            // Transitioning on: fade in from black
            return Position;
        }
        else
        {
            // Transitioning off: fade out to black
            return 1f - Position;
        }
    }

    /// <summary>
    /// Gets the black overlay alpha for the fade to black effect.
    /// </summary>
    /// <returns>Alpha value for the black overlay.</returns>
    public float GetBlackAlpha()
    {
        if (Direction > 0)
        {
            // Transitioning on: black overlay fades out
            return 1f - Position;
        }
        else
        {
            // Transitioning off: black overlay fades in
            return Position;
        }
    }
}
