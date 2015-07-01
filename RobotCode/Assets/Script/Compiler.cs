using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System;
using RobotsLibrary;
using System.CodeDom.Compiler;

public class Compiler : MonoBehaviour
{
    public static Compiler Main { get; set; }

    public RealRobot _realRobot;

    private MethodInfo _compileMethod;

    private Thread _robotThread;

    private string COMPILER_PATH;

    void Awake()
    {
        COMPILER_PATH = Application.dataPath + "/Plugin/RobotsLibrary.dll";

        if (Main == null)
        {
            Main = this;
        }
        else
        {
            print("There are two Comilers at least");
        }

        var compiler = Assembly.LoadFile(Application.dataPath + "/DynamicPlugin/Compiler.dll");

        var type = compiler.GetType("Compiler.Compiler");

        var method = type.GetMethod("Compile");

        _compileMethod = method;
    }

    public void CompileAndStartFunction(string code)
    {
        _robotThread = new Thread(delegate() { Function_CompileAndStartFunction(code); });

        _robotThread.Start();
    }

    private void Function_CompileAndStartFunction(string code)
    {
        try
        {
            var assembly = _compileMethod.Invoke(null, new object[] { code, COMPILER_PATH }) as Assembly;

            var robotClass = assembly.GetType("Program") as Type;

            var inst = Activator.CreateInstance(robotClass);

            typeof(Robot).GetField("_robot", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(inst, _realRobot);

            robotClass.GetMethod("Main").Invoke(inst, null);
        }
        catch (TargetInvocationException targertException)
        {
            Exception exception = targertException.InnerException;

            var errors = (List<string>)exception.Data["Errors"];

            if (errors == null)
            {
                print("Compilation hase failed");

                return;
            }

            foreach (string error in errors)
            {
                string[] errorData = error.Split(new string[] { " @ " }, StringSplitOptions.None);

                print("Line: " + errorData[0] + ", Column: " + errorData[1] + " : " + errorData[2]);
            }
        }
    }
}
