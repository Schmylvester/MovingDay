using UnityEngine;
using System.Collections;
using System.Collections.Generic;

enum VanState
{
    Driving_To,
    Delivering,
    Driving_From,
}

public class VanAI : MonoBehaviour
{
    [SerializeField] Transform[] path;
    [SerializeField] public float m_speed = 10;
    [SerializeField] float m_waitTime;
    float m_timer = 0;
    bool can_move = true;
    VanState state = VanState.Driving_To;

    // Use this for initialization
    void Start()
    {
        transform.position = path[0].position;
    }

    // Update is called once per frame
    void Update()
    {
        switch(state)
        {
            case VanState.Driving_To:
                if (!driveTo(path[1].position))
                    state = VanState.Delivering;
                break;
            case VanState.Delivering:
                makeDelivery();
                break;
            case VanState.Driving_From:
                if (!driveTo(path[2].position))
                    Destroy(gameObject);
                break;
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

    void makeDelivery()
    {
        GetComponent<Animator>().SetBool("IsOpen", true);
        GetComponentInChildren<FloodSpawner>().StartSpawning();

        m_timer += Time.deltaTime;
        if(m_timer > m_waitTime)
        {
            GetComponent<Animator>().SetBool("IsOpen", false);
            GetComponentInChildren<FloodSpawner>().StopSpawning();
            state = VanState.Driving_From;
        }
    }
}