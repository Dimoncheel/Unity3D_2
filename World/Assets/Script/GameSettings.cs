using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Static settings accessed by all scripts
/// </summary>
public class GameSettings : MonoBehaviour
{
    private static readonly string _SettingsFilename = "Assets/Files/settings.txt";  // не забути створити папку Files в Assets

    private static bool _mouseZoomInverted;
    private static bool _verticalInverted;
    private static float _sensitivity;
    private static float _effectsVolume;
    private static float _musicVolume;
    private static bool _effectsEnabled;
    private static bool _musicEnabled;
    private static bool _allSoundsDisabled;
    private static bool _gameTimerEnabled;
    private static bool _coinDistanceEnabled;
    private static bool _directionHintsEnabled;
    private static bool _coinTimeoutEnabled;
    private static bool _staminaEnabled;
    private static List<LeaderRecord> _leaderRecords;

    public static List<LeaderRecord> LeaderRecords => _leaderRecords;
    private static int DifficultyTogglesEnabled()
    {
        int togglesEnabled = 0;
        if (_gameTimerEnabled) ++togglesEnabled;
        if (_coinDistanceEnabled) ++togglesEnabled;
        if (_directionHintsEnabled) ++togglesEnabled;
        if (_coinTimeoutEnabled) ++togglesEnabled;
        if (_staminaEnabled) ++togglesEnabled;
        return togglesEnabled;
    }
    public static GameDifficulty Difficulty
    {
        get 
        {   // Якщо вибрані 0-1 toggle, то рівень складний, 2-3 нормальний, 4-5 простий 
            return DifficultyTogglesEnabled() switch
            {
                0 => GameDifficulty.High,
                1 => GameDifficulty.High,
                2 => GameDifficulty.Normal,
                3 => GameDifficulty.Normal,
                _ => GameDifficulty.Easy
            };
        }
    }
    public static int CoinValue
    {
        get
        {
            return DifficultyTogglesEnabled() switch
            {
                0 => 50,
                1 => 40,
                2 => 30,
                3 => 25,
                4 => 15,
                5 => 10,
                _ => 5
            };
        }
    }


    public static bool MouseZoomInverted
    {
        get => _mouseZoomInverted;
        set
        {
            _mouseZoomInverted = value;
            SaveSettings();
        }
    }

    public static bool VerticalInverted
    {
        get => _verticalInverted;
        set
        {
            _verticalInverted = value;
            SaveSettings();
        }
    }
    public static float Sensitivity
    {
        get => _sensitivity;
        set
        {
            _sensitivity = value;
            SaveSettings();
        }
    }
    public static float EffectsVolume
    {
        get => _effectsVolume;
        set
        {
            _effectsVolume = value;
            SaveSettings();
        }
    }
    public static float MusicVolume
    {
        get => _musicVolume;
        set
        {
            _musicVolume = value;
            SaveSettings();
        }
    }
    public static bool MusicEnabled
    {
        get => _musicEnabled;
        set
        {
            _musicEnabled = value;
            SaveSettings();
        }
    }
    public static bool EffectsEnabled
    {
        get => _effectsEnabled;
        set
        {
            _effectsEnabled = value;
            SaveSettings();
        }
    }
    public static bool AllSoundsDisabled
    {
        get => _allSoundsDisabled;
        set
        {
            _allSoundsDisabled = value;
            SaveSettings();
        }
    }
    public static bool GameTimerEnabled
    {
        get => _gameTimerEnabled;
        set
        {
            _gameTimerEnabled = value;
            SaveSettings();
        }
    }
    public static bool CoinDistanceEnabled
    {
        get => _coinDistanceEnabled;
        set
        {
            _coinDistanceEnabled = value;
            SaveSettings();
        }
    }
    public static bool DirectionHintsEnabled
    {
        get => _directionHintsEnabled;
        set
        {
            _directionHintsEnabled = value;
            SaveSettings();
        }
    }
    public static bool CoinTimeoutEnabled
    {
        get => _coinTimeoutEnabled;
        set
        {
            _coinTimeoutEnabled = value;
            SaveSettings();
        }
    }
    public static bool StaminaEnabled
    {
        get => _staminaEnabled;
        set
        {
            _staminaEnabled = value;
            SaveSettings();
        }
    }
   

