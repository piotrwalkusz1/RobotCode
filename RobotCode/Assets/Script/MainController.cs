using UnityEngine;
using System.Collections;
using System.Reflection;
using System;
using RobotsLibrary;
using System.Threading;

public class MainController : MonoBehaviour
{
    public RealRobot _realRobot;

    private string _code = @"
                        using System;
                        using RobotsLibrary;

                        public class Program : Robot
                        {
                            public void Main()
                            {
                                Rotate(90);
                                Move(1);
                                Fun();
                                Move(2);
                                Wait(4);
                                Move(3);
                                Move(4);
                            }

                            private void Fun() {Rotate(-30); Wait(2); Move(2.5f);}
                        }
                    ";

    private Thread _robotThread;

    void Start()
    {
        var compiler = Assembly.LoadFile(Application.dataPath + "/DynamicPlugin/Compiler.dll");

        var type = compiler.GetType("Compiler.Compiler");

        var method = type.GetMethod("Compile");

        string referencedLibraryPath = Application.dataPath + "/Plugin/RobotsLibrary.dll";

        var assembly = method.Invoke(null, new object[] { _code, referencedLibraryPath }) as Assembly;

        var robotClass = assembly.GetType("Program") as Type;

        var inst = Activator.CreateInstance(robotClass);

        typeof(Robot).GetField("_robot", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(inst, _realRobot);

        _robotThread = new Thread(delegate() { ExecuteFunction(robotClass, inst); });

        _robotThread.Start();
    }

    void Update()
    {

    }

    void ExecuteFunction(Type type, object instantiate)
    {
        type.GetMethod("Main").Invoke(instantiate, null);
    }
}
