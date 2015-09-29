using UnityEngine;
using System.Collections;

public class TowerAutoShoot : MonoBehaviour
{
    public GameObject _target;
    public GameObject _bullet;
    public GameObject _respawn;

    private bool _isFired;

    void Update()
    {
        if (!_isFired && (_target.transform.position - transform.position).sqrMagnitude < 200)
        {
            _isFired = true;

            Instantiate(_bullet, _respawn.transform.position, transform.rotation);
        }
    }
}
