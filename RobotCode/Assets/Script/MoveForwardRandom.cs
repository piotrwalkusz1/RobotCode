using UnityEngine;
using System.Collections;

public class MoveForwardRandom : MonoBehaviour
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
            if (_time >= _targetTime)
            {
                _isMove = !_isMove;

                _time = 0;

                _targetTime = Random.Range(_minTime, _maxTime);
            }

            if (_isMove)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y,
                    Mathf.MoveTowards(transform.position.z, _targetZ, _speed * Time.deltaTime));
            }

            _time += Time.deltaTime;
        }
    }

    public void StartMove()
    {
        _isStart = true;
    }
}
