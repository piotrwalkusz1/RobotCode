using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.UI;

public class CheckSave : MonoBehaviour 
{
    void Start()
    {
        if (!SaveController.GetAllSaves().Contains("Save"))
        {
            GetComponent<Button>().interactable = false;
        }
    }
}
