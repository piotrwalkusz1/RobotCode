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

    public GameObject _canvasMenu;
    public GameObject _canvasGame;

    public PlayerProfilController PlayerProfilController { get; set; }

    private static List<WinCondition> _winConditions;

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
        PlayerProfilController.SaveData = new SaveData();

        PlayerProfilController.Name = "Piotr";

        PlayerProfilController.Save();

        IsGame = true;

        _canvasMenu.SetActive(false);

        _canvasGame.SetActive(true);

        Application.LoadLevel("Scene1");
    }

    public static void CheckWinConditions()
    {
        if (_winConditions.TrueForAll(x => x.IsConditionAchieved))
        {
            Win();
        }
    }

    public static void Win()
    {
        print("Win");
    }

    private static void ResetWinConditions()
    {
        _winConditions = GameObject.FindObjectsOfType<WinCondition>().ToList();
    }
}
