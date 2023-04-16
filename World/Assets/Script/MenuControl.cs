using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuControl : MonoBehaviour
{
    [SerializeField]
    private GameObject menuContent;

    void Start()
    {
        // ShowMenu(menuContent.activeInHierarchy);
        GameSettings.LoadSettings();
        menuContent.SetActive(true);// для работы find
        ApplySettings();
        ShowMenu(false);
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            ShowMenu(!menuContent.activeInHierarchy);
        }
    }

    private void ShowMenu(bool showMode)
    {
        menuContent.SetActive(showMode);
        Cursor.visible = showMode;
        if (showMode)
        {
            Time.timeScale = 0.0f;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Time.timeScale = 1.0f;
            Cursor.lockState = CursorLockMode.Locked;  // захват курсора - вихід миші за вікно все одно передаватиме дані у це вікно
        }
    }

    private void ApplySettings()
    {
        GameObject.Find("InverseZoomToggle").GetComponent<Toggle>().isOn = GameSettings.MouseZoomInverted;
        GameObject.Find("InverseVerticalToggle").GetComponent<Toggle>().isOn = GameSettings.VerticalInverted;
        GameObject.Find("SensitivitySlider").GetComponent<Slider>().value = GameSettings.Sensitivity;

        GameObject.Find("GameTimerToggle").GetComponent<Toggle>().isOn = GameSettings.GameTimerEnabled;

        GameObject.Find("DifficultyLevelValue").GetComponent<TMPro.TextMeshProUGUI>().text =
            GameSettings.Difficulty.ToString();
        GameObject.Find("CoinCostValue").GetComponent<TMPro.TextMeshProUGUI>().text =
            GameSettings.CoinValue.ToString();


        var board = GameObject.Find("BestBoardText").GetComponent<TMPro.TextMeshProUGUI>();
        board.text = string.Empty;
        for (int i = 0; i < GameSettings.LeaderRecords.Count; i++)
        {
            var item = GameSettings.LeaderRecords[i];
            board.text += $"{i + 1}.{item.Name} -- {item.Score}\n";
        }
    }

    private void UpdateDifficalty()
    {

    }

    public void ExitButtonClick()
    {
        if (UnityEditor.EditorUtility.DisplayDialog("Game will be ended", "Are you realy want exit?", "YES", "NO"))
        {
            Application.Quit();
            UnityEditor.EditorApplication.isPlaying = false;
        }

    }

    public void ResetButtonClick()
    {
        if (UnityEditor.EditorUtility.DisplayDialog("Game will be reset", "Are you realy want to reset all settings defaults?", "YES", "NO"))
        {
            GameSettings.ResetDefaults();
            ApplySettings();
        }
    }

    public void ResumeButtonClick()
    {
        ShowMenu(false);
    }

    #region OnChange Handlers
    public void ZoomInvertedChanged(bool value)
    {
        GameSettings.MouseZoomInverted = value;
    }
    public void VerticalInvertedChanged(bool value)
    {
        GameSettings.VerticalInverted = value;
    }
    public void SensitivityChanged(float value)
    {
        GameSettings.Sensitivity = value;

    }
    public void MusicVolumeChanged(float value)
    {
        GameSettings.MusicVolume = value;

    }

    public void MusicEnabledChanged(bool value)
    {
        GameSettings.MusicEnabled = value;
    }

    public void SoundVolumeChanged(float value)
    {
        GameSettings.EffectsVolume = value;

    }

    public void SoundEnabledChanged(bool value)
    {
        GameSettings.EffectsEnabled = value;
    }
    public void AllSoundsEnabledChanged(bool value)
    {
        GameSettings.EffectsEnabled = value;
    }

    public void DisplayCoinDistanceEnabledChanged(bool value)
    {
        GameSettings.CoinDistanceEnabled = value;
    }

    public void DisplayDirectionHintsEnabledChanged(bool value)
    {
        GameSettings.DirectionHintsEnabled = value;
    }

    public void DisplayRespawnTimerEnabledChanged(bool value)
    {
        GameSettings.GameTimerEnabled = value;
    }
    public void StaminaEnabledChanged(bool value)
    {
        GameSettings.StaminaEnabled = value;
    }

    public void GameTimerEnabledChanged(bool value)
    {
        GameSettings.GameTimerEnabled = value;
        ApplySettings();
    }
    #endregion
}