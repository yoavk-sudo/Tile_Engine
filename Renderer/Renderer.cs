using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Renderer;

public class Renderer : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    Texture2D _BakgroundTexture;
    Texture2D _TileTexture;

    // Target Resolution
    private readonly int _resolutionWidth = 1920;
    private readonly int _resolutionHeight = 1080;

    // Actual Render Resolution

    private int _virtualWidth = 1920;
    private int _virtualHeight = 1080;

    private bool _isResizing = false;

    private Matrix _screenScaleMatrix;
    private Viewport _viewport;

    public Renderer()
    {
        _graphics = new GraphicsDeviceManager(this);
        _graphics.PreferredBackBufferWidth = _resolutionWidth;
        _graphics.PreferredBackBufferHeight = _resolutionHeight;
        _graphics.ApplyChanges();
        Content.RootDirectory = "Content";
        IsMouseVisible = true;

        Window.AllowUserResizing = true;
        Window.ClientSizeChanged += OnClientSizeChanged;
    }

    private void OnClientSizeChanged(object sender, EventArgs e)
    {
        if (!_isResizing && Window.ClientBounds.Width > 0 && Window.ClientBounds.Height > 0)
        {
            _isResizing = true;
            UpdateScreenScaleMatrix();
            _isResizing = false;
        }
    }

    private void UpdateScreenScaleMatrix()
    {
        float screenWidth = GraphicsDevice.PresentationParameters.BackBufferWidth;
        float screenHeight = GraphicsDevice.PresentationParameters.BackBufferHeight;

        if (screenWidth / _resolutionWidth > screenHeight / _resolutionHeight)
        {
            float aspect = screenHeight / _resolutionHeight;
            _virtualWidth = (int)(aspect * _resolutionWidth);
            _virtualHeight = (int)screenHeight;
        }
        else
        {
            float aspect = screenWidth / _resolutionWidth;
            _virtualWidth = (int)screenWidth;
            _virtualHeight = (int)(aspect * _resolutionHeight);
        }

        _screenScaleMatrix = Matrix.CreateScale(_virtualWidth / (float)_resolutionWidth);

        _viewport = new Viewport
        {
            X = (int)(screenWidth / 2 - _virtualWidth / 2),
            Y = (int)(screenHeight / 2 - _virtualHeight / 2),
            Width = _virtualWidth,
            Height = _virtualHeight,
            MinDepth = 0,
            MaxDepth = 1
        };
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        UpdateScreenScaleMatrix();
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _BakgroundTexture = Content.Load<Texture2D>("Fallen Angel Presentation Background");
        _TileTexture = Content.Load<Texture2D>("Ball");
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        GraphicsDevice.Viewport = _viewport;

        HandleBackground();

        HandleTiles();

        HandleTileHighlights();

        HandleGamePieces();

        base.Draw(gameTime);
    }


    protected void HandleBackground()
    {
        _spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: _screenScaleMatrix);
        _spriteBatch.Draw(_BakgroundTexture, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), Color.White);
        _spriteBatch.End();
    }

    protected void HandleTiles()
    {
        // throw new NotImplementedException();
    }

    protected void HandleTileHighlights()
    {
        // throw new NotImplementedException();
    }

    protected void HandleGamePieces()
    {
        // throw new NotImplementedException();
    }


}
