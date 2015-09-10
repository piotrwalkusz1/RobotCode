using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System;

public class ButtonArea : MonoBehaviour
{
    public UnityEvent _clickAction;
    public float _timeToTrigger;
    public bool _isPlayerAllowed;

    private float _time;
    private bool _isEntered;
    private bool _isUsed;

    public void Update()
    {
        if (!_isUsed && _isEntered)
        {
            _time += Time.deltaTime;

            if (_time > _timeToTrigger)
            {
                _clickAction.Invoke();

                _isUsed = true;
            }
        }
    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<RealRobot>() != null || (_isPlayerAllowed && other.tag == "Player"))
        {
            _isEntered = true;

            _time = 0;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<RealRobot>() != null || (_isPlayerAllowed && other.tag == "Player"))
        {
            _isEntered = false;
        }
    }
}
