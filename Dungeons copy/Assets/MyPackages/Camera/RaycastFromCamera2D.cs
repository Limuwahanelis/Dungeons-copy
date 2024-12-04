using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastFromCamera2D : MonoBehaviour
{
    public static Vector3 CameraInWorldPos => _cameraInWorldPos;
    [SerializeField] Camera _cam;
    [SerializeField] float _OverlapCircleRadius;
    private static Vector3 _cameraInWorldPos;
    // Start is called before the first frame update
    void Start()
    {
        if (_cam == null) _cam = Camera.main;
    }

    private void Update()
    {
        _cameraInWorldPos = _cam.ScreenPointToRay(HelperClass.MousePos).origin;
    }
    public Collider2D OverlapPoint(out Vector3 point, LayerMask mask)
    {
        Ray ray = _cam.ScreenPointToRay(HelperClass.MousePos);
        point = ray.origin;

        return Physics2D.OverlapPoint(ray.origin, mask);
    }
    public Collider2D[] OverlapCircleAll(out Vector3 point,LayerMask mask)
    {
        Ray ray = _cam.ScreenPointToRay(HelperClass.MousePos);
        point = ray.origin;

        return Physics2D.OverlapCircleAll(ray.origin, _OverlapCircleRadius, mask);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, _OverlapCircleRadius);
    }
}
