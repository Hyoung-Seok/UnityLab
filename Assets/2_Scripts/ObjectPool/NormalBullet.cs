using UnityEngine;

public class NormalBullet : Bullet
{
    protected override bool OnHit(GameObject other)
    {
        return true;
    }
}
