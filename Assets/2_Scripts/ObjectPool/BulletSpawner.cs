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
        }
        
        _pool = new Dictionary<BulletData, IObjectPool<Bullet>>();
    }

    public IObjectPool<Bullet> AddBullet(BulletData data)
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
            OnDestroyBullet
            );
        
        _pool.Add(data, pool);
        return pool;
    }

    public Bullet GetBullet(BulletData data)
    {
        var pool = AddBullet(data);
        return pool.Get();
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
