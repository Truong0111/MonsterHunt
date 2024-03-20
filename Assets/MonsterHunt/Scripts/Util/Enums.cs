using System;

public enum GameState
{
    Menu,
    Shop,
    Setting,
    Lobby,
    InGame,
    Revive,
    EndGame,
    Loading,
    Quit
}

public enum MonsterRole
{
    Creep = 0,
    MediumMonster = 1 << 0,
    Boss = 1 << 1,
}

public enum CharacterAction
{
    Idle,
    Attack,
    Die,
    Victory,
    Move,
    Sprint,
    Crouch,
    Reload
}

[Serializable]
public enum EnemyType
{
    Creep,
    Medium,
    Boss
}

[Serializable][Flags]
public enum EnemyDrop
{
    None = 0,
    Gold = 1 << 2,
    Bullet = 1 << 3,
    HealthItem = 1 << 4,
    Weapon = 4 << 5,
}

public enum CollectibleType
{
    Gold,
    Health
}

public enum LevelResult
{
    NotDecided,
    Win,
    Lose
}

public enum WeaponType
{
    Gun,
    Melee
}

