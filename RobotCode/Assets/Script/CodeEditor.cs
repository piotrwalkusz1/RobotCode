using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CodeEditor : MonoBehaviour
{
    public GUIStyle _guiStyle;
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
    public Text _text;
    public RectTransform _textRectTransform;

    private CodeInfo _codeInfo;

    private GUIContent _guiContent;

    void Start()
    {
        _textRectTransform = _text.GetComponent<RectTransform>();

        _guiContent = new GUIContent();
        _text.fontSize = _guiStyle.fontSize;
        _text.font = _guiStyle.font;
    }

    void Update()
    {
        _guiContent.text = _codeField.text;

        Vector2 size = _guiStyle.CalcSize(_guiContent);

        _codeField.GetComponent<RectTransform>().sizeDelta = new Vector2(size.x + _textRectTransform.offsetMin.x - _textRectTransform.offsetMax.x, size.y + _textRectTransform.offsetMin.y - _textRectTransform.offsetMax.y);
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
