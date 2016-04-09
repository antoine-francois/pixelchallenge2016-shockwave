using UnityEngine;
using System;
using System.IO;
using System.Collections;

public class ScreenshotTool : MonoBehaviour
{
    Canvas _tCanvas;

	void Start()
    {
        _tCanvas = GetComponent<Canvas>();
	}

	void LateUpdate()
    {
        if( _tCanvas != null && Input.GetKeyDown( KeyCode.F1 ) )
        {
            _tCanvas.enabled = !_tCanvas.enabled;
        }

        if( Input.GetKeyDown( KeyCode.F2 ) )
        {
            if( !Directory.Exists( Application.persistentDataPath + "/Screenshots" ) ) {
                Directory.CreateDirectory( Application.persistentDataPath + "/Screenshots" );
            }

            Application.CaptureScreenshot( Application.persistentDataPath + "/Screenshots/TableShock_" + DateTime.Now.ToString( "yyyy-MM-dd_HH-mm-ss" ) + ".png" );
        }

        if( Input.GetKeyDown( KeyCode.F3 ) )
        {
            Camera.main.GetComponent<CameraManager>().TogglePhotoMode();
        }
	}
}