using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour
{
    public Transform _tTarget;

    [Header("Move")]
    public float _fMoveSpeed;

    [Header("Distance")]

    public float _fRadialDistance;
    public Vector2 _tDistanceLimit;
    public float _fRadialSpeed;

    [Header("Polar")]

    [Range(0, 180)]
    public float _fPolarAngle;
    public Vector2 _tPolarLimit;
    public float _fPolarSpeed;

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
        if (PlayerManager.Instance._tPlayers[PlayerManager.Instance._iCurrentPlayer]._eState == PlayerState.Play)
        {
            float fMoveX = Joystick.GetAxis("LeftX", 0);
            float fMoveY = Joystick.GetAxis("LeftY", 0);

            Vector3 right = transform.right;
            right.y = 0.0f;
            right.Normalize();
            Vector3 forward = -transform.forward;
            forward.y = 0.0f;
            forward.Normalize();

            _tTarget.Translate((right * fMoveX + forward * fMoveY).normalized * _fMoveSpeed * Time.deltaTime, Space.World);
        }

        float fTurnX = Joystick.GetAxis("RightX", 0) * _fAzimuthSpeed * Time.deltaTime;
        float fTurnY = Joystick.GetAxis("RightY", 0) * _fPolarSpeed * Time.deltaTime;
        float fZoom = (Joystick.GetAxis("RT", 0) - Joystick.GetAxis("LT", 0)) * _fRadialSpeed * Time.deltaTime;

        _fAzimuthAngle += fTurnX;
        _fPolarAngle += fTurnY;
        _fRadialDistance += fZoom;

        _fPolarAngle = Mathf.Clamp(Mathf.Repeat( _fPolarAngle, 180.0f ), _tPolarLimit.x, _tPolarLimit.y);
        _fAzimuthAngle = Mathf.Clamp( Mathf.Repeat( _fAzimuthAngle, 360.0f ), _tAzimuthLimit.x, _tAzimuthLimit.y);
        _fRadialDistance = Mathf.Clamp(_fRadialDistance, _tDistanceLimit.x, _tDistanceLimit.y);

        float polar = Mathf.Deg2Rad * _fPolarAngle;
        float azimuth = Mathf.Deg2Rad * _fAzimuthAngle;

        float x = _fRadialDistance * Mathf.Sin(polar) * Mathf.Cos(azimuth);
        float y = _fRadialDistance * Mathf.Cos(polar);
        float z = _fRadialDistance * Mathf.Sin(polar) * Mathf.Sin(azimuth);

        this.transform.localPosition = new Vector3(x, y, z);

        this.transform.LookAt(_tTarget != null ? _tTarget.position : Vector3.zero, Vector3.up);
    }
}
