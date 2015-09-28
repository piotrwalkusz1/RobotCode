using UnityEngine;
using System.Collections;

public class Tower2 : MonoBehaviour, RobotTech.ITower
{
    public GameObject _target;
    public GameObject _bullet;

    void Update()
    {

    }

    public void Disable()
    {
        throw new System.NotImplementedException();
    }

    public void Find(string name)
    {
        throw new System.NotImplementedException();
    }
}
