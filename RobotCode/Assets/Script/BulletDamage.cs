using UnityEngine;
using System.Collections;

public class BulletDamage : MonoBehaviour
{
    public float _speed;
    public float _lifeTime;

    private float _time;

    void Update()
    {
        if (_time > _lifeTime)
        {
            Destroy(this);
        }
        else
        {
            _time += Time.deltaTime;
        }

        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<BulletTarget>() != null)
        {
            other.GetComponent<BulletTarget>().DestroyTarget();

            Destroy(gameObject);
        }
        else if (!other.isTrigger)
        {
            Destroy(gameObject);
        }
    }
}
