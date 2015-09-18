using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Messager : MonoBehaviour
{
    public float _maxTime;
    public Text _text;

    public static Messager Main { get; set; }

    private static float _time;
    private static bool _isMessage;

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

            if (_time > _maxTime)
            {
                _text.text = "";

                _isMessage = false;

                GUIController.HideMessager();
            }
        }
    }

    public static void ShowMessage(string text, MessageColor color)
    {
        Main._text.text = text;

        _time = 0;
        _isMessage = true;
    }
}
