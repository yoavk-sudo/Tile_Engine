using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoTileGame
{
    public class OneShotMouseButtons
    {
        static MouseState currentMouseState;
        static MouseState lastMouseState;

        public OneShotMouseButtons()
        {

        }

        public static MouseState GetState()
        {
            lastMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();
            return currentMouseState;
        }

        public static bool IsPressed(bool left)
        {
            if (left)
            {
                return currentMouseState.LeftButton == ButtonState.Pressed;
            }
            else
            {
                return currentMouseState.RightButton == ButtonState.Pressed;
            }
        }

        public static bool HasNotBeenPressed(bool left)
        {
            if (left)
            {
                return currentMouseState.LeftButton == ButtonState.Pressed && !(lastMouseState.LeftButton == ButtonState.Pressed);
            }
            else
            {
                return currentMouseState.RightButton == ButtonState.Pressed && !(lastMouseState.RightButton == ButtonState.Pressed);
            }
        }
    }
}
