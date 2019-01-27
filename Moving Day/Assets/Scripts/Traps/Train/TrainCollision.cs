using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainCollision : MonoBehaviour
{ 
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Wall" || collision.gameObject.name == "Van")
        {
            Physics.IgnoreCollision(GetComponent<Collider>(), collision.collider);
        }
    }
}
