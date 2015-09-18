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

    public static Action OnCompile;

    private MethodInfo _compileMethod;

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

    public void Compile(CodeInfo codeInfo)
    {
        Thread thread = new Thread(delegate() { Function_Compile(codeInfo); });

        thread.Start();
    }
    

    public void CompileAndStartFunction(CodeInfo codeInfo, RealRobot realRobot)
    {
        Thread thread = new Thread(delegate() { Function_CompileAndStartFunction(codeInfo, realRobot); });

        thread.Start();
    }

    private void Function_Compile(CodeInfo codeInfo)
    {
        try
        {
            var assembly = _compileMethod.Invoke(null, new object[] { codeInfo.Code, COMPILER_PATH }) as Assembly;

            codeInfo.CompiledAssembly = assembly;

            codeInfo.IsEdited = false;

            if (OnCompile != null) OnCompile();
        }
        catch (TargetInvocationException targertException)
        {
            Exception exception = targertException.InnerException;

            var errors = (List<string>)exception.Data["Errors"];

            if (errors == null)
            {
                print("Compilation has failed");

                return;
            }

            /*foreach (string error in errors)
            {
                string[] errorData = error.Split(new string[] { " @ " }, StringSplitOptions.None);

                //print("Line: " + errorData[0] + ", Column: " + errorData[1] + " : " + errorData[2]);

                ShowErrorMessage(errorData);
            }*/

            if (errors.Count > 0)
            {
                string[] errorData = errors[0].Split(new string[] { " @ " }, StringSplitOptions.None);

                ShowErrorMessage(errorData);
            }
        }
    }

    private void Function_CompileAndStartFunction(CodeInfo codeInfo, RealRobot realRobot)
    {
        try
        {
            var assembly = _compileMethod.Invoke(null, new object[] { codeInfo.Code, COMPILER_PATH }) as Assembly;

            codeInfo.CompiledAssembly = assembly;

            codeInfo.IsEdited = false;

            if (OnCompile != null) OnCompile();

            var robotClass = assembly.GetType("Program") as Type;

            var inst = Activator.CreateInstance(robotClass);

            typeof(Robot).GetField("_robot", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(inst, realRobot);

            robotClass.GetMethod("Main").Invoke(inst, null);
        }
        catch (TargetInvocationException targertException)
        {
            Exception exception = targertException.InnerException;

            var errors = (List<string>)exception.Data["Errors"];

            if (errors == null)
            {
                print("Compilation has failed");

                return;
            }

            /*foreach (string error in errors)
            {
                string[] errorData = error.Split(new string[] { " @ " }, StringSplitOptions.None);

                //print("Line: " + errorData[0] + ", Column: " + errorData[1] + " : " + errorData[2]);

                ShowErrorMessage(errorData);
            }*/

            if (errors.Count > 0)
            {
                string[] errorData = errors[0].Split(new string[] { " @ " }, StringSplitOptions.None);

                ShowErrorMessage(errorData);
            }
        }
    }

    private void ShowErrorMessage(string[] errorData)
    {
        MainController.UpdateEvent += delegate() { GUIController.ShowMessage("Wiersz " + errorData[0] + " : " + errorData[2], MessageColor.Red); };
    }
}
