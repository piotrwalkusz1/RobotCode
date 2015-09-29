using UnityEngine;
using System.Collections;

public class Tower2 : MonoBehaviour
{
    public string _name;

    public void Disable()
    {
        GetComponent<TowerAutoShoot>().enabled = false;

        GetComponent<UnityStandardAssets.Cameras.LookatTarget>().enabled = false;
    }
}
