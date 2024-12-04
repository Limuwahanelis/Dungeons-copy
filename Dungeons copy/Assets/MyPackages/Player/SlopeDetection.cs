using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlopeDetection : MonoBehaviour
{
    [SerializeField] PlayerChecks _playerChecks;
    [SerializeField] float _maxIncline;
    [SerializeField] Transform _mainBody;
    public bool CanWalkOnSlope()
    {
        if (_playerChecks.GroundHit)
        {
            if (Vector2.SignedAngle(_mainBody.up, _playerChecks.GroundHit.normal.normalized) > _maxIncline) return false;
            else return true;
        }
        return true;
    }
}
