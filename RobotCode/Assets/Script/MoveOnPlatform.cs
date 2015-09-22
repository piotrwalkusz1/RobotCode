using UnityEngine;
using System.Collections;

public class MoveOnPlatform : MonoBehaviour
{
    public float _distanceTouch;

    private RaycastHit _hit;

	void Update ()
    {
        if (Physics.Raycast(transform.position, -Vector3.up, out _hit, _distanceTouch))
        {
            PlatformMove platform = _hit.collider.GetComponent<PlatformMove>();

            if (platform != null)
            {
                transform.SetParent(platform.transform);
            }
        }
        else
        {
            transform.SetParent(null);
        }
	}
}
