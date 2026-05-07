using UnityEngine;

[CreateAssetMenu(fileName = "BulletData", menuName = "Weapon/BulletData")]
public class BulletData : ScriptableObject
{
    public float Damage => damage;
    public float Velocity => velocity;
    public float LifeTime => lifeTime;
    public Bullet BulletPrefab => bulletPrefab;

    [SerializeField] private float damage;
    [SerializeField] private float velocity;
    [SerializeField] private float lifeTime;
    [SerializeField] private Bullet bulletPrefab;
}
