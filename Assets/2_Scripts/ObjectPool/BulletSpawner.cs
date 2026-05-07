using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BulletSpawner : MonoBehaviour
{
    public static BulletSpawner Instance { get; private set; }
    private Dictionary<BulletData, IObjectPool<Bullet>> _pool;
    
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
            return;
        }
        
        _pool = new Dictionary<BulletData, IObjectPool<Bullet>>();
    }

    public Bullet GetBullet(BulletData data)
    {
        var pool = GetOrCreateBullet(data);
        return pool.Get();
    }
    
    private IObjectPool<Bullet> GetOrCreateBullet(BulletData data)
    {
        if(_pool.TryGetValue(data, out var bulletPool))
        {
            return bulletPool;
        }
        
        IObjectPool<Bullet> pool = null;
        
        pool = new ObjectPool<Bullet>(
            createFunc: () => 
            {
                var obj = Instantiate(data.BulletPrefab, transform);
                obj.SetPool(pool);
                return obj;
            },
            OnGetBullet,
            OnReleaseBullet,
            OnDestroyBullet,
            defaultCapacity: 10,
            maxSize: 100
            );
        
        _pool.Add(data, pool);
        return pool;
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
