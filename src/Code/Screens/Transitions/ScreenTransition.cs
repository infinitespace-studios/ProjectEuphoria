using Microsoft.Xna.Framework;

namespace Core.Screens.Transitions;

/// <summary>
/// Base class for screen transitions that defines how screens appear and disappear.
/// </summary>
public abstract class ScreenTransition
{
    private const int TRANSITION_ON = 1;
    private const int TRANSITION_OFF = -1;

    /// <summary>
    /// Gets the duration of the transition in seconds.
    /// </summary>
    public float Duration { get; protected set; }

    /// <summary>
    /// Gets the current position in the transition (0.0 to 1.0).
    /// </summary>
    public float Position { get; protected set; }

    /// <summary>
    /// Gets the direction of the transition (1 for on, -1 for off).
    /// </summary>
    protected int Direction { get; private set; }

    /// <summary>
    /// Gets whether the transition has completed.
    /// </summary>
    public bool IsComplete => Position >= 1.0f;

    /// <summary>
    /// Initializes a new instance of the ScreenTransition class.
    /// </summary>
    /// <param name="duration">Duration of the transition in seconds.</param>
    protected ScreenTransition(float duration)
    {
        Duration = duration;
        Position = 0f;
        Direction = TRANSITION_ON;
    }

    /// <summary>
    /// Starts the transition in the specified direction.
    /// </summary>
    /// <param name="transitionOn">True to transition on, false to transition off.</param>
    public void Start(bool transitionOn)
    {
        Direction = transitionOn ? TRANSITION_ON : TRANSITION_OFF;
        
        // Set initial position based on direction, but preserve current position if already transitioning
        if (Position > 0f && Position < 1f)
        {
            // Already mid-transition, just reverse direction and keep current position
            return;
        }
        
        // Starting fresh transition
        Position = transitionOn ? 0f : 1f;
    }

    /// <summary>
    /// Updates the transition position.
    /// </summary>
    /// <param name="gameTime">Provides a snapshot of timing values.</param>
    public virtual void Update(GameTime gameTime)
    {
        if (IsComplete && Direction == TRANSITION_ON)
            return;

        if (Position <= 0f && Direction == TRANSITION_OFF)
            return;

        float delta = (float)gameTime.ElapsedGameTime.TotalSeconds / Duration;
        Position += delta * Direction;
        Position = MathHelper.Clamp(Position, 0f, 1f);
    }

    /// <summary>
    /// Gets the alpha value for the screen based on the current transition position.
    /// </summary>
    /// <returns>Alpha value from 0.0 (transparent) to 1.0 (opaque).</returns>
    public abstract float GetAlpha();

    /// <summary>
    /// Gets the scale value for the screen based on the current transition position.
    /// </summary>
    /// <returns>Scale value where 1.0 is normal size.</returns>
    public virtual float GetScale()
    {
        return 1.0f;
    }
}
