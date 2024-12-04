using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using SaveSystem;

public class ScreenSetUp : MonoBehaviour
{
    public static List<ScreenSettings.MyResolution> resolutions = new List<ScreenSettings.MyResolution>() {
        new ScreenSettings.MyResolution(800,600),
        new ScreenSettings.MyResolution(1024,768),
        new ScreenSettings.MyResolution(1280,1024),
        new ScreenSettings.MyResolution(1366,768),
        new ScreenSettings.MyResolution(1600,900),
        new ScreenSettings.MyResolution(1280,800),
        new ScreenSettings.MyResolution(1440,900),
        new ScreenSettings.MyResolution(1680,1050),
        new ScreenSettings.MyResolution(1920,1080),
    };
    public static List<int> framerates = new List<int>() { -1, 30, 60, 144 };
    private void Awake()
    {

        if (ScreenSettingsSaver.LoadScreenSettings()==null)
        {
            SortAllResolutions();
            Debug.Log("org res: " + resolutions[resolutions.Count - 1]);
            ScreenSettingsData screenData = new ScreenSettingsData(resolutions[0],false,0,0,false,-1);
            ScreenSettingsSaver.SaveScreenSettings(screenData);
            Screen.SetResolution(resolutions[0].width, resolutions[0].height, false);
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = -1;
        }
        else
        {
            ScreenSettingsData configs = ScreenSettingsSaver.LoadScreenSettings();
            Screen.SetResolution(configs.resolution.width,configs.resolution.height,configs.fullScreen);
            if (configs.VSync) QualitySettings.vSyncCount = configs.VSyncCountIndex;
            else QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = configs.targetFrameRate;
        }
    }
    void SortAllResolutions()
    {
        for (int i = 0; i < resolutions.Count; i++)
        {
            resolutions.Sort((r1, r2) => r1.height.CompareTo(r2.height));
            resolutions.Sort((r1, r2) => r1.width.CompareTo(r2.width));
        }
    }
}
