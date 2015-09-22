﻿using UnityEngine;
using System.Collections;
using RobotsLibrary;
using System.Threading;
using System.Runtime.Remoting.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;

public class RealRobot : MonoBehaviour, IRobot
{
    public GameObject _blueLight;

    public string _firstCode;

    public float _maxMovementSpeed;
    public float _maxRotationSpeed;

    public Action OnLightFunction;

    public CodeInfo _code { get; set; }

    private float _currentMovementSpeed;
    private float _currentRotationSpeed;

    private List<Action> _tasks { get; set; }
    private object _tasks_lock = new object();

    private bool _isMove;
    private bool _movementTypeIsTime; // false - distance, true - time
    private float _movementDistanceOrTimeLeft;
    private Vector2 _movementLastPosition;

    private bool _rotationTypeIsTime; // false - distance, true - time
    private int _rotationPhase = 0; // -1 - left, 0 - nothing, 1 - right
    private float _rotationDeegresOrTimeLeft;

    private bool _isWait = false;
    private DateTime _waitEndTime;

    private bool _isWaitOneFrame = false;

    private AutoResetEvent _stopEvent = new AutoResetEvent(false);

	// Use this for initialization
	protected void Start () 
    {
        _tasks = new List<Action>();

        _code = new CodeInfo();
        if(_firstCode != null && _firstCode != "") _code.Code = _firstCode;

        _currentMovementSpeed = _maxMovementSpeed;
        _currentRotationSpeed = _maxRotationSpeed;
	}
	
	// Update is called once per frame
	protected void Update () 
    {
        ExecuteTasks();

        Update_Rotate();       

        Update_Wait();

        if (_isWaitOneFrame)
        {
            ContinueProcess();

            _isWaitOneFrame = false;
        }
	}

    public void FixedUpdate()
    {
        Update_Move(); 
    }

    public void CompileOrRun(CodeInfo codeInfo)
    {
        if (codeInfo.IsEdited)
        {
            codeInfo.CompileAndRun(this);
        }
        else
        {
            codeInfo.Run(this);
        }
    }

    public void ContinueProcess()
    {
        _stopEvent.Set();
    }

    public void StopProcess()
    {
        _stopEvent.WaitOne();
    }

    public bool IsMove()
    {
        return _isMove;
    }

    public float GetMovementSpeed()
    {
        return _currentMovementSpeed;
    }

    public float GetRotationSpeed()
    {
        return _currentRotationSpeed;
    }

    public float GetMaxMovementSpeed()
    {
        return _maxMovementSpeed;
    }

    public float GetMaxRotationSpeed()
    {
        return _maxRotationSpeed;
    }

    public void SetMovementSpeed(float speed)
    {
        _currentMovementSpeed = Mathf.Clamp(speed, -_maxMovementSpeed, _maxMovementSpeed);
    }

    public void SetRotationSpeed(float speed)
    {
        _currentRotationSpeed = Mathf.Clamp(speed, -_maxRotationSpeed, _maxRotationSpeed);
    }

    public void StartMove(float time)
    {
        AddTaskAndWaitOneFrame(delegate() { Function_StartMove(time); });
    }

    public void StartRotate(float time)
    {
        AddTaskAndWaitOneFrame(delegate() { Function_StartRotate(time); });
    }

    public void Move(float distance)
    {
        AddTaskAndWaitToStart(delegate() { Function_Move(distance); });
    }

    public void Rotate(float deegres)
    {
        AddTaskAndWaitToStart(delegate() { Function_Rotate(deegres); });
    }

    public void Wait(float time)
    {
        AddTaskAndWaitToStart(delegate() { Function_Wait(time); });
    }

    public float Raycast()
    {
        return Raycast(0, 1);
    }

    public float Raycast(float x, float y)
    {
        float result = -2f;

        AddTaskAndWaitOneFrame(delegate() { Function_Raycast(x, y, out result); });

        return result;
    }

    public void Light()
    {
        AddTaskAndWaitOneFrame(delegate() { Function_Light(); });
    }

    public void Answer(float answer)
    {
        AddTaskAndWaitOneFrame(delegate() { Function_Answer(answer); });
    }

    public bool IsButtonPressed(string buttonName)
    {
        bool result = false;

        AddTaskAndWaitOneFrame(delegate() { Function_IsButtonPressed(buttonName, out result); });

        return result;
    }

    public Position GetPosition(string name)
    {
        Position result = null;

        AddTaskAndWaitOneFrame(delegate() { Function_GetPosition(name, out result); });

        return result;
    }

    public void Print(string message)
    {
        print(message);
    }

    private void Function_StartMove(float time)
    {
        _movementDistanceOrTimeLeft = time;

        _movementTypeIsTime = true;

        _isMove = true;
    }

    private void Function_StartRotate(float time)
    {
        _rotationPhase = 1;

        _rotationDeegresOrTimeLeft = time;

        _rotationTypeIsTime = true;
    }

