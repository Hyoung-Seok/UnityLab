using UnityEngine;
using UnityEngine.Pool;

public abstract class Bullet : MonoBehaviour
{
    protected BulletData BulletData;
    
    private IObjectPool<Bullet> _pool;
    
    private Vector3 _dir;
    private float _curTime = 0f;

    protected abstract void OnHit(GameObject other);
    
    public void Init(BulletData bulletData, Vector3 dir, Vector3 pos)
    {
        BulletData = bulletData;
        _dir = dir;
        _curTime = 0f;
        
        transform.position = pos;
        transform.rotation = Quaternion.LookRotation(dir);
    }

    public void SetPool(IObjectPool<Bullet> pool)
    {
        _pool = pool;    
    }
    
    private void Update()
    {
        transform.position += _dir * (BulletData.Velocity * Time.deltaTime);
        _curTime += Time.deltaTime;

        if (_curTime >= BulletData.LifeTime)
        {
            _pool.Release(this);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            OnHit(other.gameObject);
        }
    }
}
