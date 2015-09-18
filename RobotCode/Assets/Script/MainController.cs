using UnityEngine;
using System.Collections;
using System.Reflection;
using System;
using RobotsLibrary;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class MainController : MonoBehaviour
{
    public static event Action UpdateEvent;

    public static MainController Main { get; private set; }

    public static bool IsGame { get; set; }
    public static int Lvl { get; set; }

    public PlayerProfilController PlayerProfilController { get; set; }

    private static List<WinCondition> _winConditions;

    private static bool _isAllWinConditionsAchieved;
    private static DateTime _winTime;
    private static float _timeToDefeat;
    private static bool _isDefeat;

    private const float TIME_TO_WIN_AFTER_CONDITIONS_ACHIEVED = 5f;
    private const float TIME_TO_DEFEAT = 4F;

    void Awake()
    {
        if (Main == null)
        {
            Main = this;
        }
        else
        {
            print("There are two MainControllers at least");
        }

        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (UpdateEvent != null)
        {
            UpdateEvent();

            UpdateEvent = null;
        }

        if (_isDefeat)
        {
            _timeToDefeat += Time.deltaTime;

            if (_timeToDefeat > TIME_TO_DEFEAT)
            {
                ReloadLevel();
            }
        }
        else if (_isAllWinConditionsAchieved && DateTime.UtcNow > _winTime)
        {
            Win();
        }
    }

    public void OnLevelWasLoaded(int level)
    {
        if (MainController.IsGame)
        {
            ResetWinConditions();

            CheckWinConditions();
        }
    }

    public void NewGame()
    {
        PlayerProfilController.Set(new SaveData(), "Piotr Wielki");

        PlayerProfilController.Save();

        InitializeGame();

        LoadScene(15);
    }

    public static void LoadGame(string saveName)
    {
        var saveData = SaveController.Load(saveName);

        PlayerProfilController.Set(saveData, saveName);

        InitializeGame();

        LoadScene(saveData.Lvl);
    }

    public static void Defeat()
    {
        GUIController.ShowMessage("Przegrałeś!", MessageColor.Red);

        _timeToDefeat = 0;

        _isDefeat = true;
    }

    public void ReloadLevel()
    {
        LoadScene(Lvl);
    }

    public static void InitializeGame()
    {
        IsGame = true;

        GUIController.HideMainMenu();

        GUIController.Main.HideLoadGamePanel();
    }

    public static void CheckWinConditions()
    {
        if (_winConditions.TrueForAll(x => x.IsConditionAchieved))
        {
            _isAllWinConditionsAchieved = true;

            _winTime = DateTime.UtcNow.AddSeconds(TIME_TO_WIN_AFTER_CONDITIONS_ACHIEVED);
        }
        else
        {
            _isAllWinConditionsAchieved = false;
        }
    }

    public static void Win()
    {
        _isAllWinConditionsAchieved = false;

        UpdatePlayerProfil();

        LoadNextScene();
    }

    private static void UpdatePlayerProfil()
    {
        PlayerProfilController.SaveData.Lvl = Lvl + 1;
    }

    private static void LoadNextScene()
    {
        LoadScene(Lvl + 1);
    }

    private static void LoadScene(int index)
    {
        Lvl = index;

        Application.LoadLevel(index);
    }

    private static void ResetWinConditions()
    {
        _winConditions = GameObject.FindObjectsOfType<WinCondition>().ToList();
    }
}
