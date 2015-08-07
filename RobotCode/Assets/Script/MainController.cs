using UnityEngine;
using System.Collections;
using System.Reflection;
using System;
using RobotsLibrary;
using System.Threading;
using System.Collections.Generic;
using System.Linq;

public class MainController : MonoBehaviour
{
    public static MainController Main { get; private set; }

    public static bool IsGame { get; set; }
    public static int Lvl { get; set; }

    public GameObject _canvasMenu;
    public GameObject _canvasGame;

    public PlayerProfilController PlayerProfilController { get; set; }

    private static List<WinCondition> _winConditions;

    private static bool _isAllWinConditionsAchieved;
    private static DateTime _winTime;

    private const float TIME_TO_WIN_AFTER_CONDITIONS_ACHIEVED = 5f;

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
        if (_isAllWinConditionsAchieved && DateTime.UtcNow > _winTime)
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

        LoadScene(4);
    }

    public static void LoadGame(string saveName)
    {
        var saveData = SaveController.Load(saveName);

        PlayerProfilController.Set(saveData, saveName);

        InitializeGame();

        LoadScene(saveData.Lvl);
    }

    public static void InitializeGame()
    {
        IsGame = true;

        Main._canvasMenu.SetActive(false);

        Main._canvasGame.SetActive(true);
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

        Application.LoadLevel("Scene" + index.ToString());
    }

    private static void ResetWinConditions()
    {
        _winConditions = GameObject.FindObjectsOfType<WinCondition>().ToList();
    }
}
