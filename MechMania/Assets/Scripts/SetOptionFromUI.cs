using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

// /https://www.youtube.com/watch?v=6WfowlZ51i8
public class SetOptionFromUI : MonoBehaviour
{
    public Scrollbar volumeSlider;
    public TMPro.TMP_Dropdown songDropdown;

    private void Start()
    {
        volumeSlider.onValueChanged.AddListener(SetGlobalVolume);
        songDropdown.onValueChanged.AddListener(SetStartScene);

        /*if (PlayerPrefs.HasKey("turn"))
            turnDropdown.SetValueWithoutNotify(PlayerPrefs.GetInt("turn"));
        */
    }

    public void SetGlobalVolume(float value)
    {
        AudioListener.volume = value;
    }

    public void SetStartScene(int value)
    {
        Debug.LogWarning("SetStartScene called with value: " + value);
        string strVal = value.ToString();
        //Find start menu script
        GameStartMenu startMenu = FindObjectOfType<GameStartMenu>();
        if (startMenu != null)
        {
            startMenu.ChangeStartScene(strVal);
        }
        else
        {
            Debug.LogWarning("StartMenu script not found in the scene.");
        }
    }
}
