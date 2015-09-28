using UnityEngine;
using System.Collections;

public class DestroyDefeat : MonoBehaviour
{
    public void Start()
    {
        GetComponent<BulletTarget>().OnDestroy += ActionOnDestroy;
    }

    public void ActionOnDestroy()
    {
        MainController.Defeat();
    }
}
