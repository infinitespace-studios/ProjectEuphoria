using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Euphoria.UnitTests.Fixture;

/// <summary>
/// A test fixture that provides a fully initialized MonoGame environment
/// for unit testing graphics-related functionality in headless CI environments.
/// The fixture ensures all graphics operations run on a dedicated STA thread.
/// 
/// <para><strong>Integration with Tests:</strong></para>
/// <para>
/// This fixture automatically manages the graphics thread. You can use it in two ways:
/// </para>
/// 
/// <para><strong>1. Direct Access (Automatic Thread Safety):</strong></para>
/// <code>
/// [Collection("GraphicsTest")]
/// public class MyTests
/// {
///     private readonly GraphicsTestFixture _fixture;
///     
///     public MyTests(GraphicsTestFixture fixture) => _fixture = fixture;
///     
///     [Fact]
///     public void Test()
///     {
///         // Properties and helper methods are already thread-safe
///         var device = _fixture.GraphicsDevice;
///         var texture = _fixture.CreatePixelTexture();
///     }
/// }
/// </code>
/// </summary>
[UISettings]
public sealed class GraphicsTestFixture : IDisposable
{
    private readonly Game? _game;
    private readonly SpriteBatch? _spriteBatch;
    
    public Game Game => _game ?? throw new InvalidOperationException("Game not initialized");
    public GraphicsDevice GraphicsDevice => Game.GraphicsDevice;
    public SpriteBatch SpriteBatch => _spriteBatch ?? throw new InvalidOperationException("SpriteBatch not initialized");

    public GraphicsTestFixture()
    {
        _game = new TestGame();

        // Initialize the game completely
        Game.RunOneFrame();

        // Ensure we have a valid graphics device
        if (Game.GraphicsDevice == null)
        {
            throw new InvalidOperationException("Failed to initialize GraphicsDevice. This may indicate an issue with the headless environment setup.");
        }

        Console.WriteLine("===================");
        Console.WriteLine("Graphics Adapter: " + GraphicsDevice.Adapter.Description);
        Console.WriteLine("Thread: " + Thread.CurrentThread.Name + " (ApartmentState: " + Thread.CurrentThread.GetApartmentState() + ")");
        Console.WriteLine("===================");

        // Create commonly used graphics objects for tests
        _spriteBatch = new SpriteBatch(_game.GraphicsDevice);
    
    }

    public void Dispose()
    {
        _spriteBatch?.Dispose();
        _game?.Dispose();
    }

    /// <summary>
    /// Creates a test texture with the specified dimensions and color.
    /// Useful for setting up test scenarios.
    /// </summary>
    public Texture2D CreateTestTexture(int width, int height, Color color)
    {
        Texture2D texture = new Texture2D(GraphicsDevice, width, height);
        Color[] data = new Color[width * height];
        Array.Fill(data, color);
        texture.SetData(data);
        return texture;
    }

    /// <summary>
    /// Creates a simple 1x1 white pixel texture for testing.
    /// </summary>
    public Texture2D CreatePixelTexture() => CreateTestTexture(1, 1, Color.White);

    /// <summary>
    /// Validates that the graphics device is properly initialized and functional.
    /// </summary>
    public void AssertGraphicsDeviceIsValid()
    {
        Assert.NotNull(GraphicsDevice);
        Assert.True(GraphicsDevice.Viewport.Width > 0);
        Assert.True(GraphicsDevice.Viewport.Height > 0);
        Assert.NotNull(GraphicsDevice.PresentationParameters);
    }

    private sealed class TestGame : Game
    {
        public GraphicsDeviceManager GraphicsDeviceManager { get; }

        public TestGame()
        {
            GraphicsDeviceManager = new GraphicsDeviceManager(this);

            // Configure for headless testing
            GraphicsDeviceManager.GraphicsProfile = GraphicsProfile.Reach;
            GraphicsDeviceManager.PreferredBackBufferWidth = 800;
            GraphicsDeviceManager.PreferredBackBufferHeight = 600;

            // Don't sync with vertical retrace for faster test execution
            GraphicsDeviceManager.SynchronizeWithVerticalRetrace = false;

            // On Windows, explicitly prefer WARP adapter for headless CI environments
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                // Force selection of WARP (software renderer) adapter
                // This is done before initialization to ensure WARP is used in headless environments
                GraphicsDeviceManager.PreparingDeviceSettings += OnPreparingDeviceSettings;
            }
        }

        private void OnPreparingDeviceSettings(object? sender, PreparingDeviceSettingsEventArgs e)
        {
            // Try to find the WARP adapter (Microsoft Basic Render Driver)
            GraphicsAdapter? warpAdapter = null;

            foreach (GraphicsAdapter adapter in GraphicsAdapter.Adapters)
            {
                if (adapter.Description.Contains("Microsoft Basic Render Driver") ||
                    adapter.Description.Contains("WARP"))
                {
                    warpAdapter = adapter;
                    break;
                }
            }

            // If WARP adapter is found, use it
            if (warpAdapter != null)
            {
                e.GraphicsDeviceInformation.Adapter = warpAdapter;
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            // Minimal draw for testing
            GraphicsDevice.Clear(Color.CornflowerBlue);
            base.Draw(gameTime);
        }
    }
}