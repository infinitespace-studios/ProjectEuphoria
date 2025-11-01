namespace Core.Screens.Transitions;

/// <summary>
/// A direct crossfade transition between screens without an intermediate black screen.
/// </summary>
public class CrossFadeTransition : FadeTransition
{
    /// <summary>
    /// Initializes a new instance of the CrossFadeTransition class.
    /// </summary>
    /// <param name="duration">Duration of the crossfade in seconds (default: 0.75).</param>
    public CrossFadeTransition(float duration = 0.75f) : base(duration)
    {
    }
}
