using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceGame
{
    public class InputManager
    {
        KeyboardState keyState;
        MouseState mouseState;
        GamePadState[] padState;

        KeyboardState lastKeyState;
        MouseState lastMouseState;
        GamePadState[] lastPadState;

        public InputManager()
        {
            padState = new GamePadState[4];
            lastPadState = new GamePadState[4];
        }

        public void Update()
        {
            lastKeyState = keyState;
            lastMouseState = mouseState;
            for (int i = 0; i < 4; i++)
            {
                lastPadState[i] = padState[i];
            }

            keyState = Keyboard.GetState();
            mouseState = Mouse.GetState();

            for (int i = 0; i < 4; i++)
            {
                padState[i] = GamePad.GetState(i);
            }
        }

        public bool IsKeyboardPressed(Keys key)
        {
            if (keyState.IsKeyDown(key) && !lastKeyState.IsKeyDown(key))
                return true;

            return false;
        }

        public bool IsKeyboardHeld(Keys key)
        {
            if (keyState.IsKeyDown(key) && lastKeyState.IsKeyDown(key))
                return true;

            return false;
        }

        public bool IsPadConnected()
        {
            return padState[0].IsConnected;
        }

        public bool IsPadConnected(int index)
        {
            if (index < 0 || index >= 4)
                return false;

            return padState[index].IsConnected;
        }

        public bool IsPadPressed(Buttons button)
        {
            if (padState[0].IsButtonDown(button) && !lastPadState[0].IsButtonDown(button))
                return true;

            return false;
        }

        public bool IsPadPressed(int index, Buttons button)
        {
            if (index < 0 || index >= 4)
                return false;

            if (padState[index].IsButtonDown(button) && !lastPadState[index].IsButtonDown(button))
                return true;

            return false;
        }

        public bool IsPadHeld(Buttons button)
        {
            if (padState[0].IsButtonDown(button) && lastPadState[0].IsButtonDown(button))
                return true;

            return false;
        }

        public bool IsPadHeld(int index, Buttons button)
        {
            if (index < 0 || index >= 4)
                return false;

            if (padState[index].IsButtonDown(button) && lastPadState[index].IsButtonDown(button))
                return true;

            return false;
        }

        // Game Specific
        public bool InputUp()
        {
            if (IsKeyboardHeld(Keys.W) || IsKeyboardHeld(Keys.Up) ||
                IsPadHeld(Buttons.DPadUp) || IsPadHeld(Buttons.LeftThumbstickUp))
                return true;

            return false;
        }

        public bool InputDown()
        {
            if (IsKeyboardHeld(Keys.S) || IsKeyboardHeld(Keys.Down) ||
                IsPadHeld(Buttons.DPadDown) || IsPadHeld(Buttons.LeftThumbstickDown))
                return true;

            return false;
        }

        public bool InputLeftPressed()
        {
            if (IsKeyboardPressed(Keys.A) || IsKeyboardPressed(Keys.Left) ||
                IsPadPressed(Buttons.DPadLeft) || IsPadPressed(Buttons.LeftThumbstickLeft))
                return true;

            return false;
        }

        public bool InputRightPressed()
        {
            if (IsKeyboardPressed(Keys.D) || IsKeyboardPressed(Keys.Right) ||
                IsPadPressed(Buttons.DPadRight) || IsPadPressed(Buttons.LeftThumbstickRight))
                return true;

            return false;
        }

        public bool InputFire()
        {
            if (IsKeyboardHeld(Keys.Space) || IsPadHeld(Buttons.RightTrigger) || IsPadHeld(Buttons.A))
                return true;

            return false;
        }

        public bool InputAccept()
        {
            if (IsKeyboardPressed(Keys.Enter) || IsPadPressed(Buttons.A))
                return true;

            return false;
        }

        public bool InputPause()
        {
            if (IsKeyboardPressed(Keys.Escape) || IsPadPressed(Buttons.Start))
                return true;

            return false;
        }

    }
}
