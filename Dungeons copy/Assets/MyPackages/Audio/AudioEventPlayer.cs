using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioEventPlayer : MonoBehaviour
{
    [SerializeField] AudioSource _audioSource;
    private void Reset()
    {
        _audioSource=GetComponent<AudioSource>();
    }
    public void PlayeAudioEvent(AudioEvent audioEvent)
    {
        audioEvent.Play(_audioSource);
    }
}
