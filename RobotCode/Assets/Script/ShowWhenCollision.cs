using UnityEngine;
using System.Collections;

public class ShowWhenCollision : MonoBehaviour
{
    public GameObject _object;

    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<RealRobot>() != null)
        {
            _object.SetActive(true);
        }
    }
}
