using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SaveSystem
{
    [System.Serializable]
    public class ScreenSettingsData
    {
        public int VSyncCountIndex=0;
        public int targetFrameRateIndex = 0;
        public int targetFrameRate;
        public bool VSync = false;
        public ScreenSettings.MyResolution resolution;
        public bool fullScreen;
        public ScreenSettingsData(ScreenSettings.MyResolution resolution, bool fullScreen,int VSyncCountIndex,int targetFrameRateIndex,bool VSync,int targetFrameRate)
        {
            this.resolution = resolution;
            this.fullScreen = fullScreen;
            this.VSyncCountIndex = VSyncCountIndex;
            this.targetFrameRateIndex = targetFrameRateIndex;
            this.VSync = VSync;
            this.targetFrameRate = targetFrameRate;
        }
    }
}
