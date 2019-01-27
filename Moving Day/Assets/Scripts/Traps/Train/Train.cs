using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum TrainState
{
    Going_To,
    Going_From,
}

public class Train : MonoBehaviour
{
    [SerializeField] Transform[] path;
    [SerializeField] public float m_speed = 10;
    float m_timer = 0;
    bool can_move = true;
    float lifetime = 0;
    TrainState state = TrainState.Going_To;

    // Use this for initialization
    void Start()
    {
        transform.position = path[0].position;
    }

    // Update is called once per frame
    void Update()
    {
        lifetime += Time.deltaTime;

        switch (state)
        {
            case TrainState.Going_To:
                if(!driveTo(path[0].position))
                    state = TrainState.Going_From;
                break;
            case TrainState.Going_From:
                if (!driveTo(path[1].position))
                    Destroy(gameObject);
                break;
        }

        if(lifetime > 1.75)
        {
            Destroy(gameObject);
        }

    }

    bool driveTo(Vector3 target)
    {
        if (Vector3.Distance(transform.position, target) > 0.05f)
        {
            Vector3 direction = target - transform.position;
            direction.Normalize();
            transform.position += direction * m_speed * Time.deltaTime;
            return true;
        }
        else
            return false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Wall" || collision.gameObject.name == "Van")
        {
            Physics.IgnoreCollision(GetComponent<Collider>(), collision.collider);
        }
    }
}
