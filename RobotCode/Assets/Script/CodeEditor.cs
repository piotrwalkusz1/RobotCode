using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CodeEditor : MonoBehaviour
{
    public InputField _codeField;

    void Start()
    {


        _codeField.text = @"using System;
using RobotsLibrary;

public class Program : Robot
{
    public void Main()
    {
        Rotate(90);
        Move(1);
        Move(2);
        Wait(4);
        Move(3);
        Move(4);
    }

}
";
    }

    public void Compile()
    {
        Compiler.Main.CompileAndStartFunction(_codeField.text);
    }
}