    private static void SaveSettings()
    {
        _leaderRecords = new()
        {
            new(){Name="Player 1",Score=200},
            new(){Name="Player 2",Score=180},
            new(){Name="Player 3",Score=160},
            new(){Name="Player 4",Score=140},
            new(){Name="Player 5",Score=120}
        };
        var stringBuilder = new System.Text.StringBuilder()
            .Append(_mouseZoomInverted).Append('\n')
            .Append(_verticalInverted).Append('\n')
            .Append(_sensitivity).Append('\n')
            .Append(_gameTimerEnabled).Append('\n')
            .Append(_coinDistanceEnabled).Append('\n')
            .Append(_directionHintsEnabled).Append('\n')
            .Append(_coinTimeoutEnabled).Append('\n')
            .Append(_staminaEnabled).Append('\n')
            .Append(_effectsVolume).Append('\n')
            .Append(_musicVolume).Append('\n')
            .Append(_allSoundsDisabled).Append('\n')
            ;
        foreach (var item in _leaderRecords)
        {
            stringBuilder.Append(item.ToString()).Append('\n');
        }
        System.IO.File.WriteAllText(_SettingsFilename, stringBuilder.ToString());
    }

    public static void LoadSettings()
    {
        try
        {
            string[] lines = System.IO.File.ReadAllText(_SettingsFilename).Split('\n',System.StringSplitOptions.RemoveEmptyEntries);
            int n = 0;
            _mouseZoomInverted = System.Convert.ToBoolean( lines[n++] );
            _verticalInverted  = System.Convert.ToBoolean( lines[n++] );
            _sensitivity       = System.Convert.ToSingle ( lines[n++] );
            _gameTimerEnabled  = System.Convert.ToBoolean( lines[n++] );
            _coinDistanceEnabled  = System.Convert.ToBoolean( lines[n++] );
            _directionHintsEnabled  = System.Convert.ToBoolean( lines[n++] );
            _coinTimeoutEnabled  = System.Convert.ToBoolean( lines[n++] );
            _staminaEnabled  = System.Convert.ToBoolean( lines[n++] );
            _effectsVolume  = System.Convert.ToSingle( lines[n++] );
            _musicVolume  = System.Convert.ToSingle( lines[n++] );
            _allSoundsDisabled  = System.Convert.ToBoolean( lines[n++] );

            _leaderRecords.Clear();
            for(int i = n; i < lines.Length; i++)
            {
                try
                {
                    _leaderRecords.Add(LeaderRecord.Parse(lines[i]));
                }
                catch(System.ArgumentException ex)
                {
                    Debug.Log(ex.Message);
                }
            }
        }
        catch (System.Exception ex)
        {
            // Set settings to defaults
            Debug.Log(ex.Message);
        }
    }
    public static void ResetDefaults()
    {
        _mouseZoomInverted = false;
        _verticalInverted = false;
        _sensitivity = 0.5f;
        _gameTimerEnabled = true;
        _coinDistanceEnabled = true;
        _directionHintsEnabled = true;
        _coinTimeoutEnabled = true;
        _staminaEnabled = true;
        _effectsVolume = 0.5f;
        _musicVolume = 0.5f;
        _allSoundsDisabled = false;
        _effectsEnabled = true;
        _musicEnabled = true;

        SaveSettings();
    }
    public enum GameDifficulty
    {
        Easy = 0,
        Normal = 1,
        High = 2
    }

    public class LeaderRecord
    {
        public string Name { get; set; }
        public int Score { get; set; }
        public override string ToString()
        {
            return $"{Name};{Score}";
        }

        public static LeaderRecord Parse(string data)
        {
            try
            {
                string[] parts = data.Split(';');
                return new LeaderRecord()
                {
                    Name = parts[0],
                    Score = int.Parse(parts[1])
                };
            }
            catch
            {
                throw new System.ArgumentException($"Invalid LeaderRecord init string: '{data};");
            }
        }
    }
}
/* Закладаємо умову, що зміна налаштувань має зберігатись на постійній
 * основі (у файлі)
 * Схема взаємодії:
 *  GUI [v]
 *   |
 * MenuControl                   GameSettings               Інші скрипти
 *  OnChange(v) ---------------->  Val { get --------------->
 *    { GS.Val = v }                |  set Save() }
 *  ApplySettings()                LoadSettings()
 *  
 *  
 *  Д.З. Реалізувати збереження та відновлення налагоджень ігрового меню
 *  Для налагоджень складності забезпечити оновлення показників відразу при
 *  виборі користувача
 */