using UnityEngine;
using System.Collections;

public class MoveForwardWait : MonoBehaviour
{
    public float _maxTime;
    public float _minTime;
    public float _speed;
    public float _targetZ;
    public bool _isStart;

    private float _time;
    private float _targetTime;
    private bool _isMove;

    void Update()
    {
        if (_isStart)
        {
            if (_isMove && _time >= _targetTime)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y,
                    Mathf.MoveTowards(transform.position.z, _targetZ, _speed * Time.deltaTime));
            }

            if(_time < _targetTime) _time += Time.deltaTime;
        }
    }

    public void StartMove()
    {
        _isStart = true;

        _targetTime = Random.Range(_minTime, _maxTime);
    }
}
