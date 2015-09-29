using UnityEngine;
using System.Collections;

public class MusicOnDestroy : MonoBehaviour
{
    void Start()
    {
        GetComponentInParent<BulletTarget>().OnDestroy += ActionOnDestroy;
    }

    public void ActionOnDestroy()
    {
        transform.SetParent(null);

        GetComponent<AudioSource>().Play();
    }
}
