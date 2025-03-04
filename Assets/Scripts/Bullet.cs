using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float timeLife = 4;

    public ParticleSystem fire;

    public Action OnTrigger;

    private void Awake()
    {
        Destroy(gameObject, timeLife);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "UFO")
        {
            Instantiate(fire, transform.position, Quaternion.identity);

            Destroy(gameObject, .1f);

            Destroy(collision.gameObject, .1f);

            OnTrigger?.Invoke();
        }
    }

    public void InitBullet(Action action)
    {
        OnTrigger = action;
    }
}
