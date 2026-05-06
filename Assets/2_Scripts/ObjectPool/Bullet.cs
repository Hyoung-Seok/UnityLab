using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    private IObjectPool<Bullet> _pool;
    
    private Vector3 _dir;
    private float _velocity;

    private float _despawnTime;
    private const float LIFE_TIME = 5f;
    
    public void Init(Vector3 dir, Vector3 pos, float velocity, IObjectPool<Bullet> pool)
    {
        _dir = dir.normalized;
        transform.position = pos;
        _velocity = velocity;
        _pool = pool;

        _despawnTime = Time.time + LIFE_TIME;
    }
    
    private void Update()
    {
        transform.position += _dir * (_velocity * Time.deltaTime);

        if (Time.time >= _despawnTime)
        {
            _pool.Release(this);
        }
    }
}
