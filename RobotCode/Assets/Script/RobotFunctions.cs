using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RobotFunctions : MonoBehaviour
{
    public List<CodeInfo> Functions { get; set; }

	// Use this for initialization
	void Awake () 
    {
        Functions = new List<CodeInfo>();

        for (int i = 0; i < 4; i++)
        {
            Functions.Add(new CodeInfo());
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}
}
