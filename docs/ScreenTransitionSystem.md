# Screen Transition System

This document describes the screen transition system implemented for Project Euphoria.

## Overview

The screen transition system provides flexible transitions between game screens, including:
- Instant transitions (no animation)
- Fade transitions
- Crossfade transitions (direct fade between screens)
- Fade-to-black transitions (fade out to black, then fade in)
- Dialog/popup overlays with background tinting

## Components

### TransitionState Enum
Tracks the current state of a screen transition:
- `TransitionOn` - Screen is becoming visible
- `Active` - Screen is fully visible
- `TransitionOff` - Screen is becoming hidden
- `Hidden` - Screen has finished transitioning and is hidden

### ScreenTransition Base Class
Abstract base class for all transition effects. Handles timing and position tracking.

Key properties:
- `Duration` - How long the transition takes in seconds
- `Position` - Current position (0.0 to 1.0)
- `IsComplete` - Whether the transition has finished

Key methods:
- `Start(bool transitionOn)` - Begins the transition
- `Update(GameTime gameTime)` - Updates transition progress
- `GetAlpha()` - Returns the alpha value for rendering (0.0 to 1.0)

### Transition Implementations

#### InstantTransition
No animation - instant switch between screens.

```csharp
var screen = new GameScreen { Transition = new InstantTransition() };
```

#### FadeTransition
Simple fade in/out effect.

```csharp
var screen = new GameScreen { Transition = new FadeTransition(0.5f) }; // 0.5 second fade
```

#### CrossFadeTransition
Direct crossfade between screens (extends FadeTransition).

```csharp
var screen = new GameScreen { Transition = new CrossFadeTransition(0.75f) }; // 0.75 second crossfade
```

#### FadeToBlackTransition
Two-stage transition that fades to black then fades in the new screen.

```csharp
var screen = new GameScreen { Transition = new FadeToBlackTransition(1.0f) }; // 1 second total (0.5s out, 0.5s in)
```

## Screen Properties

### IsPopup
When `true`, the screen acts as an overlay/dialog on top of other screens. The underlying screens remain visible with a dark tint applied.

```csharp
var optionsScreen = new OptionsScreen 
{ 
    IsPopup = true,
    Transition = new FadeTransition(0.3f)
};
ScreenManager.AddScreen(optionsScreen);
```

### Transition
The transition effect to use when this screen appears/disappears.

```csharp
screen.Transition = new FadeTransition(0.5f);
```

### TransitionAlpha
Read-only property that returns the current alpha value based on the transition state. Use this when rendering to apply transition effects.

```csharp
var tintColor = Color.White * TransitionAlpha;
```

## ScreenManager Methods

### TransitionTo(Screen screen)
Transitions to a new screen, removing all existing screens with their respective transitions.

```csharp
var gameScreen = new GameScreen { Transition = new CrossFadeTransition() };
ScreenManager.TransitionTo(gameScreen);
```

### AddScreen(Screen screen)
Adds a screen to the manager. If the screen has a transition, it will transition on.

```csharp
var popupScreen = new OptionsScreen { IsPopup = true, Transition = new FadeTransition() };
ScreenManager.AddScreen(popupScreen);
```

### RemoveScreen(Screen screen)
Removes a screen. If the screen has a transition, it will transition off before being removed.

```csharp
ScreenManager.RemoveScreen(this);
```

## Usage Examples

### Example 1: Menu to Game with Crossfade
```csharp
var gameScreen = new GameScreen { Transition = new CrossFadeTransition(0.75f) };
ScreenManager.TransitionTo(gameScreen);
```

### Example 2: Show Options Dialog
```csharp
var optionsScreen = new OptionsScreen 
{ 
    IsPopup = true,
    Transition = new FadeTransition(0.3f)
};
ScreenManager.AddScreen(optionsScreen);
```

### Example 3: Dramatic Scene Change with Fade to Black
```csharp
var newLevel = new GameScreen { Transition = new FadeToBlackTransition(1.5f) };
ScreenManager.TransitionTo(newLevel);
```

### Example 4: Instant Switch (Loading Screens, etc.)
```csharp
var loadingScreen = new LoadingScreen { Transition = new InstantTransition() };
ScreenManager.TransitionTo(loadingScreen);
```

## Extensibility

The system is designed to be extensible. To create custom transitions:

1. Create a new class that inherits from `ScreenTransition`
2. Override `GetAlpha()` to control opacity
3. Optionally override `GetScale()` for scale effects
4. Optionally override `Update()` for custom timing behavior

Example custom transition:
```csharp
public class ZoomTransition : ScreenTransition
{
    public ZoomTransition(float duration = 0.5f) : base(duration) { }
    
    public override float GetAlpha()
    {
        return Direction > 0 ? Position : 1f - Position;
    }
    
    public override float GetScale()
    {
        // Zoom in when transitioning on, zoom out when transitioning off
        if (Direction > 0)
            return MathHelper.Lerp(0.5f, 1.0f, Position);
        else
            return MathHelper.Lerp(1.0f, 1.5f, Position);
    }
}
```

## Demo

The MenuScreen includes keyboard shortcuts to demonstrate different transitions:
- Press `1` - Instant transition to GameScreen
- Press `2` - Fade transition to GameScreen
- Press `3` - Crossfade transition to GameScreen
- Press `4` - Fade-to-black transition to GameScreen
- Press `5` - Show Options screen as popup overlay

From GameScreen:
- Press `M` - Return to menu with crossfade

From OptionsScreen (when shown as popup):
- Press `O` or `Escape` - Close options screen
