using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private Camera _camera;
    private Plane _aimPlane;

    private void Start()
    {
        _camera = Camera.main;
        _aimPlane = new Plane(Vector3.up, transform.position.y);
    }

    private void Update()
    {
        var ray = _camera.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (_aimPlane.Raycast(ray, out var dist))
        {
            var hit = ray.GetPoint(dist);
            var dir = hit - transform.position;
            dir.y = 0;

            if (dir.sqrMagnitude > 0.001f)
            {
                transform.rotation = Quaternion.LookRotation(dir);
            }
        }
    }
}
