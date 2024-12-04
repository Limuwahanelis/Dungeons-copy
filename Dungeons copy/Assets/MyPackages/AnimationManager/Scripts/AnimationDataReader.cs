using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationDataReader : MonoBehaviour
{
    public AnimationDurationList animList;
    public float GetAnimationLength(string name, int layer = 0)
    {
        if (name == "Empty") return 0;
        float clipDuration = 0;
        AnimationData animData = animList.animations.Find(x => x.name == name && x.layer == layer);
        if (animData == null) return 0.1f;
        else return animData.duration;
    }
}
