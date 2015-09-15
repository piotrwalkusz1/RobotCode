using UnityEngine;
using System.Collections;

public class SpeedController : MonoBehaviour
{
    public float _speedDefeat;

    private RealRobot _robot;

    void Awake()
    {
        _robot = GetComponent<RealRobot>();
    }

    void Update()
    {
        if (_robot.GetMovementSpeed() >= _speedDefeat && _robot.IsMove())
        {
            MainController.Defeat();
        }
    }
}
