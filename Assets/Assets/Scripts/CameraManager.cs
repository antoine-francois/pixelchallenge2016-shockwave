using UnityEngine;
using System.Collections;
using XInputDotNetPure;
using UnityEngine.UI;

public class CameraManager : MonoBehaviour
{
    public Transform _tTarget;
    public Image _tMark;
    public Color _tP1;
    public Color _tP2;
    public Color _tP3;
    public Color _tP4;

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

    void Start()
    {
    }

    void Update()
    {
        int iCurrentPlayer = PlayerManager.Instance._iCurrentPlayer;

        switch( iCurrentPlayer )
        {
            case 0:
                _tMark.color = _tP1;
                break;
            case 1:
                _tMark.color = _tP2;
                break;
            case 2:
                _tMark.color = _tP3;
                break;
            case 3:
                _tMark.color = _tP4;
                break;
        }

        int iCurrentPad = ( GameSettings._iNbGamepad == 1 ) ? 0 : iCurrentPlayer;

        GamePadState tState = PlayerManager.Instance._tPlayers[iCurrentPlayer]._tState;
        //GamePadState tPrevState = PlayerManager.Instance._tPlayers[iCurrentPlayer]._tPrevState;

        float fMoveX = Joystick.GetAxis( XInputKey.LStickX, tState );
        float fMoveY = Joystick.GetAxis( XInputKey.LStickY, tState );

        Vector3 right = transform.right;
        right.y = 0.0f;
        right.Normalize();
        Vector3 forward = -transform.forward;
        forward.y = 0.0f;
        forward.Normalize();

        _tTarget.Translate((right * fMoveX + forward * fMoveY).normalized * _fMoveSpeed * Time.deltaTime, Space.World);

        float fTurnX = Joystick.GetAxis( XInputKey.RStickX, tState ) * _fAzimuthSpeed * Time.deltaTime;

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
}
