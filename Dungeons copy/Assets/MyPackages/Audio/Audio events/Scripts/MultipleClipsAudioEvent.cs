using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Audio Event/MultipleClipsEvent")]
public class MultipleClipsAudioEvent : AudioEvent
{
    [Range(0, 1)]
    private float _volume = 1f;
    [Range(0, 2)]
    private float _pitch = 1f;
    public AudioClip[] audioclips;
    public bool canOverride;
    public override void Play(AudioSource audioSource)
    {
        audioSource.clip = audioclips[Random.Range(0, audioclips.Length)];
        audioSource.volume = _volume*(AudioVolumes.Master / 100.0f) * (AudioVolumes.SFX / 100.0f);
        if (audioSource.isPlaying)
        {
            if(!canOverride) return;
        }
            
        audioSource.Play();
    }
}