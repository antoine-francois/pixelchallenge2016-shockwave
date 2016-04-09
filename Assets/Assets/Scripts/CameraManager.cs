using UnityEngine;
using System.Collections;
using XInputDotNetPure;
using UnityEngine.UI;

public class CameraManager : MonoBehaviour
{
    public Transform _tTarget;
    public Image _tMark;

    private bool _bPhotoMode = false;
    private Vector3 _tSavePos;
    private Quaternion _tSaveRot;

    [Header("Move")]
    public float _fMoveSpeed;

    [Header("Distance")]

    public float _fRadialDistance;

    [Header("Polar")]

    [Range(0, 180)]
    public float _fPolarAngle;

    [Header("Azimuth")]

    [Range(0, 360)]
    public float _fAzimuthAngle;
    public Vector2 _tAzimuthLimit;
    public float _fAzimuthSpeed;

    void Update()
    {
        PlayerColor eCurrentPlayer = PlayerManager.Instance._eCurrentPlayer;

        PlayerState eState = PlayerManager.Instance._tPlayers[eCurrentPlayer]._eState;
        if( eState == PlayerState.ChargeShockwave ) {
            return;
        }

        _tMark.color = GameSettings.Instance._tPlayerColors[eCurrentPlayer];

        GamePadState tState = PlayerManager.Instance._tPlayers[eCurrentPlayer]._tState;

        bool bFirstShot = ( PlayerManager.Instance.TurnCount == 1 && eState == PlayerState.Play );

        float fMoveX = ( eState != PlayerState.Intro ) ? Joystick.GetAxis( XInputKey.LStickX, tState ) : 0f;
        float fMoveY = ( eState != PlayerState.Intro && !bFirstShot ) ? Joystick.GetAxis( XInputKey.LStickY, tState ) : 0f;

        Vector3 right = transform.right;
        right.y = 0.0f;
        right.Normalize();
        Vector3 forward = -transform.forward;
        forward.y = 0.0f;
        forward.Normalize();

        Vector3 tMovement = (right * fMoveX + forward * fMoveY) * _fMoveSpeed * Time.deltaTime;
        _tTarget.Translate( tMovement, Space.World );

        if( !Physics.Raycast( transform.parent.position, Vector3.down, ( 1 << LayerMask.NameToLayer( "Floor" ) ) ) ) {
            _tTarget.Translate( - tMovement, Space.World );
        }

        float fTurnX = ( eState != PlayerState.Intro && !bFirstShot ) ? Joystick.GetAxis( XInputKey.RStickX, tState ) * _fAzimuthSpeed * Time.deltaTime : 0f;

        _fAzimuthAngle += fTurnX;

        _fAzimuthAngle = Mathf.Clamp( Mathf.Repeat( _fAzimuthAngle, 360.0f ), _tAzimuthLimit.x, _tAzimuthLimit.y);

        float polar = Mathf.Deg2Rad * _fPolarAngle;
        float azimuth = Mathf.Deg2Rad * _fAzimuthAngle;

        float x = _fRadialDistance * Mathf.Sin(polar) * Mathf.Cos(azimuth);
        float y = _fRadialDistance * Mathf.Cos(polar);
        float z = _fRadialDistance * Mathf.Sin(polar) * Mathf.Sin(azimuth);

        this.transform.localPosition = new Vector3(x, y, z);

        this.transform.LookAt(_tTarget != null ? _tTarget.position : Vector3.zero, Vector3.up);
    }


    public void TogglePhotoMode()
    {
        _bPhotoMode = !_bPhotoMode;

        if( _bPhotoMode )
        {
            Time.timeScale = 0f;
            _tSavePos = _tTarget.position;
            _tSaveRot = _tTarget.rotation;
        }
        else
        {
            Time.timeScale = 1f;
            _tTarget.position = _tSavePos;
            _tTarget.rotation = _tSaveRot;
        }
    }
}
