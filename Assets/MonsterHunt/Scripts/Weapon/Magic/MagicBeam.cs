using System;
using UnityEngine;

public class MagicBeam : MagicAttackObject
{
    public enum BeamType
    {
        None,
        Continuous
    }

    public BeamType beamType;
}