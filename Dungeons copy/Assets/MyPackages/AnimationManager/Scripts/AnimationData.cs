using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AnimationData
{
    public string name;
    public float duration;
    public float speed;
    public int layer;

    public AnimationData(string name, float duration, float speed, int layer)
    {
        this.name = name;
        this.duration = duration;
        this.speed = speed;
        this.layer = layer;
    }
}
