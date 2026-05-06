using System;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Bullet currentBullet;
    [SerializeField] private float fireDelay;
    
    private BulletSpawner _bulletSpawner;
    private float _lastFireTime = float.NegativeInfinity;

    private void Start()
    {
        _bulletSpawner = BulletSpawner.Instance;
        
        _bulletSpawner.AddBullet(currentBullet);
    }

    public void Fire()
    {
        if (Time.time - _lastFireTime < fireDelay) return;
        
        var bullet = _bulletSpawner.BulletPool.Get();
        bullet.Init(transform.forward, transform.position, 10f, _bulletSpawner.BulletPool);
        
        _lastFireTime = Time.time;
    }
}
