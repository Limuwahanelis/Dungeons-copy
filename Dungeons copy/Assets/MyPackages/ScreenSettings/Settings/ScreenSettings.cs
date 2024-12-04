using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using UnityEngine.UI;
using System;
using System.Linq;
using SaveSystem;
public class ScreenSettings : MonoBehaviour
{
    [Serializable]
    // normal Resolution struct is non serializable, so i made my own
    public class MyResolution
    {

        public MyResolution(Resolution res)
        {
            width = res.width;
            height = res.height;
        }
        public MyResolution(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        public int width;
        public int height;
        public static bool operator !=(MyResolution res1, MyResolution res2)
        {
            if (res1 == res2) return true;
            return false;
        }
        public static bool operator ==(MyResolution res1, MyResolution res2)
        {
            if (res1.width != res2.width) return false;
            if (res1.height != res2.height) return false;
            return true;
        }
        public static Resolution GetNormalResolution(MyResolution res)
        {
            Resolution resolution = new Resolution()
            { height = res.height, width = res.width };
            return resolution;
        }

        public override bool Equals(object obj)
        {
            return obj is Resolution resolution &&
                   width == resolution.width &&
                   height == resolution.height;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
    public MyResolution SelectedResolution => _selectedResolution;
    public int TargetFrameRate => _targetFrameRate;
    public int TargetFrameRateIndex => _targetFrameRateIndex;
    public int VSyncCountIndex => _VsyncCount;
    public bool Vsync => _VSync;
    public bool FullScreen => _useFullScreen;
    [SerializeField] TMP_Dropdown _resolutionDropdown;
    [SerializeField] TMP_Dropdown _frameratesDropdown;
    [SerializeField] Toggle _fullscreenToggle;
    [SerializeField] Toggle _VSyncToggle;
    [SerializeField] Slider _VSyncCountSlider;
    private MyResolution _selectedResolution;
    private bool _useFullScreen = false;
    private int _targetFrameRate;
    private int _targetFrameRateIndex;
    private bool _VSync = false;
    private int _VsyncCount;
    private int _currentResIndex;
    private void OnEnable()
    {
        _resolutionDropdown.options.Clear();
        _frameratesDropdown.options.Clear();
        _targetFrameRateIndex = 0;
        _currentResIndex = 0;
        ScreenSettingsData configs = ScreenSettingsSaver.LoadScreenSettings();
        if (configs != null)
        {
            _targetFrameRateIndex = configs.targetFrameRateIndex;
            _targetFrameRate = configs.targetFrameRate;
            _currentResIndex = ScreenSetUp.resolutions.FindIndex(x => x.width == configs.resolution.width && x.height == configs.resolution.height);
            _fullscreenToggle.isOn = configs.fullScreen;
            if (configs.VSync)
            {
                _VSyncCountSlider.value = configs.VSyncCountIndex;
                _VSyncCountSlider.SetValueWithoutNotify(configs.VSyncCountIndex);
                _VSyncToggle.isOn = configs.VSync;
            }
        }

        List<string> resolutionOptions = new List<string>();
        List<string> frameratesOptions = new List<string>();
        for (int i = 0; i < ScreenSetUp.resolutions.Count; i++)
        {
            resolutionOptions.Add(ScreenSetUp.resolutions[i].width + " x " + ScreenSetUp.resolutions[i].height);
        }
        _resolutionDropdown.AddOptions(resolutionOptions);
        _resolutionDropdown.value = _currentResIndex;

        frameratesOptions.Add("No");
        
        for (int i = 1; i < ScreenSetUp.framerates.Count; i++)
        {
            frameratesOptions.Add(ScreenSetUp.framerates[i].ToString());
        }
        _frameratesDropdown.AddOptions(frameratesOptions);
        _frameratesDropdown.value = _targetFrameRateIndex;

    }

    public void SetFullscreen(bool isFullscreen)
    {
        _useFullScreen = isFullscreen;
        Screen.fullScreen = isFullscreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        _currentResIndex = resolutionIndex;
        _selectedResolution = ScreenSetUp.resolutions[_currentResIndex];
        Screen.SetResolution(_selectedResolution.width, _selectedResolution.height, _useFullScreen);
    }
    public void SetFramerate(int index)
    {
        _targetFrameRateIndex = index;
        _targetFrameRate = ScreenSetUp.framerates[_targetFrameRateIndex];
        Logger.Log(ScreenSetUp.framerates[_targetFrameRateIndex]);
        Application.targetFrameRate = ScreenSetUp.framerates[_targetFrameRateIndex];
    }
    public void SetVsync(bool value)
    {
        _VSync = value;
        QualitySettings.vSyncCount = value ? _VsyncCount : 0;
    }
    public void SetVsyncCount(float value)
    {
        int val = (int)value;
        _VsyncCount = val;
        QualitySettings.vSyncCount = _VsyncCount;
    }
}
