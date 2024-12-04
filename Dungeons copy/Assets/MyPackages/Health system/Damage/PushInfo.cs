using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushInfo
{
    public Vector3 pushPosition;
    public Collider2D[] involvedColliders;

    public PushInfo(Vector3 pushpos, Collider2D[] involvedColliders=null) 
    {
        this.pushPosition = pushpos;
        this.involvedColliders = involvedColliders;
    }
}
