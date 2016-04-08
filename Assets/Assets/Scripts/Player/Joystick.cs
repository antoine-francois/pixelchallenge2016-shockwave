using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using XInputDotNetPure;

public enum XInputKey
{
    A,
    B,
    X,
    Y,
    Start,
    Back,
    LB,
    RB,
    LT,
    RT,
    LThumb,
    RThumb,
    DPUp,
    DPDown,
    DPRight,
    DPLeft,
    LStickX,
    LStickY,
    RStickX,
    RStickY
}


public class Joystick
{
    public static float GetAxis( XInputKey eKey, GamePadState tState )
    {
        switch( eKey )
        {
            case XInputKey.LStickX:
                return tState.ThumbSticks.Left.X;
            case XInputKey.LStickY:
                return - tState.ThumbSticks.Left.Y;
            case XInputKey.RStickX:
                return tState.ThumbSticks.Right.X;
            case XInputKey.RStickY:
                return - tState.ThumbSticks.Right.Y;
            case XInputKey.LT:
                return tState.Triggers.Left;
            case XInputKey.RT:
                return tState.Triggers.Right;
        }
        return 0f;
    }

    public static bool GetButtonDown( XInputKey eKey, GamePadState tState, GamePadState tPrevState )
    {
        switch( eKey )
        {
            case XInputKey.A:
                return ( tPrevState.Buttons.A == ButtonState.Released && tState.Buttons.A == ButtonState.Pressed );
            case XInputKey.B:
                return ( tPrevState.Buttons.B == ButtonState.Released && tState.Buttons.B == ButtonState.Pressed );
            case XInputKey.X:
                return ( tPrevState.Buttons.X == ButtonState.Released && tState.Buttons.X == ButtonState.Pressed );
            case XInputKey.Y:
                return ( tPrevState.Buttons.Y == ButtonState.Released && tState.Buttons.Y == ButtonState.Pressed );
            case XInputKey.Start:
                return ( tPrevState.Buttons.Start == ButtonState.Released && tState.Buttons.Start == ButtonState.Pressed );
            case XInputKey.Back:
                return ( tPrevState.Buttons.Back == ButtonState.Released && tState.Buttons.Back == ButtonState.Pressed );
            case XInputKey.LB:
                return ( tPrevState.Buttons.LeftShoulder == ButtonState.Released && tState.Buttons.LeftShoulder == ButtonState.Pressed );
            case XInputKey.RB:
                return ( tPrevState.Buttons.RightShoulder == ButtonState.Released && tState.Buttons.RightShoulder == ButtonState.Pressed );
            case XInputKey.LThumb:
                return ( tPrevState.Buttons.LeftStick == ButtonState.Released && tState.Buttons.LeftStick == ButtonState.Pressed );
            case XInputKey.RThumb:
                return ( tPrevState.Buttons.RightStick == ButtonState.Released && tState.Buttons.RightStick == ButtonState.Pressed );
            case XInputKey.DPUp:
                return ( tPrevState.DPad.Up == ButtonState.Released && tState.DPad.Up == ButtonState.Pressed );
            case XInputKey.DPDown:
                return ( tPrevState.DPad.Down == ButtonState.Released && tState.DPad.Down == ButtonState.Pressed );
            case XInputKey.DPLeft:
                return ( tPrevState.DPad.Left == ButtonState.Released && tState.DPad.Left == ButtonState.Pressed );
            case XInputKey.DPRight:
                return ( tPrevState.DPad.Right == ButtonState.Released && tState.DPad.Right == ButtonState.Pressed );
            case XInputKey.LT:
                return ( tPrevState.Triggers.Left < 0.05f && tState.Triggers.Left > 0.05f );
            case XInputKey.RT:
                return ( tPrevState.Triggers.Right < 0.05f && tState.Triggers.Right > 0.05f );
        }
        return false;
    }

