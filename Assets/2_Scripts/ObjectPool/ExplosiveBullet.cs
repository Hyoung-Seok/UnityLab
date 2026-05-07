using UnityEngine;

public class ExplosiveBullet : Bullet
{
    protected override bool OnHit(GameObject other)
    {
        return true;
    }
}
