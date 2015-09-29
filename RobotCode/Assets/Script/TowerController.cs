using UnityEngine;
using System.Collections;
using System.Linq;

public class TowerController : RobotTech.ITower
{
    public Tower2 _tower;

    public void Disable()
    {
        MainController.UpdateEvent += delegate()
        {
            if (_tower != null)
            {
                _tower.Disable();
            }
            else
            {
                GUIController.ShowMessage("Obiekt typu 'Tower' nie wskazuje na żadną wieżę!", MessageColor.Red);
            }
        };       
    }

    public void Find(string name)
    {
        MainController.UpdateEvent += delegate()
        {
            var tower = GameObject.FindObjectsOfType<Tower2>().FirstOrDefault();

            if (tower != null && tower._name == name)
            {
                _tower = tower;
            }
            else
            {
                GUIController.ShowMessage("Wieża o nazwie '" + name + "' nie istnieje!", MessageColor.Red);
            }
        };
    }
}
