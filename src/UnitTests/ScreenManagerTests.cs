using System;
using Microsoft.Xna.Framework;
using Core;
using Core.Screens;
using Euphoria.UnitTests.Fixture;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace UnitTests;

[Collection("GraphicsTest")]
public class ScreenManagerTests
{
    private readonly GraphicsTestFixture _graphicsFixture;
    private readonly ContentManager _content;

    public ScreenManagerTests(GraphicsTestFixture graphicsFixture)
    {
        _graphicsFixture = graphicsFixture;
        _content = new ContentManager(_graphicsFixture.ServiceProvider, "Content");
    }

    [Fact]
    public void ScreenManager_Creation_ShouldNotThrow()
    {
        GraphicsDevice gd = _graphicsFixture.GraphicsDevice;
        var screenManager = new ScreenManager(gd, _content);
        Assert.NotNull(screenManager);
    }

    [Fact]
    public void ScreenManager_AddSplashScreen_ShouldWork_AfterUpdateIsCalled()
    {
        GraphicsDevice gd = _graphicsFixture.GraphicsDevice;
        var screenManager = new ScreenManager(gd, _content);
        var testScreen = new SplashScreen();
        screenManager.AddScreen(testScreen);
        Assert.DoesNotContain(testScreen, screenManager.Screens);
        screenManager.Update(new GameTime());
        Assert.Contains(testScreen, screenManager.Screens);
    }
}