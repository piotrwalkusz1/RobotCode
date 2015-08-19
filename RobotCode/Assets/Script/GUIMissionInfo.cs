using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Xml;
using System.Linq;

public class GUIMissionInfo : MonoBehaviour
{
    public Text _name;
    public Text _description;

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
    }
}
