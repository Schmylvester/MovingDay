using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour
{
    private float lifespan = 0;

    private void Update()
    {
        lifespan += Time.deltaTime;

        if(lifespan > 5)
        {
            Destroy(this.gameObject);
        }
    }

    private void FixedUpdate()
    {
        transform.position += transform.right * (55 * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Wall")
        {
            Physics.IgnoreCollision(GetComponent<Collider>(), collision.collider);
        }
    }
}
