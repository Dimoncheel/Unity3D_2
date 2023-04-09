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
            ShowMenu( ! menuContent.activeInHierarchy);            
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
        for(int i = 0; i < GameSettings.LeaderRecords.Count; i++)
        {
            var item=GameSettings.LeaderRecords[i];
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
    public void GameTimerEnabledChanged(bool value)
    {
        GameSettings.GameTimerEnabled = value;
        // після зміни якоїсь частини, що визначають складність, слід перерахувати рівень та вартість монети
        ApplySettings();
    }
    #endregion
    /*
    void OnGUI()
    {
        if (Event.current.isKey && Event.current.type == EventType.KeyDown)
        {
            Debug.Log(Event.current.keyCode);
        }
    } */
}
/* Д.З. Забезпечити адаптивне розміщення блоків ігрового меню (зразок у Teams),
 * їх зміну для екранів з різною роздільною здатністю
 * **почати розміщувати елементи керування
 */
/* Меню паузи
 * Викликається під час гри, після закриття повертаємось у гру
 * Всі налаштування зберігаються, при наступних запусках залишаються
 * попередньо встановлені налаштування
 * 
 * Налаштування управління
 *  - інвертувати колесо (зум камери)
 *  - чутливість миши до обертання камери 
 *   [окремо по горизонталі та вертикалі]
 *  - інверсія до обертання (тільки по вертикалі)
 *  
 *  Налаштування звуку [+ загальне відключення усіх звуків]
 *   - Фонова музика (гучність + відключення)
 *   - Звукові ефекти (гучність + відключення)
 *   
 *  Рекорди
 *   - Максимально кількість зібраних монет
 *   - Кількість зроблених кроків
 *   - Пропрозиція ввести ім'я для збереження рекорду
 *   
 *  Налаштування відображення
 *   - Показувати ігровий таймер (годинник)
 *   - Показувати відстань до монети
 *   - .. напрям на монету
 *   - .. таймер зникнення монети
 *    [+ відомості про рівень складності з вибраними показниками]
 *    [+ відомості про кількість балів за монету]
 *    [? змінювати висоту спавну (необхідність стрибати)]
 *    
 *  Кнопки
 *   - Закрити (меню)
 *   - Вийти з гри
 *   - Відновити стандартні налаштування
 * ------------------------------------------------------------
 * Д.З.
 * 1. Фон - напівпрозорий рисунок на 100% екрану (адаптивний до будь-якої
 *     роздільної здатності)
 * 2. Контейнер центральної частини, менш прозорий, еластичний пропорційно
 *     до розмірів екрану
 * 3. Заголовок "GAME MENU" з адаптивним розміром у відповідності до екрану
 * 
 */
