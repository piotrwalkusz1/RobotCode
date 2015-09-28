using UnityEngine;
using System.Collections;
using System;

public class BulletTarget : MonoBehaviour
{
    public event Action OnDestroy;

    public void DestroyTarget()
    {
        if(OnDestroy != null)
        {
            OnDestroy();
            OnDestroy = null;
        }

        Destroy(gameObject);
    }
}
