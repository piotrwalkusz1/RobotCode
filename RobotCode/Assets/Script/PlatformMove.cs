using UnityEngine;
using System.Collections;

public class PlatformMove : MonoBehaviour 
{
    public Vector2 _poz1;
    public Vector2 _poz2;
    public float _speed;

    private bool _moveToPoz1;

    void Update()
    {
        Vector2 currentPoz = new Vector2(transform.position.x, transform.position.z);

        Vector2 newPoz = Vector2.MoveTowards(currentPoz, _moveToPoz1 ? _poz1 : _poz2, _speed * Time.deltaTime);

        transform.position = new Vector3(newPoz.x, transform.position.y, newPoz.y);

        if (newPoz == (_moveToPoz1 ? _poz1 : _poz2))
        {
            _moveToPoz1 = !_moveToPoz1;
        }
    }

}
