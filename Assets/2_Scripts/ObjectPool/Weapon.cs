using System;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private float fireDelay;
    
    private BulletData _loadedBullet;
    private float _lastFireTime = float.NegativeInfinity;
    

    public void Fire()
    {
        if (Time.time - _lastFireTime < fireDelay) return;
        
        var bullet = BulletSpawner.Instance.GetBullet(_loadedBullet);
        bullet.Init(_loadedBullet, transform.forward, transform.position);
        
        _lastFireTime = Time.time;
    }

    public void SwapBullet(BulletData bullet)
    {
        _loadedBullet = bullet;
        
    }
}
