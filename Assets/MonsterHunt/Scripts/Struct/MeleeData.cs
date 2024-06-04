using System;

[Serializable]
public struct MeleeData
{
    public float attackDelay;
    public float attackSpeed;

    public MeleeData Clone()
    {
        return (MeleeData)MemberwiseClone();
    }
}