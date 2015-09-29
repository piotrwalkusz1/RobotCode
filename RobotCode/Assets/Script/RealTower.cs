using UnityEngine;
using System.Collections;
using RobotsLibrary;

public class RealTower : MonoBehaviour, ITower
{
    public Transform _bulletRespawn;
    public GameObject _bullet;
    public GameObject _sphere;
    public float _rotationSpeed;

    private Vector3 _target;
    private bool _prepareToFire;
    private bool _wasFired;

    void Update()
    {
        if (_prepareToFire)
        {
            Vector3 targetDir = _target - transform.position;

            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, _rotationSpeed * Time.deltaTime, 0);

            transform.rotation = Quaternion.LookRotation(newDir);

            if (!_wasFired && Vector3.Angle(targetDir, transform.forward) < 0.5f)
            {
                _wasFired = true;

                Instantiate(_bullet, _bulletRespawn.position, transform.rotation);

                StartCoroutine(Fuction_ReadyFire());
            }
        }
    }

    public void Shoot(Position position)
    {
        MainController.UpdateEvent += delegate() { Function_Shoot(position); };
    }

    public void Function_Shoot(Position position)
    {
        if (_prepareToFire)
        {
            GUIController.ShowMessage("Wieża nie jest gotowa do strzału!", MessageColor.Red);
        }
        else
        {
            _prepareToFire = true;

            _target = new Vector3(position.x, _sphere.transform.position.y, position.y);
        }
    }

    private IEnumerator Fuction_ReadyFire()
    {
        yield return new WaitForSeconds(0.3f);

        _prepareToFire = false;

        _wasFired = false;
    }
}
