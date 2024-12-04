using SaveSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSettingsSaver : MonoBehaviour
{
    public static readonly string audioSettingsFileName = "audioConfigs";
    public void SaveAudioSettings()
    {
        AudioSettingsData audioData = new AudioSettingsData(AudioVolumes.Master,AudioVolumes.BGM,AudioVolumes.SFX);
        JsonSave.SaveToFile(audioData, audioSettingsFileName);
    }
    public static AudioSettingsData LoadAudioSettings()
    {
        return JsonSave.GetDataFromJson<AudioSettingsData>(audioSettingsFileName);
    }
    public static void SaveAudioSettings(AudioSettingsData screenData)
    {
        JsonSave.SaveToFile(screenData, audioSettingsFileName);
    }
}
