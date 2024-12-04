using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AudioSettings : MonoBehaviour
{
    [SerializeField] Slider _masterVolumeSlider;
    [SerializeField] Slider _BGMVolumeSlider;
    [SerializeField] Slider _sfxVolumeSlider;
    public void SetMasterVolumee(float volume)=> AudioVolumes.SetMasterVolume(((int)volume));
    public void SetBGMVolume(float volume) => AudioVolumes.SetBGMVolume(((int)volume));
    public void SetSfxVolume(float volume) =>AudioVolumes.SetSFXVolume(((int)volume));

    private void OnEnable()
    {
        _masterVolumeSlider.value = AudioVolumes.Master;
        _BGMVolumeSlider.value = AudioVolumes.BGM;
        _sfxVolumeSlider.value = AudioVolumes.SFX;
    }
}
