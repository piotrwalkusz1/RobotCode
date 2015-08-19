using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Prefabs : MonoBehaviour
{
    public static Prefabs Main { get; set; }

    void Awake()
    {
        if (Main == null)
        {
            Main = this;
        }
        else
        {
            print("There are two Prefabs at least.");
        }
    }
}
