using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace MonoTileGame
{
    public class OneShotKeyboard
    {
        static KeyboardState currentKeyState;
        static KeyboardState lastKeyState;

        public OneShotKeyboard()
        {
            currentKeyState = Keyboard.GetState();
        }

        public static KeyboardState GetState()
        {
            lastKeyState = currentKeyState;
            currentKeyState = Keyboard.GetState();
            return currentKeyState;
        }

        public static bool IsPressed(Keys key)
        {
            return currentKeyState.IsKeyDown(key);
        }

        public static bool HasNotBeenPressed(Keys key)
        {
            bool isDown = currentKeyState.IsKeyDown(key);
            bool hasBeenDown = lastKeyState.IsKeyDown(key);
            return isDown && !hasBeenDown;
        }
    }
}
