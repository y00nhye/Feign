using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenSizeSet : MonoBehaviour
{
    FullScreenMode screenMode;
    List<Resolution> resolutions = new List<Resolution>();
    public Toggle fullscreenBtn;
    public Dropdown resolutionDropdown;
    int resolutionNum;
    int beforeResolutionNum = -1;

    private void Start()
    {
        InitUI();
    }

    void InitUI()
    {
        for (int i = 0; i < Screen.resolutions.Length; i++)
        {
            if (Screen.resolutions[i].refreshRate == 60)
            {
                resolutions.Add(Screen.resolutions[i]);
            }
        }

        resolutionDropdown.options.Clear();

        for (int i = 0; i < resolutions.Count; i++)
        {
            Dropdown.OptionData option = new Dropdown.OptionData();
            option.text = resolutions[i].width + " x " + resolutions[i].height;
            resolutionDropdown.options.Add(option);

            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                resolutionDropdown.value = i;
            }
        }

        resolutionDropdown.RefreshShownValue();

        fullscreenBtn.isOn = Screen.fullScreenMode.Equals(FullScreenMode.FullScreenWindow) ? true : false;
    }

    public void DropboxOptionChange(int x)
    {
        if (beforeResolutionNum == -1)
        {
            beforeResolutionNum = resolutionDropdown.value;
        }

        resolutionNum = x;
    }

    public void FullScreenBtn(bool isFull)
    {
        screenMode = isFull ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;
    }

    public void OKBtnClick()
    {
        Screen.SetResolution(resolutions[resolutionNum].width, resolutions[resolutionNum].height, screenMode);
    }

    public void CancleBtnClick()
    {
        resolutionDropdown.value = beforeResolutionNum;
    }
}
