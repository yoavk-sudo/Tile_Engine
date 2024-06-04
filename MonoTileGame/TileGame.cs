using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using Tile_Engine;

namespace MonoTileGame
{
    public class TileGame : Game
    {

        private Color BackgroundColor = Color.Black;
        private bool _UseBackground = false;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        // Target Resolution
        private readonly int _resolutionWidth = 1920;
        private readonly int _resolutionHeight = 1080;

        // Actual Render Resolution

        private int _virtualWidth = 1920;
        private int _virtualHeight = 1080;

        private bool _isResizing = false;

        private Matrix _screenScaleMatrix;
        private Viewport _viewport;

        //Game Data

        private TileMap _tileMap;
        private int TileSize;

        public Action<Tile> MousePressedOnTile = (t) => { return; };
        public event Action<int, int> MousePressedOutsideOfTileMap = (t,s) => { return; };

        public TileGame(TileMap tileMap)
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = _resolutionWidth;
            _graphics.PreferredBackBufferHeight = _resolutionHeight;
            _graphics.ApplyChanges();
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            

            Window.AllowUserResizing = true;
            Window.ClientSizeChanged += OnClientSizeChanged;

            LoadLibrary.Instance.Init(this);

            _tileMap = tileMap;

            
            foreach (Tile T in _tileMap)
            {
                T.InitTexture(new TileRenderer());
                if (!T.IsEmpty)
                {
                    T.TileObject.InitTexture(new TileRenderer());
                }
            }
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
        
        /*
        public void Tint(Tile tile)
        {
            tile.Texture.Tint(new Object());
        }

        */


        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            UpdateScreenScaleMatrix();
            TileSize = Math.Min((int)(_graphics.PreferredBackBufferWidth / TileMap.Map.GetLength(0)), (int)(_graphics.PreferredBackBufferHeight / TileMap.Map.GetLength(1)));
            //MousePressedOnTile += Tint;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);


        }

        protected override void Update(GameTime gameTime)
        {
            MouseState inputData = Mouse.GetState();
            int updatedX = ((inputData.X - _viewport.X) * _resolutionWidth) / _virtualWidth;
            int updatedY = ((inputData.Y - _viewport.Y) * _resolutionHeight) / _virtualHeight;
            if (inputData.LeftButton == ButtonState.Pressed)
            {
                if (updatedX > TileSize * TileMap.Map.GetLength(0) || updatedY > TileSize * TileMap.Map.GetLength(1) || updatedX < 0 || updatedY < 0)
                {
                    MousePressedOutsideOfTileMap.Invoke(updatedX, updatedY);
                }
                else
                {
                    //TileMap.Map[inputData.X / TileSize, inputData.Y / TileSize].Texture.Tint(new Object());
                    MousePressedOnTile.Invoke(TileMap.Map[updatedX / TileSize, updatedY / TileSize]);
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(BackgroundColor);

            GraphicsDevice.Viewport = _viewport;

            TileSize = Math.Min((int)(_graphics.PreferredBackBufferWidth / TileMap.Map.GetLength(0)), (int)(_graphics.PreferredBackBufferHeight / TileMap.Map.GetLength(1)));

            HandleBackground();

            HandleTiles();

            HandleGamePieces();

            base.Draw(gameTime);
        }


        protected void HandleBackground()
        {
            if (_UseBackground)
            {
                _spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: _screenScaleMatrix);
                _spriteBatch.Draw(LoadLibrary.Instance.GetTexture("BackgroundTexture"), new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), Color.White);
                _spriteBatch.End();
            }

        }

        protected void HandleTiles()
        {
            _spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: _screenScaleMatrix);
            for (int x = 0; x < TileMap.Map.GetLength(0); x++)
            {
                for (int y = 0; y < TileMap.Map.GetLength(1); y++)
                {
                    _spriteBatch.Draw(LoadLibrary.Instance.GetTexture(((TileRenderer)TileMap.Map[x, y].Texture).sprite.Key), new Rectangle(x * TileSize, y * TileSize, TileSize, TileSize), ((TileRenderer)TileMap.Map[x, y].Texture).tint);
                }
            }
            _spriteBatch.End();
        }

        protected void HandleGamePieces()
        {
            _spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: _screenScaleMatrix);
            for (int x = 0; x < TileMap.Map.GetLength(0); x++)
            {
                for (int y = 0; y < TileMap.Map.GetLength(1); y++)
                {
                    if (TileMap.Map[x, y].TileObject != null)
                    {
                        _spriteBatch.Draw(
                            LoadLibrary.Instance.GetTexture(((TileRenderer)TileMap.Map[x, y].TileObject.Texture).sprite.Key),
                            new Rectangle(x * TileSize, y * TileSize, TileSize, TileSize),
                            ((TileRenderer)TileMap.Map[x, y].TileObject.Texture).tint);
                    }

                }
            }
            _spriteBatch.End();
        }

        public void UpdateBackground(Color backgraoundColor, bool UseSprite)
        {
            BackgroundColor = backgraoundColor;
            _UseBackground = UseSprite;
        }

        public void UpdateBackground(string SpriteUrl, bool UseSprite)
        {
            LoadLibrary.Instance.UpdateData("BackgroundTexture", SpriteUrl);
            _UseBackground = UseSprite;
        }


    }
}


