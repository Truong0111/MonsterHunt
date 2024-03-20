using System;

[Serializable]
public struct CharacterData
{
    public short id;
    public string nameCharacter;
    public float maxHealth;
    public float speed;
    public float jumpForce;
    public float armor;
    
    public CharacterData Clone()
    {
        return (CharacterData)MemberwiseClone();
    }
}