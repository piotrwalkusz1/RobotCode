using UnityEngine;
using System.Collections;

public class PlayerProfilController : MonoBehaviour
{
    public static PlayerProfilController Main { get; set; }

    public static string Name { get; set; }

    public static SaveData SaveData { get; set; }

    public void Awake()
    {
        if (Main == null)
        {
            Main = this;
        }
        else
        {
            print("There are at least two PlayerProfilController");
        }
    }

    public static void Save()
    {
        SaveController.Save(SaveData, Name);
    }
}
