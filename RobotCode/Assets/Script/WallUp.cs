using UnityEngine;
using System.Collections;

public class WallUp : MonoBehaviour
{
    public float _targetY;

    public void GoUp()
    {
        StartCoroutine(Function_GoUp());
    }

    private IEnumerator Function_GoUp()
    {
        while (transform.position.y != _targetY)
        {
            transform.position = new Vector3(transform.position.x, Mathf.MoveTowards(transform.position.y, _targetY, Time.deltaTime), transform.position.z);

            yield return null;
        }
    }
}
