using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Pathfinder : MonoBehaviour {
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Vector3[] targets;

	// Update is called once per frame
	void Update ()
    {
        if(Input.GetKeyDown(KeyCode.A))
            agent.SetDestination(targets[Random.Range(0, targets.Length)]);
	}
}
