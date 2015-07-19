using UnityEngine;
using System.Collections;

public class PlayerRobotSelector : MonoBehaviour 
{
    public LayerMask _mask;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) 
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 20f, _mask.value))
            {
                RealRobot realRobot = hit.collider.gameObject.GetComponent<RealRobot>();

                if (realRobot != null)
                {
                    GUIController.ShowFunctionsList(realRobot);
                }
            }
        }
    }
}
