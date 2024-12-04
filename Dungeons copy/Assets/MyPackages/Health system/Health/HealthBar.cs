using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class HealthBar : MonoBehaviour
{
    public abstract void Initialize();
    public abstract void SetHealth(int hp);
    public abstract void SetMaxHealth(int value);
    public abstract void ReduceHP(int value);
}
