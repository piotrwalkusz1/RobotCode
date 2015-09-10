using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Xml;
using System.Linq;

public class GUIMissionInfo : MonoBehaviour
{
    public GUIStyle _guiStyle;
    public Text _name;
    public Text _description;
    public GameObject _descriptionParent;

    public void Show()
    {
        GUIController.ShowMissionInfo();
    }

    public void Hide()
    {
        GUIController.HideMissionInfo();
    }

    public void LoadMission()
    {
        TextAsset file = Resources.Load<TextAsset>("missions");

        XmlDocument xml = new XmlDocument();

        xml.LoadXml(file.text);

        XmlNode root = xml.FirstChild;

        XmlNodeList missionList = root.ChildNodes;

        XmlNode mission = missionList.Item(MainController.Lvl - 1);

        _name.text = mission.ChildNodes[1].InnerText;

        _description.text = mission.ChildNodes[2].InnerText;



        _description.GetComponent<Text>().font = _guiStyle.font;
        _description.GetComponent<Text>().fontSize = _guiStyle.fontSize;

        GUIContent _guiContent = new GUIContent(_description.GetComponent<Text>().text);

        int textMaxWidth = (int)_description.GetComponent<RectTransform>().sizeDelta.x;

        Vector2 size = new Vector2(textMaxWidth, _guiStyle.CalcHeight(_guiContent, textMaxWidth));

        var textRectTransform = _description.GetComponent<RectTransform>();

        _descriptionParent.GetComponent<RectTransform>().sizeDelta = new Vector2(size.x + textRectTransform.offsetMin.x - textRectTransform.offsetMax.x, size.y + textRectTransform.offsetMin.y - textRectTransform.offsetMax.y);
    }
}
