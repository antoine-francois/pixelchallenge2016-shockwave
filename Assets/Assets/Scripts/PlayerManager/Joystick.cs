using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Joystick
{
    static Dictionary<string, string> _tName2Key;

    public static void Init()
    {
        _tName2Key = new Dictionary<string, string>();
        _tName2Key.Add("LeftX", "L_XAxis_");
        _tName2Key.Add("LeftY", "L_YAxis_");
        _tName2Key.Add("LeftStick", "LS_");
        _tName2Key.Add("RightX", "R_XAxis_");
        _tName2Key.Add("RightY", "R_YAxis_");
        _tName2Key.Add("RightStick", "RS_");
        _tName2Key.Add("DPadX", "DPad_XAxis_");
        _tName2Key.Add("DPadY", "DPad_YAxis_");
        _tName2Key.Add("A", "A_");
        _tName2Key.Add("B", "B_");
        _tName2Key.Add("X", "X_");
        _tName2Key.Add("Y", "Y_");
        _tName2Key.Add("Start", "Start_");
        _tName2Key.Add("Back", "Back_");
        _tName2Key.Add("LB", "LB_");
        _tName2Key.Add("RB", "RB_");
        _tName2Key.Add("LT", "TriggersL_");
        _tName2Key.Add("RT", "TriggersR_");
    }

    public static float GetAxis(string name, int id)
    {
        return Input.GetAxis(_tName2Key[name] + ( id + 1 ) );
    }

    public static float GetAxisRaw(string name, int id)
    {
        return Input.GetAxisRaw(_tName2Key[name] + ( id + 1 ) );
    }

    public static bool GetButtonDown(string name, int id)
    {
        return Input.GetButtonDown(_tName2Key[name] + ( id + 1 ) );
    }

    public static bool GetButton(string name, int id)
    {
        return Input.GetButton(_tName2Key[name] + ( id + 1 ) );
    }

    public static bool GetButtonUp(string name, int id)
    {
        return Input.GetButtonUp(_tName2Key[name] + ( id + 1 ) );
    }
}
