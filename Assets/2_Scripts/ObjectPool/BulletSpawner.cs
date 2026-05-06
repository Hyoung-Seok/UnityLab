using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BulletSpawner : MonoBehaviour
{
    public static BulletSpawner Instance { get; private set; }
    public IObjectPool<Bullet> BulletPool => _pool;

    private Bullet _curBullet;
    private ObjectPool<Bullet> _pool;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddBullet(Bullet bullet)
    {
        _curBullet = bullet;

        _pool = new ObjectPool<Bullet>(
            CreateBullet,
            OnGetBullet,
            OnReleaseBullet,
            OnDestroyBullet,
            collectionCheck: true,
            defaultCapacity: 20,
            maxSize: 100
            );
    }

    private Bullet CreateBullet()
    {
        var obj = Instantiate(_curBullet, transform);
        return obj;
    }

    private void OnGetBullet(Bullet bullet)
    {
        bullet.gameObject.SetActive(true);
    }

    private void OnReleaseBullet(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    private void OnDestroyBullet(Bullet bullet)
    {
        Destroy(bullet.gameObject);
    }
    
}
