using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloodSpawner : MonoBehaviour {

    [SerializeField] int m_numberToSpawn;
    [SerializeField] Vector3 force;
    [SerializeField] float m_delayBetweenSpawns;
    float m_timer = 0;
    int m_numberSpawned = 0;
    bool spawning;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (spawning)
        {
            m_timer += Time.deltaTime;
            if (m_timer > m_delayBetweenSpawns)
            {
                //do a spawn
                if (m_numberSpawned < m_numberToSpawn)
                {
                    Vector3 newForce = transform.right * -force.x;
                    newForce.y += force.y;
                    m_numberSpawned++;
                    newForce.x = Mathf.Min(newForce.x, 20);
                    GetComponent<Spawner>().SpawnRandom(newForce);
                }
                m_timer = 0;
            }
        }
	}

    public void StartSpawning()
    {
        spawning = true;
    }

    public void StopSpawning()
    {
        spawning = false;
    }
}
