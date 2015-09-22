using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Xml;

public class GUIDocumentation : MonoBehaviour
{
    public static GUIDocumentation Main { get; set; }

    public GUIStyle _guiStyle;
    public GameObject _docNavButtonParent;
    public GameObject _docNavButtonPrefab;
    public int _docNavButtonHeight;
    public GameObject _docTextParent;
    public GameObject _docTextPrefab;

    private List<GameObject> _docNavButtonsList = new List<GameObject>();
    private List<GameObject> _docTextsList = new List<GameObject>();
    private int _choosen = -1;

    private GUIContent _guiContent;

    void Awake()
    {
        if (Main == null)
        {
            Main = this;

            _guiContent = new GUIContent();
        }
        else
        {
            print("There are two GUIDocumentation at least");
        }

        LoadDocumentation();
    }

    public void Hide()
    {
        GUIController.HideDocumentation();
    }

    public void LoadDocumentation()
    {
        DeleteAllDocNavButtons();

        DeleteAllDocTexts();

        TextAsset file = Resources.Load<TextAsset>("documentation");

        XmlDocument xml = new XmlDocument();

        xml.LoadXml(file.text);

        XmlNode root = xml.FirstChild;

        XmlNodeList navList = root.ChildNodes;

        for(int i = 0; i < navList.Count; i++)
        {
            var button = AddDocNavButton();

            button.GetComponentsInChildren<Text>(includeInactive: true)[0].text = navList.Item(i).ChildNodes.Item(0).InnerText;

            button.GetComponent<Identificator>()._id = i;

            var docText = AddDocText();

            docText.text = navList.Item(i).ChildNodes.Item(1).InnerText;

            docText.GetComponent<Identificator>()._id = i;
        }
    }

    public void ShowDocText(int id)
    {
        HideAllDocTexts();

        _choosen = id;

        var find = _docTextsList.Find(x => x.GetComponent<Identificator>()._id == _choosen);

        find.SetActive(true);

        find.GetComponent<Text>().font = _guiStyle.font;
        find.GetComponent<Text>().fontSize = _guiStyle.fontSize;

        Text docText = find.GetComponent<Text>();

        _guiContent.text = docText.text;

        int docTextMaxWidth = (int)_docTextParent.GetComponent<RectTransform>().sizeDelta.x;

        Vector2 size = new Vector2(docTextMaxWidth, _guiStyle.CalcHeight(_guiContent, docTextMaxWidth));

        var textRectTransform = docText.GetComponent<RectTransform>();

        _docTextParent.GetComponent<RectTransform>().sizeDelta = new Vector2(size.x + textRectTransform.offsetMin.x - textRectTransform.offsetMax.x, docText.preferredHeight + textRectTransform.offsetMin.y - textRectTransform.offsetMax.y);
    }

    public void DeleteAllDocNavButtons()
    {
        _docNavButtonsList.ForEach(x => Destroy(x));

        _docNavButtonsList.Clear();
    }

    public void HideAllDocTexts()
    {
        _docTextsList.ForEach(x => x.SetActive(false));
    }

    public void DeleteAllDocTexts()
    {
        _docTextsList.ForEach(x => Destroy(x));

        _docTextsList.Clear();
    }

    public Button AddDocNavButton()
    {
        var position = new Vector3(0, -_docNavButtonHeight * _docNavButtonsList.Count, 0);

        var go = Instantiate(_docNavButtonPrefab, position, Quaternion.identity) as GameObject;

        go.transform.SetParent(_docNavButtonParent.transform, worldPositionStays: false);

        _docNavButtonsList.Add(go);

        return go.GetComponent<Button>();
    }

    public Text AddDocText()
    {
        var go = Instantiate(_docTextPrefab) as GameObject;

        go.transform.SetParent(_docTextParent.transform, worldPositionStays: false);

        go.SetActive(false);

        _docTextsList.Add(go);

        return go.GetComponent<Text>();
    }
}
