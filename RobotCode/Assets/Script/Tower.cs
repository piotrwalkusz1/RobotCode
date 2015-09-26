using UnityEngine;
using System.Collections;
using RobotsLibrary;

public class Tower : MonoBehaviour 
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
            Vector3 newDir = Vector3.RotateTowards(transform.forward, _target - transform.position, _rotationSpeed * Time.deltaTime, 0);

            transform.LookAt(newDir);

            if (!_wasFired && Vector3.Angle(newDir, transform.forward) < 0.5f)
            {
                _wasFired = true;

                Instantiate(_bullet, _bulletRespawn.position, transform.rotation);

                StartCoroutine(Fuction_ReadyFire());
            }
        }
    }

    public void Fire(Position position)
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
        yield return new WaitForSeconds(1);

        _prepareToFire = false;
    }
}
