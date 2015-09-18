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

    void Update()
    {
        if (_isStart)
        {
            if (_time >= _targetTime)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y,
                    Mathf.MoveTowards(transform.position.z, _targetZ, _speed * Time.deltaTime));
            }

            if(_time < _targetTime) _time += Time.deltaTime;
        }
    }

    public void StartMove()
    {
        if (!_isStart)
        {
            _isStart = true;

            _targetTime = Random.Range(_minTime, _maxTime);
        }    
    }
}
