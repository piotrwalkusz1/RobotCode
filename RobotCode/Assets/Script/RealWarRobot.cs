using UnityEngine;
using System.Collections;
using RobotsLibrary;

public class RealWarRobot : RealRobot, IWarRobot
{
    public GameObject _bullet;
    public Transform _bulletRespawn;
    public float _bulletMaxTime;

    private float _bulletTime;

    protected new void Update()
    {
        base.Update();

        Update_Bullet();
    }

    public void Shoot()
    {
        AddTaskAndWaitOneFrame(delegate() { Function_Shoot(); });
    }

    private void Function_Shoot()
    {
        if (_bulletTime >= _bulletMaxTime)
        {
            Instantiate(_bullet, _bulletRespawn.position, _bulletRespawn.rotation);

            _bulletTime = 0;
        }
        else
        {
            GUIController.ShowMessage("Robot nie jest jeszcze gotowy do strzału");
        }
    }

    private void Update_Bullet()
    {
        if (_bulletTime <= _bulletMaxTime)
        {
            _bulletTime += Time.deltaTime;
        }
    }
}
