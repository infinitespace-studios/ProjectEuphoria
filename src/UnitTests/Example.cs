using System;
using Microsoft.Xna.Framework;
using Euphoria.UnitTests.Fixture;

namespace UnitTests;

[Collection("GraphicsTest")]
public class Example
{
    private readonly GraphicsTestFixture _graphicsFixture;

    public Example(GraphicsTestFixture graphicsFixture)
    {
        _graphicsFixture = graphicsFixture;
    }

    [UIFact]
    public void SampleGraphicsTest()
    {
        // All operations automatically run on the graphics STA thread
        var graphicsDevice = _graphicsFixture.GraphicsDevice;
        var spriteBatch = _graphicsFixture.SpriteBatch;
        Assert.NotNull(graphicsDevice);
        Assert.NotNull(spriteBatch);
    }

    [UIFact]
    public void TestTextureCreation()
    {
        // Texture creation is automatically thread-safe
        var texture = _graphicsFixture.CreatePixelTexture();
        Assert.NotNull(texture);
        Assert.Equal(1, texture.Width);
        Assert.Equal(1, texture.Height);
        texture.Dispose();
    }

    [UIFact]
    public void TestCustomGraphicsOperation()
    {
        // For custom operations, use RunOnGraphicsThread
        var device = _graphicsFixture.GraphicsDevice;
        device.Clear(Color.CornflowerBlue);
        
        // Verify we're on the STA thread
        var apartmentState = Thread.CurrentThread.GetApartmentState();
        Assert.Equal(ApartmentState.STA, apartmentState);
    }

    [UIFact]
    public void TestGraphicsOperationWithReturnValue()
    {
        // Get information from the graphics thread
        var viewportWidth = _graphicsFixture.GraphicsDevice.Viewport.Width;

        Assert.True(viewportWidth > 0);
    }
}
