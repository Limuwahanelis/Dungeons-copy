using SaveSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenSettingsSaver : MonoBehaviour
{
    public static readonly string screenSettingsFileName = "screenConfigs";
    [SerializeField] ScreenSettings _screenSettings;

    public void SaveScreenSettings()
    {
        ScreenSettingsData screenData = new ScreenSettingsData(_screenSettings.SelectedResolution, _screenSettings.FullScreen,_screenSettings.VSyncCountIndex,_screenSettings.TargetFrameRateIndex,_screenSettings.Vsync,_screenSettings.TargetFrameRate);
        JsonSave.SaveToFile(screenData, screenSettingsFileName);
        //SaveSystem.SaveScreenSettings.SaveScreenConfigs(_screenSettings.selectedResolution, _screenSettings.fullScreen);
    }
    public static ScreenSettingsData LoadScreenSettings()
    {
        return JsonSave.GetDataFromJson<ScreenSettingsData>(screenSettingsFileName);
    }
    public static void SaveScreenSettings(ScreenSettingsData screenData)
    {
        JsonSave.SaveToFile(screenData, screenSettingsFileName);
    }
}
