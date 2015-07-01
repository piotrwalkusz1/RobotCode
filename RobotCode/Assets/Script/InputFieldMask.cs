using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InputFieldMask : MonoBehaviour
{
    void Update()
    {
        foreach (var children in gameObject.GetComponentsInChildren<Transform>())
        {
            if (children != transform && !children.GetComponent<Text>())
            {
                children.gameObject.AddComponent<Image>();

                this.enabled = false;
            }
        }
    }
}
