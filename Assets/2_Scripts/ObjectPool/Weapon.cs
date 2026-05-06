using System;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private BulletData loadedBullet;
    [SerializeField] private float fireDelay;
    
    private BulletSpawner _bulletSpawner;
    private float _lastFireTime = float.NegativeInfinity;

    private void Start()
    {
        _bulletSpawner = BulletSpawner.Instance;
        SwapBullet(loadedBullet);
    }

    public void Fire()
    {
        if (Time.time - _lastFireTime < fireDelay) return;
        
        var bullet = BulletSpawner.Instance.GetBullet(loadedBullet);
        bullet.Init(loadedBullet, transform.forward, transform.position);
        
        _lastFireTime = Time.time;
    }

    public void SwapBullet(BulletData bullet)
    {
        loadedBullet = bullet;
    }
}
