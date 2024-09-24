using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Tile_Engine;
using static System.Net.Mime.MediaTypeNames;

namespace MonoTileGame
{
    public class TextBox
    {
        public string CurrentText { get; set; }
        public Vector2 CurrentTextPosition { get; set; }
        public Vector2 CursorPosition { get; set; }
        public float AnimationTime { get; set; }
        public bool Visible {  get; set; }
        public float LayerDepth { get; set; }
        public Vector2 Position { get; set; }
        public bool Selected { get; set; }
        public bool userEditable { get; set; }
        public int CellWidth { get; set; }
        public int CellHeight { get; set; }
        private int _CursorWidth;
        private int _CursorHeight;
        private int _length;
        private bool _numericOnly;
        private Texture2D _texture;
        private Texture2D _cursorTexture;
        private Point _cursorDimensions;
        private SpriteFont _font;

        public TextBox(Texture2D texture, Texture2D cursorTexture,Point Dimensions,Point cursorDimensions, Vector2 position, int length,  bool user_Editable, bool visible, SpriteFont font, float layerDepth, bool numericOnly = false, string Text = "")
        {
            _texture = texture;
            _cursorTexture = cursorTexture;
            _cursorDimensions = cursorDimensions;
            CellHeight = Dimensions.Y;
            CellWidth = Dimensions.X;
            _numericOnly = numericOnly;
            _length = length;
            userEditable = user_Editable;
            Visible = visible;
            _font = font;
            _CursorWidth = cursorDimensions.X;
            _CursorHeight = cursorDimensions.Y;
            LayerDepth = layerDepth;
            AnimationTime = 0;
            Position = position;
            CursorPosition = new Vector2(position.X+20, position.Y+_CursorHeight/2);
            CurrentTextPosition = new Vector2(position.X + 20, position.Y + _CursorHeight/2);
            CurrentText = Text;
            Selected = false;
            CursorPosition = new Vector2(CursorPosition.X + (_font.MeasureString(CurrentText)).X, CursorPosition.Y);
            
        }

        public void Update(GameTime gameTime)
        {
            AnimationTime += (float)gameTime.ElapsedGameTime.Milliseconds/1000;
            if (AnimationTime > 2 ) AnimationTime %= 2;
        }

        public bool IsFlashingCursotVisible()
        {
            
            if (AnimationTime >= 0 && AnimationTime <1) 
                return true;
            else return false;
        }

        public void AddMoreText(char text)
        {
            Vector2 spacing = new Vector2();
            KeyboardState keyState = OneShotKeyboard.GetState();
            bool lowerThisCharacter = true;

            if (keyState.CapsLock || keyState.IsKeyDown(Keys.LeftShift) || keyState.IsKeyDown(Keys.RightShift)) lowerThisCharacter = false;

            if (_numericOnly && (int)Char.GetNumericValue(text)<0|| (int)Char.GetNumericValue(text) > 9)
            {
                if (text != '\b')
                    return;
            }

            if (text != '\b')
            {
                if (CurrentText.Length < _length)
                {
                    if (lowerThisCharacter)
                    {
                        text = Char.ToLower(text);
                    }

                    CurrentText += text;
                    spacing = _font.MeasureString(text.ToString());
                    CursorPosition = new Vector2(CursorPosition.X + spacing.X,CursorPosition.Y);
                }
            }
            else
            { 
                if (CurrentText.Length >0)
                {
                    spacing = _font.MeasureString(CurrentText.Substring(CurrentText.Length - 1));

                    CurrentText = CurrentText.Remove(CurrentText.Length - 1, 1);
                    CursorPosition = new Vector2(CursorPosition.X-spacing.X,CursorPosition.Y);
                }
            }
        }

        public string InsertNewText(string text)
        {
            string substring;
            string output = "";
            if (text.Length<= _length)
            {
                substring = text;
                
            }
            else
            {
                substring = text.Substring(0, _length);
                output = text.Substring(_length);
            }

            CursorPosition = new Vector2(CursorPosition.X - (_font.MeasureString(CurrentText)).X, CursorPosition.Y);
            CursorPosition = new Vector2(CursorPosition.X + (_font.MeasureString(text)).X, CursorPosition.Y);
            CurrentText = substring;
            return output;
        }

        public void Render(SpriteBatch spriteBatch)
        {
            if (Visible)
            {
                spriteBatch.Draw(_texture,new Rectangle((int)Position.X,(int)Position.Y, CellWidth,CellHeight),Color.White);
                spriteBatch.DrawString(_font, CurrentText, CurrentTextPosition, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, LayerDepth);

                if(Selected && IsFlashingCursotVisible())
                {
                    Rectangle SourceRectangle = new Rectangle(0,0,_CursorWidth, _CursorHeight);
                    Rectangle DestinationRectangle = new Rectangle((int)CursorPosition.X,(int)CursorPosition.Y,_CursorWidth,_CursorHeight);

                    spriteBatch.Draw(_cursorTexture, DestinationRectangle, SourceRectangle, Color.White,0f, Vector2.Zero,SpriteEffects.None, LayerDepth);
                }

            }
        }
    }


    public class Logger
    {
        TextBox[] logs;
        int length;

        public Logger(int BottomLeftCornoerX, int BottomLeftConrnerY, int width, int height,int buffer, SpriteFont font,int Length)
        {
            logs = new TextBox[(BottomLeftConrnerY-buffer)/height];
            length = Length;

            for (int i = 0;i < logs.Length;i++)
            {
                logs[i] = new TextBox(LoadLibrary.Instance.GetTexture("logBackground"), LoadLibrary.Instance.GetTexture("TextCursor"),
                                            new Point(width, 50),
                                            new Point(5, 30), new Vector2(BottomLeftCornoerX,BottomLeftConrnerY-height*(i+1)),
                                            length, true, true, font, 0);
            }
        }

        public void Render(SpriteBatch spriteBatch)
        {
            foreach (TextBox log in logs) log.Render(spriteBatch);
        }

        public void PushNewText(string Text)
        {
            while (Text.Length > 0)
            {
                for (int i = logs.Length-1;i>0;i--)
                {
                    logs[i].InsertNewText(logs[i-1].CurrentText);
                }
                if (Text.Length < length)
                {
                    logs[0].InsertNewText(Text);
                    Text = "";
                }
                else
                {
                    logs[0].InsertNewText(Text.Substring(0,length));
                    Text = Text.Substring(length);
                }
            }
        }
        
    }
}