    private void Function_Move(float distance)
    {
        _movementDistanceOrTimeLeft = distance;

        _movementLastPosition = new Vector2(transform.position.x, transform.position.z);

        _movementTypeIsTime = false;

        _isMove = true;
    }

    private void Function_Rotate(float deegres)
    {
        _rotationPhase = deegres > 0 ? 1 : -1;

        _rotationDeegresOrTimeLeft = Mathf.Abs(deegres);

        _rotationTypeIsTime = false;
    }

    private void Function_Wait(float seconds)
    {
        _waitEndTime = DateTime.UtcNow.AddSeconds(seconds);

        _isWait = true;

        _isMove = false;
        _rotationPhase = 0;
    }

    private void Function_Raycast(float x, float y, out float result)
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(x, 0, y), out hit))
        {
            result = hit.distance;
        }
        else
        {
            result = -1;
        }
    }

    private void Function_IsButtonPressed(string buttonName, out bool result)
    {
        var button = GameObject.FindObjectsOfType<NormalButton>().FirstOrDefault(x => x._name == buttonName);

        if (button == null)
        {
            result = false;

            GUIController.ShowMessage("Przycisk o nazwie \"" + buttonName + "\" nie istnieje. Zostanie zwrócona wartość 'false'!", MessageColor.Red);
        }
        else
        {
            result = button.IsPressed;
        }
    }

    private void Function_Light()
    {
        _blueLight.SetActive(!_blueLight.activeSelf);

        if (OnLightFunction != null) { OnLightFunction(); OnLightFunction = null; }
    }

    private void Function_Answer(float answer)
    {
        var win = GameObject.FindObjectOfType<WinAnswerCondition>();

        if (win == null)
        {
            GUIController.ShowMessage("Ta misja nie polega na podawaniu odpowiedzi", MessageColor.Yellow);
            return;
        }

        win.Answer(answer);
    }

    private void Function_GetPosition(string name, out Position result)
    {
        var pos = FindObjectsOfType<GetPositionName>().FirstOrDefault(x => x._name == name);

        if (pos == null)
        {
            result = new Position(0, 0);

            GUIController.ShowMessage("Obiekt o nazwie \"" + name + "\" nie istnieje. Zostanie zwrócona wartość (0, 0)!", MessageColor.Red);            
        }
        else
        {
            result = new Position(pos.transform.position.x, pos.transform.position.z);
        }
    }

    private void Update_Move()
    {
        var rigidbody = GetComponent<Rigidbody>();

        if (_isMove)
        {
            if (_movementDistanceOrTimeLeft <= 0)
            {
                _isMove = false;

                rigidbody.velocity = new Vector3(0, rigidbody.velocity.y, 0);

                if (!_movementTypeIsTime) ContinueProcess();            
            }
            else
            {
                if (_movementTypeIsTime)
                {
                    Vector3 velocity = transform.TransformDirection(Vector3.forward) * _currentMovementSpeed;

                    velocity.y = rigidbody.velocity.y;

                    rigidbody.velocity = velocity;

                    _movementDistanceOrTimeLeft -= Time.deltaTime;
                }
                else
                {
                    Vector3 velocity = transform.TransformDirection(Vector3.forward) * _currentMovementSpeed;

                    velocity.y = rigidbody.velocity.y;

                    rigidbody.velocity = velocity;

                    Vector2 currentPos = new Vector2(transform.position.x, transform.position.z);

                    float traveledDistance = (currentPos - _movementLastPosition).magnitude;

                    _movementDistanceOrTimeLeft -= traveledDistance;

                    _movementLastPosition = currentPos;
                }              
            }
        }
        else
        {
            rigidbody.velocity = new Vector3(0, rigidbody.velocity.y, 0);
        }
    }

    private void Update_Rotate()
    {
        if (_rotationPhase != 0)
        {
            if (_rotationDeegresOrTimeLeft <= 0)
            {
                _rotationPhase = 0;

                if(!_rotationTypeIsTime) ContinueProcess();
            }
            else
            {
                if (_rotationTypeIsTime)
                {
                    transform.Rotate(0, _currentRotationSpeed * Time.deltaTime, 0);

                    _rotationDeegresOrTimeLeft -= Time.deltaTime;
                }
                else
                {
                    float startRot = transform.eulerAngles.y;

                    transform.Rotate(0, _rotationPhase * _currentRotationSpeed * Time.deltaTime, 0);

                    float endRot = transform.eulerAngles.y;

                    _rotationDeegresOrTimeLeft -= Math.Abs(Mathf.DeltaAngle(startRot, endRot));
                }        
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

    private void AddTask(Action task)
    {
        lock (_tasks_lock)
        {
            _tasks.Add(task);
        }
    }

    protected void AddTaskAndWaitToStart(Action task)
    {
        AddTask(task);

        StopProcess();
    }

    protected void AddTaskAndWaitOneFrame(Action task)
    {
        AddTask(task);

        _isWaitOneFrame = true;

        StopProcess();
    }
}
