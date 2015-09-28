using UnityEngine;
using System.Collections;

public class ExplodeOnDestroy : MonoBehaviour
{
    public Detonator _detonator;

    public void OnDestroy()
    {
        _detonator.transform.SetParent(null);

        _detonator.Explode();
    }
}
