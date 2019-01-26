using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    ParticleSystem.MainModule particles;

    float spread_timer = 0;

    SphereCollider sphere_collider;

    private void Start()
    {
        sphere_collider = GetComponent<SphereCollider>();
        particles = GetComponent<ParticleSystem>().main;
    }

    private void Update()
    {
        if(spread_timer < 1.25)
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
        sphere_collider.radius += 0.5f;
        float scale_value = 0.25f * Time.deltaTime;
        Vector3 add_scale = new Vector3(scale_value, scale_value, scale_value);
        transform.localScale += add_scale;
        particles.startSpeed = new ParticleSystem.MinMaxCurve(2.5f, 10f);
        particles.startSize = new ParticleSystem.MinMaxCurve(5f, 10f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name != "Train" && other.gameObject.name != "Player")
        {
            if (other.gameObject.GetComponent<Rigidbody>() != null)
            {
                Destroy(other.gameObject);
            }
        }
    }
}
