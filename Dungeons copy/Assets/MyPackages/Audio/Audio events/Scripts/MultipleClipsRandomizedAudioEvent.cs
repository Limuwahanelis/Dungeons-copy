using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Audio Event/MultipleRandomizedClipsEvent")]
public class MultipleClipsRandomizedAudioEvent : AudioEvent
{

    [MinMaxRange(0, 1),SerializeField]
    private RangedFloat _volume;
    [MinMaxRange(0, 2),SerializeField]
    private RangedFloat _pitch;
    [SerializeField] AudioClip[] _audioclips;
    private bool canOverride;
    public override void Play(AudioSource audioSource)
    {
        audioSource.clip = _audioclips[Random.Range(0, _audioclips.Length)];
        float volumef = Random.Range(_volume.minValue, _volume.maxValue);
        audioSource.volume = volumef * (AudioVolumes.Master / 100.0f) * (AudioVolumes.SFX / 100.0f);
        audioSource.pitch = Random.Range(_pitch.minValue, _pitch.maxValue);
        if (audioSource.isPlaying)
        {
            if (!canOverride) return;
        }

        audioSource.Play();
    }
    //public override void Play(AudioSource audioSource, bool overPlay = false)
    //{
    //    audioSource.clip = audioClip;
    //    audioSource.volume = volume * (AudioVolumes.Master / 100.0f) * (AudioVolumes.SFX / 100.0f);
    //    audioSource.pitch = pitch;
    //    if (!overPlay) return;
    //    audioSource.Play();
    //}
    public override void Preview(AudioSource audioSource, float masterVol, float multVol)
    {
        if (_audioclips.Length == 0) Logger.Error("No clips to preview");
        audioSource.clip = _audioclips[Random.Range(0, _audioclips.Length)];
        float volumef = Random.Range(_volume.minValue, _volume.maxValue);
        audioSource.volume = volumef * (AudioVolumes.Master / 100.0f) * (AudioVolumes.SFX / 100.0f);
        audioSource.pitch = Random.Range(_pitch.minValue, _pitch.maxValue);
        audioSource.Play();
    }
}
