using RobotsLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;

public class CodeInfo
{
    public Assembly CompiledAssembly
    {
        get
        {
            lock (_compiledAssembly_lock)
            {
                return _compiledAssembly;
            } 
        }
        set
        {
            lock (_compiledAssembly_lock)
            {
                _compiledAssembly = value; 
            }
        }
    }
    public string Code { get; set; }
    public bool IsEdited { get; set; }

    private Assembly _compiledAssembly;
    private object _compiledAssembly_lock = new object();

    public CodeInfo()
    {
        IsEdited = true;

        Code = @"using RobotsLibrary;

public class Program : Robot
{
    public void Main()
    {

    }
}";
    }

    public void Compile()
    {
        Compiler.Main.Compile(this);
    }

    public void Run(RealRobot realRobot)
    {
        if (CompiledAssembly == null)
        {
            throw new InvalidOperationException("There isn't any compiled assembly.");
        }

        Thread thread = new Thread(delegate() { Function_Run(realRobot); });

        thread.Start();
    }

    public void CompileAndRun(RealRobot realRobot)
    {
        Compiler.Main.CompileAndStartFunction(this, realRobot);
    }

    private void Function_Run(RealRobot realRobot)
    {
        var robotClass = CompiledAssembly.GetType("Program") as Type;

        var inst = Activator.CreateInstance(robotClass);

        typeof(Robot).GetField("_robot", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(inst, realRobot);

        robotClass.GetMethod("Main").Invoke(inst, null);
    }
}
