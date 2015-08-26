using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class GUIController : MonoBehaviour
{
    public static GUIController Main { get; set; }

    public Color _enableButtonColor;
    public Color _disableButtonColor;

    public List<MonoBehaviour> _componentsDisableWhileGUI;

    public CodeEditor _codeEditor;
    public GUIFunctionList _functionsList;
    public GUIMissionInfo _missionInfo;
    public GUIDocumentation _documentation;
    public GameObject _menuPanel;
    public GameObject _loadGamePanel;
    public GameObject _aim;

    public static RealRobot RealRobot { get; set; }

    void Awake()
    {
        if (Main == null)
        {
            Main = this;
        }
        else
        {
            print("There are two GUIControllers at least.");
        }
    }

    public void OnLevelWasLoaded(int level)
    {
        if (MainController.IsGame)
        {
            _componentsDisableWhileGUI.Clear();

            _componentsDisableWhileGUI.Add(GameObject.FindObjectOfType<RigidbodyFirstPersonController>());

            _componentsDisableWhileGUI.Add(GameObject.FindObjectOfType<PlayerRobotSelector>());

            HideAll();

            _missionInfo.LoadMission();

            ShowMissionInfo();
        } 
    }

    public static void ShowMessage(string message)
    {
        print(message);
    }

    public static void HideAll()
    {
        HideAim();
        HideCodeEditor();
        HideMissionInfo();
        HideFunctionsList();
        HideDocumentation();
    }

    public static void ShowFunctionsList(RealRobot realRobot)
    {
        RealRobot = realRobot;

        Main._functionsList.gameObject.SetActive(true);

        RefreshComponentsDisableWhileGUI();
    }

    public static void HideFunctionsList()
    {
        Main._functionsList.gameObject.SetActive(false);

        RefreshComponentsDisableWhileGUI();
    }

    public static void ShowCodeEditor(CodeInfo codeInfo)
    {
        Main._codeEditor.Set(codeInfo, RealRobot);

        Main._codeEditor.gameObject.SetActive(true);

        Main._functionsList.gameObject.SetActive(false);

        RefreshComponentsDisableWhileGUI();
    }

    public static void HideCodeEditor()
    {
        Main._codeEditor.gameObject.SetActive(false);

        RefreshComponentsDisableWhileGUI();
    }

    public static void ShowMissionInfo()
    {
        Main._missionInfo.gameObject.SetActive(true);

        RefreshComponentsDisableWhileGUI();
    }

    public static void HideMissionInfo()
    {
        Main._missionInfo.gameObject.SetActive(false);

        RefreshComponentsDisableWhileGUI();
    }

    public static void ShowDocumentation()
    {
        Main._documentation.gameObject.SetActive(true);

        RefreshComponentsDisableWhileGUI();
    }

    public static void HideDocumentation()
    {
        Main._documentation.gameObject.SetActive(false);

        RefreshComponentsDisableWhileGUI();
    }

    public void ShowLoadGamePanel()
    {
        Main._menuPanel.SetActive(false);

        Main._loadGamePanel.SetActive(true);

        Main._loadGamePanel.GetComponentInChildren<GUILoadGameList>().Initialize();
    }

    public void ShowMenuPanel()
    {
        Main._menuPanel.SetActive(true);

        Main._loadGamePanel.SetActive(false);
    }

    public static void ShowAim()
    {
        Main._aim.SetActive(true);
    }

    public static void HideAim()
    {
        Main._aim.SetActive(false);
    }

    private static void RefreshComponentsDisableWhileGUI()
    {
        if(Main._codeEditor.gameObject.activeInHierarchy ||
           Main._missionInfo.gameObject.activeInHierarchy ||
           Main._functionsList.gameObject.activeInHierarchy ||
           Main._documentation.gameObject.activeInHierarchy)
        {
            Main._componentsDisableWhileGUI.ForEach(x => x.enabled = false);

            HideAim();
        }
        else
        {
            Main._componentsDisableWhileGUI.ForEach(x => x.enabled = true);

            ShowAim();
        }
    }
}
