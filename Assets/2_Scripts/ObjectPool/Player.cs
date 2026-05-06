using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private Weapon equippedWeapon;
    [SerializeField] private List<BulletData> inventory;

    private int _curInventoryIndex = 0;
    private GameInput _gameInput;
    private Camera _camera;
    private Plane _aimPlane;

    private void Awake()
    {
        _camera = Camera.main;
        _aimPlane = new Plane(Vector3.up, transform.position.y);
        
        _gameInput = new GameInput();
        
        equippedWeapon.SwapBullet(inventory[_curInventoryIndex]);
    }

    private void Update()
    {
        var ray = _camera.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (_aimPlane.Raycast(ray, out var dist) == true)
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

    private void OnEnable()
    {
        _gameInput.Player.Enable();
        
        _gameInput.Player.Fire.performed += OnStartFire;
        _gameInput.Player.Reload.started += OnStartReload;
    }

    private void OnDisable()
    {
        _gameInput.Player.Disable();
        
        _gameInput.Player.Fire.performed -= OnStartFire;
        _gameInput.Player.Reload.started -= OnStartReload;
    }

    private void OnDestroy()
    {
        _gameInput.Dispose();
    }

    private void OnStartFire(InputAction.CallbackContext _)
    {
        equippedWeapon.Fire();
    }

    private void OnStartReload(InputAction.CallbackContext _)
    {
        _curInventoryIndex = (_curInventoryIndex + 1 >= inventory.Count) ? 0 : _curInventoryIndex + 1;
        equippedWeapon.SwapBullet(inventory[_curInventoryIndex]);
    }
}
