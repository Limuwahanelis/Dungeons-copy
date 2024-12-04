using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct DamageInfo
{
    [Flags]
    public enum DamageType
    {
        NONE = 0,
        ENEMY = 1,
        MISSILE = 2,
        TRAPS = 4,
        SHADOW_SPIKE = 8,
        PLAYER = 16,
        BOSS = 32,
        ALL = ~0,
    }

    public int dmg;
    public Vector3 dmgPosition;
    public Collider2D[] involvedColliders;
    public DamageInfo(int dmg,Vector3 dmgPosition,Collider2D[] colliders=null) 
    {
        this.dmg = dmg;
        this.dmgPosition = dmgPosition;
        involvedColliders = colliders;
    }
}
