using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Xml;

public class GUIDocumentation : MonoBehaviour
{
    public static GUIDocumentation Main { get; set; }

    public GameObject _docNavButtonParent;
    public GameObject _docNavButton;
    public int _docNavButtonHeight;
    public GameObject _docTextParent;
    public GameObject _docText;

    private List<GameObject> _docNavButtonsList = new List<GameObject>();
    private List<GameObject> _docTextsList = new List<GameObject>();
    private int _choosen = -1;

    void Awake()
    {
        if (Main == null)
        {
            Main = this;
        }
        else
        {
            print("There are two GUIDocumentation at least");
        }
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

        var go = Instantiate(_docNavButton, position, Quaternion.identity) as GameObject;

        go.transform.SetParent(_docNavButtonParent.transform, worldPositionStays: false);

        _docNavButtonsList.Add(go);

        return go.GetComponent<Button>();
    }

    public Text AddDocText()
    {
        var go = Instantiate(_docText) as GameObject;

        go.transform.SetParent(_docTextParent.transform, worldPositionStays: false);

        go.SetActive(false);

        _docTextsList.Add(go);

        return go.GetComponent<Text>();
    }
}
