using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    enum TYPE { CIRCLE,
        CUBE,
        SPHERE };

    public GameObject[] m_spawned; //this is the list objects that we wish to spawn from this spawner
    [SerializeField] TYPE m_type;
    [SerializeField] float m_radius = 1;
    [SerializeField] float m_cubeWidth = 1;
    [SerializeField] float m_cubeHeight = 1;
    [SerializeField] float m_cubeDepth = 1;


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        //if (Input.GetKey(KeyCode.Space))
        //{
        //    Spawn();
        //}
	}

#if(UNITY_EDITOR)
    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.white;
        switch (m_type)
        {
            case TYPE.CUBE:
                Gizmos.DrawWireCube(transform.position, new Vector3(m_cubeWidth, m_cubeHeight, m_cubeDepth));
                break;
            case TYPE.SPHERE:
                Gizmos.DrawWireSphere(transform.position, m_radius);
                break;
            case TYPE.CIRCLE:
                UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.up, m_radius);
                break;
        }
    }
#endif
    //always spawns the gameobject at the beginning of the array in the exact position of the spawner
    public void Spawn()
    {
        switch (m_type)
        {
            case TYPE.CIRCLE:
                SpawnCircleArea(0);
                break;
            case TYPE.SPHERE:
                SpawnSphereArea(0);
                break;
            case TYPE.CUBE:
                SpawnCubeArea(0);
                break;
        }
    }

    //Spawns a specific gameobject from the array
    public void Spawn(int _index)
    {
        //check that we can spawn this gameobject
        if (_index >= m_spawned.Length || _index < 0)
        {
            Debug.Log("Invalid index called for spawn");
        }
        else
        {
            switch (m_type)
            {
                case TYPE.CIRCLE:
                    SpawnCircleArea(_index);
                    break;
                case TYPE.SPHERE:
                    SpawnSphereArea(_index);
                    break;
                case TYPE.CUBE:
                    SpawnCubeArea(_index);
                    break;
            }
        }
    }

    //spawns a random gameobject from the list
    public void SpawnRandom()
    {
        int index = Random.Range(0, m_spawned.Length);

        //spawn based on type
        switch(m_type)
        {
            case TYPE.CIRCLE:
                SpawnCircleArea(index);
                break;
            case TYPE.SPHERE:
                SpawnSphereArea(index);
                break;
            case TYPE.CUBE:
                SpawnCubeArea(index);
                break;
        }
    }

    //spawn from random positions in a cube area
    void SpawnCubeArea(int _index)
    {
        Instantiate(m_spawned[_index], new Vector3(Random.Range(transform.position.x - m_cubeWidth / 2, transform.position.y + m_cubeWidth / 2),
            Random.Range(transform.position.y - m_cubeHeight / 2, transform.position.y + m_cubeWidth / 2),
            Random.Range(transform.position.z - m_cubeDepth / 2, transform.position.z + m_cubeDepth / 2)), transform.rotation);
    }

    //spawn from random 3d position in sphere
    void SpawnSphereArea(int _index)
    {
        Instantiate(m_spawned[_index], new Vector3(Random.Range(transform.position.x - m_radius, transform.position.y + m_radius),
            Random.Range(transform.position.y - m_radius, transform.position.y + m_radius),
            Random.Range(transform.position.z - m_radius, transform.position.z + m_radius)), transform.rotation);
    }

    //spawn from random position in a circle
    public void SpawnCircleArea(int _index)
    {
        Instantiate(m_spawned[_index], new Vector3(Random.Range(transform.position.x - m_radius, transform.position.y + m_radius),
            transform.position.y,
            Random.Range(transform.position.z - m_radius, transform.position.z + m_radius)), transform.rotation);
    }
}
