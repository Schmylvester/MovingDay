using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    ParticleSystem.Particle particles;

    float spread_timer = 0;

    SphereCollider sphere_collider;

    private void Start()
    {
        sphere_collider = GetComponent<SphereCollider>();
    }

    private void Update()
    {
        if(spread_timer < 10)
        {
            SpreadFire();
            spread_timer += Time.deltaTime;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void SpreadFire()
    {
        sphere_collider.radius += 0.1f * Time.deltaTime;
        particles.startSize += 0.1f;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if (other.gameObject.GetComponent<ObjectData>())
        {
            Destroy(other.gameObject);
        }
    }
}
