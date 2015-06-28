using UnityEngine;
using System.Collections;
using RobotsLibrary;
using System.Threading;
using System.Runtime.Remoting.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;

public class RealRobot : MonoBehaviour, IRobot
{
    public float _movementSpeed;
    public float _rotationSpeed;

    private List<Action> _tasks { get; set; }
    private object _tasks_lock = new object();

    private bool _isMove = false;
    private float _movementDistanceLeft;

    private int _rotationPhase = 0; // -1 - left, 0 - nothing, 1 - right
    private float _rotationDeegresLeft;

    private bool _isWait = false;
    private DateTime _waitEndTime;

    private AutoResetEvent _stopEvent = new AutoResetEvent(false);

	// Use this for initialization
	void Awake () {
        _tasks = new List<Action>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        ExecuteTasks();

        Update_Move();

        Update_Rotate();

        Update_Wait();
	}

    public void ContinueProcess()
    {
        _stopEvent.Set();
    }

    public void StopProcess()
    {
        _stopEvent.WaitOne();
    }

    public void Move(float distance)
    {
        AddTaskAndWait(delegate() { Function_Move(distance); });
    }

    public void Rotate(float deegres)
    {
        AddTaskAndWait(delegate() { Function_Rotate(deegres); });
    }

    public void Wait(float seconds)
    {
        AddTaskAndWait(delegate() { Function_Wait(seconds); });
    }

    private void Function_Move(float distance)
    {
        _movementDistanceLeft = distance;

        _isMove = true;
    }

    private void Function_Rotate(float deegres)
    {
        _rotationPhase = deegres > 0 ? 1 : -1;

        _rotationDeegresLeft = Mathf.Abs(deegres);
    }

    private void Function_Wait(float seconds)
    {
        _waitEndTime = DateTime.UtcNow.AddSeconds(seconds);

        _isWait = true;
    }

    private void Update_Move()
    {
        if (_isMove)
        {
            if (_movementDistanceLeft <= 0)
            {
                _isMove = false;

                ContinueProcess();
            }
            else
            {
                Vector2 startPos = new Vector2(transform.position.x, transform.position.z);

                transform.Translate(0, 0, _movementSpeed * Time.deltaTime);

                Vector2 endPos = new Vector2(transform.position.x, transform.position.z);

                float traveledDistance = (endPos - startPos).magnitude;

                _movementDistanceLeft -= traveledDistance;
            }
        }      
    }

    private void Update_Rotate()
    {
        if (_rotationPhase != 0)
        {
            if (_rotationDeegresLeft <= 0)
            {
                _rotationPhase = 0;

                ContinueProcess();
            }
            else
            {
                float startRot = transform.eulerAngles.y;

                transform.Rotate(0, _rotationPhase * _rotationSpeed * Time.deltaTime, 0);

                float endRot = transform.eulerAngles.y;

                _rotationDeegresLeft -= Math.Abs(Mathf.DeltaAngle(startRot, endRot));
            }
        }      
    }

    private void Update_Wait()
    {
        if (_isWait)
        {
            if (DateTime.UtcNow > _waitEndTime)
            {
                _isWait = false;

                ContinueProcess();
            }
        }
    }

    private void ExecuteTasks()
    {
        Action task;

        lock (_tasks_lock)
        {
            while (_tasks.Count > 0)
            {
                task = _tasks[0];

                _tasks.RemoveAt(0);

                task();
            }
        }
    }

    private void AddTaskAndWait(Action task)
    {
        lock (_tasks_lock)
        {
            _tasks.Add(task);
        }

        StopProcess();
    }
}
