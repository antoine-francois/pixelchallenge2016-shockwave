﻿using UnityEngine;
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

    void Update()
    {
        int iCurrentPlayer = PlayerManager.Instance._iCurrentPlayer;

        PlayerState eState = PlayerManager.Instance._tPlayers[iCurrentPlayer]._eState;
        if( eState == PlayerState.ChargeShockwave ) {
            return;
        }

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

        GamePadState tState = PlayerManager.Instance._tPlayers[iCurrentPlayer]._tState;

        bool bFirstShot = ( PlayerManager.Instance.TurnCount == 1 && eState == PlayerState.Play );

        float fMoveX = ( eState != PlayerState.Intro ) ? Joystick.GetAxis( XInputKey.LStickX, tState ) : 0f;
        float fMoveY = ( eState != PlayerState.Intro && !bFirstShot ) ? Joystick.GetAxis( XInputKey.LStickY, tState ) : 0f;

        Vector3 right = transform.right;
        right.y = 0.0f;
        right.Normalize();
        Vector3 forward = -transform.forward;
        forward.y = 0.0f;
        forward.Normalize();

        _tTarget.Translate( (right * fMoveX + forward * fMoveY) * _fMoveSpeed * Time.deltaTime, Space.World );

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
}
