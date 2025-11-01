namespace Core.Screens.Transitions;

/// <summary>
/// An instant transition with no animation.
/// </summary>
public class InstantTransition : ScreenTransition
{
    /// <summary>
    /// Initializes a new instance of the InstantTransition class.
    /// </summary>
    public InstantTransition() : base(0f)
    {
    }

    /// <summary>
    /// Gets the alpha value, which is always 1.0 for instant transitions.
    /// </summary>
    public override float GetAlpha()
    {
        return 1.0f;
    }
}
