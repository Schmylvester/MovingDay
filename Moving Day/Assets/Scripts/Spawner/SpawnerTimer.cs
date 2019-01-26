using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerTimer : MonoBehaviour {

    [SerializeField] float m_maxFrequencySeconds = 5.0f;
    [SerializeField] float m_minFrequencySeconds = 1.0f;
    float m_elapsedTime = 0;
    float m_timer = 0;
	// Use this for initialization
	void Start () {
        m_timer = Random.Range(m_minFrequencySeconds, m_maxFrequencySeconds);
	}
	
	// Update is called once per frame
	void Update () {
        m_elapsedTime += Time.deltaTime;

        if (m_elapsedTime >= m_timer)
        {
            m_elapsedTime = 0;
            m_timer = Random.Range(m_minFrequencySeconds, m_maxFrequencySeconds);
            GetComponent<Spawner>().Spawn();
        }
	}
}
