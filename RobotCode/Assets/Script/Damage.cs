using UnityEngine;
using System.Collections;

public class Damage : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<RealRobot>() != null)
        {
            MainController.Defeat();
        }
    }
}
