using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CodeEditor : MonoBehaviour
{
    public RealRobot RealRobot { get; set; }
    public CodeInfo CodeInfo
    {
        get { return _codeInfo; }
        set
        {
            _codeInfo = value;

            if (_codeInfo == null)
            {
                _codeField.text = "";
            }
            else
            {
                _codeField.text = _codeInfo.Code;
            }
        }
    }

    public InputField _codeField;

    private CodeInfo _codeInfo;

    void Start()
    {

    }

    public void Hide()
    {
        GUIController.HideCodeEditor();
    }

    public void Compile()
    {
        SaveChanges();

        RealRobot.CompileOrRun(CodeInfo);
    }

    public void SaveChanges()
    {
        if (CodeInfo.Code != _codeField.text)
        {
            CodeInfo.Code = _codeField.text;

            CodeInfo.IsEdited = true;
        }
    }

    public void Set(CodeInfo codeInfo, RealRobot realRobot)
    {
        CodeInfo = codeInfo;

        RealRobot = realRobot;
    }
}
