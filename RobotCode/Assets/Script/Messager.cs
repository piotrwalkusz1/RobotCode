using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Messager : MonoBehaviour
{
    public float _maxTime;
    public Image _panel;
    public Text _text;

    public static Messager Main { get; set; }

    private static float _time;
    private static bool _isMessage;
    private float _currentMaxTime;

    void Awake()
    {
        if (Main == null)
        {
            Main = this;
        }
        else
        {
            print("There are 2 messager at least");
        }
    }

    void Update()
    {
        if (_isMessage)
        {
            _time += Time.deltaTime;

            if (_time > _currentMaxTime)
            {
                _text.text = "";

                _isMessage = false;

                GUIController.HideMessager();
            }
        }
    }

    public static void ShowMessage(string text, MessageColor color)
    {
        ShowMessage(text, color, Main._maxTime);  
    }

    public static void ShowMessage(string text, MessageColor color, float time)
    {
        Main._text.text = text;

        _time = 0;
        _isMessage = true;
        Main._currentMaxTime = time;

        switch (color)
        {
            case MessageColor.Green:
                Main._panel.color = new Color(0, 1, 23 / 255);
                break;
            case MessageColor.Red:
                Main._panel.color = new Color(1, 62 / 255, 62 / 255);
                break;
            default:
                Main._panel.color = new Color(242 / 255, 246 / 255, 14 / 255);
                break;
        }

    }
}
