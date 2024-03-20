using UnityEngine;
using UnityEngine.Serialization;

public class BulletBox : DropObject
{
    public BulletType bulletType;
    public int count;

    public BulletBox(BulletType bulletType, int count)
    {
        this.bulletType = bulletType;
        this.count = count;
    }
}