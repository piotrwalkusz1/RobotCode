using UnityEngine;
using System.Collections;

public class NormalButton : MonoBehaviour
{
    public string _name;

    public bool IsPressed
    {
        get { return _objectsNumber > 0; }
    }

    private int _objectsNumber;

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" || other.GetComponent<RealRobot>() != null)
        {
            _objectsNumber++;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" || other.GetComponent<RealRobot>() != null)
        {
            _objectsNumber--;
        }
    }
}