    public static bool GetButton( XInputKey eKey, GamePadState tState )
    {
        switch( eKey )
        {
            case XInputKey.A:
                return ( tState.Buttons.A == ButtonState.Pressed );
            case XInputKey.B:
                return ( tState.Buttons.B == ButtonState.Pressed );
            case XInputKey.X:
                return ( tState.Buttons.X == ButtonState.Pressed );
            case XInputKey.Y:
                return ( tState.Buttons.Y == ButtonState.Pressed );
            case XInputKey.Start:
                return ( tState.Buttons.Start == ButtonState.Pressed );
            case XInputKey.Back:
                return ( tState.Buttons.Back == ButtonState.Pressed );
            case XInputKey.LB:
                return ( tState.Buttons.LeftShoulder == ButtonState.Pressed );
            case XInputKey.RB:
                return ( tState.Buttons.RightShoulder == ButtonState.Pressed );
            case XInputKey.LThumb:
                return ( tState.Buttons.LeftStick == ButtonState.Pressed );
            case XInputKey.RThumb:
                return ( tState.Buttons.RightStick == ButtonState.Pressed );
            case XInputKey.DPUp:
                return ( tState.DPad.Up == ButtonState.Pressed );
            case XInputKey.DPDown:
                return ( tState.DPad.Down == ButtonState.Pressed );
            case XInputKey.DPLeft:
                return ( tState.DPad.Left == ButtonState.Pressed );
            case XInputKey.DPRight:
                return ( tState.DPad.Right == ButtonState.Pressed );
            case XInputKey.LT:
                return ( tState.Triggers.Left > 0.05f );
            case XInputKey.RT:
                return ( tState.Triggers.Right > 0.05f );
        }
        return false;
    }

    public static bool GetButtonUp( XInputKey eKey, GamePadState tState, GamePadState tPrevState )
    {
        switch( eKey )
        {
            case XInputKey.A:
                return ( tPrevState.Buttons.A == ButtonState.Pressed && tState.Buttons.A == ButtonState.Released );
            case XInputKey.B:
                return ( tPrevState.Buttons.B == ButtonState.Pressed && tState.Buttons.B == ButtonState.Released );
            case XInputKey.X:
                return ( tPrevState.Buttons.X == ButtonState.Pressed && tState.Buttons.X == ButtonState.Released );
            case XInputKey.Y:
                return ( tPrevState.Buttons.Y == ButtonState.Pressed && tState.Buttons.Y == ButtonState.Released );
            case XInputKey.Start:
                return ( tPrevState.Buttons.Start == ButtonState.Pressed && tState.Buttons.Start == ButtonState.Released );
            case XInputKey.Back:
                return ( tPrevState.Buttons.Back == ButtonState.Pressed && tState.Buttons.Back == ButtonState.Released );
            case XInputKey.LB:
                return ( tPrevState.Buttons.LeftShoulder == ButtonState.Pressed && tState.Buttons.LeftShoulder == ButtonState.Released );
            case XInputKey.RB:
                return ( tPrevState.Buttons.RightShoulder == ButtonState.Pressed && tState.Buttons.RightShoulder == ButtonState.Released );
            case XInputKey.LThumb:
                return ( tPrevState.Buttons.LeftStick == ButtonState.Pressed && tState.Buttons.LeftStick == ButtonState.Released );
            case XInputKey.RThumb:
                return ( tPrevState.Buttons.RightStick == ButtonState.Pressed && tState.Buttons.RightStick == ButtonState.Released );
            case XInputKey.DPUp:
                return ( tPrevState.DPad.Up == ButtonState.Pressed && tState.DPad.Up == ButtonState.Released );
            case XInputKey.DPDown:
                return ( tPrevState.DPad.Down == ButtonState.Pressed && tState.DPad.Down == ButtonState.Released );
            case XInputKey.DPLeft:
                return ( tPrevState.DPad.Left == ButtonState.Pressed && tState.DPad.Left == ButtonState.Released );
            case XInputKey.DPRight:
                return ( tPrevState.DPad.Right == ButtonState.Pressed && tState.DPad.Right == ButtonState.Released );
            case XInputKey.LT:
                return ( tPrevState.Triggers.Left > 0.05f && tState.Triggers.Left < 0.05f );
            case XInputKey.RT:
                return ( tPrevState.Triggers.Right > 0.05f && tState.Triggers.Right < 0.05f );
        }
        return false;
    }
}
