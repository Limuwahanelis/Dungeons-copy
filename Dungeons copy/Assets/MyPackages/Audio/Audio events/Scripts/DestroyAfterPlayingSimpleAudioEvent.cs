using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Audio Event/SingleClipDestroyEvent")]
public class DestroyAfterPlayingSimpleAudioEvent : AudioEvent
{
    [Range(0, 1)]
    private float _volume = 1f;
    [Range(0, 2)]
    private float _pitch = 1f;
    public AudioClip audioClip;
    public override void Play(AudioSource audioSource)
    {
        audioSource.clip = audioClip;
        audioSource.volume = _volume * (AudioVolumes.Master / 100.0f) * (AudioVolumes.SFX / 100.0f);
        audioSource.pitch = _pitch;
        if (audioSource.isPlaying) return;
        audioSource.Play();
    }
    public override void Preview(AudioSource audioSource, float masterVol, float multVol)
    {
        audioSource.clip = audioClip;
        audioSource.volume = _volume * (masterVol / 100.0f) * (multVol / 100.0f);
        audioSource.pitch = _pitch;
        if (audioSource.isPlaying) return;
        audioSource.Play();
    }

}
