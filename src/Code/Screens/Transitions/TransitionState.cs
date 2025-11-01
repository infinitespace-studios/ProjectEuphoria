namespace Core.Screens.Transitions;

/// <summary>
/// Represents the current state of a screen transition.
/// </summary>
public enum TransitionState
{
    /// <summary>
    /// The screen is transitioning on (becoming visible).
    /// </summary>
    TransitionOn,

    /// <summary>
    /// The screen is active and fully visible.
    /// </summary>
    Active,

    /// <summary>
    /// The screen is transitioning off (becoming hidden).
    /// </summary>
    TransitionOff,

    /// <summary>
    /// The screen has finished transitioning and is hidden.
    /// </summary>
    Hidden
}
