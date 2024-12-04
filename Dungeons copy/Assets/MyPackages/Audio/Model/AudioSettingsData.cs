using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSettingsData
{
    public int masterVolume;
    public int BGMVolume;
    public int sfxVolume;

    public AudioSettingsData(int masterVolume, int bgmVolume,int sfxVolume)
    {
        this.masterVolume = masterVolume;
        this.BGMVolume = bgmVolume;
        this.sfxVolume = sfxVolume;
    }
}
