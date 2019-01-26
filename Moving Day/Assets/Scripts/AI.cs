using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{

    [SerializeField] List<GameObject> m_nodeList;
    [SerializeField] GameObject m_entrynode;
    [SerializeField] public float m_speed = 10;
    [SerializeField] float m_maxSpeed = 7;
    [SerializeField] float m_minSpeed = 4;
    [SerializeField] public int m_pathIndex = 0;
    [SerializeField] float m_maxWaitTimeSeconds = 5.0f;
    [SerializeField] float m_minWaitTimeSeconds = 1.0f;
    [SerializeField] int m_maxPathSize = 100;
    GameObject m_minigame;
    [SerializeField]Vector3 m_posTo;
    [SerializeField]bool m_waiting;
    [SerializeField] float m_waitTime;
    float m_timer = 0;
    bool can_move = true;

    // Use this for initialization
    void Start()
    {
        m_entrynode = GameObject.Find("FirstNode");
        m_posTo = m_entrynode.transform.position;
        Reset();
    }

    // Update is called once per frame
    void Update()
    {
        // move to the next node
        float step = m_speed * Time.deltaTime;
        if (can_move)
            transform.position = Vector3.MoveTowards(transform.position, m_posTo, step);

        //when we reach the node, go to the next node or wait for a bit
        if (transform.position == m_posTo)
        {
            Node node = m_nodeList[m_pathIndex].GetComponent<Node>();
            if (node.m_canStopOn)
            {
                m_waitTime = Random.Range(m_minWaitTimeSeconds, m_maxWaitTimeSeconds);
                m_waiting = true;
                m_speed = 0;
            }
            else
                m_waitTime = 0.001f;
            m_pathIndex++;



            //when we have gone back to the entry point, reset or kill the ai
            //comment out the thing we don't want to do
            if (m_pathIndex >= m_nodeList.Count)
            {
                //Reset();
                Destroy(this.gameObject);
            }
            else
                m_posTo = m_nodeList[m_pathIndex].GetComponent<Node>().transform.position;
        }

        if (m_waiting)
        {
            //here goes code for opening van
            GetComponent<Animator>().SetBool("IsOpen", true);
            GetComponentInChildren<FloodSpawner>().StartSpawning();
            m_timer += Time.deltaTime;
            if (m_timer >= m_waitTime)
            {
                m_timer = 0.0f;
                m_waiting = false;
                m_speed = Random.Range(m_minSpeed, m_maxSpeed);
                GetComponent<Animator>().SetBool("IsOpen", false);
                GetComponent<FloodSpawner>().StopSpawning();
            }
        }

    }

    private void Reset()
    {
        //clear everything
        m_pathIndex = 0;
        m_nodeList.Clear();
        m_nodeList.Add(m_entrynode);

        //create our randomly generated path
        int i = 0;
        bool ended = false;
        while (!ended)
        {
            //pick randomly from each available linked node
            int count = m_nodeList[m_nodeList.Count - 1].GetComponent<Node>().m_linkedNodes.Count;
            int newIndex = Random.Range(0, count);
            if (m_nodeList.Count > 1)
            {
                if (m_nodeList[m_nodeList.Count - 1].GetComponent<Node>().m_linkedNodes[newIndex] == m_nodeList[m_nodeList.Count - 2])
                {
                    //re roll if the next node matches the previous node
                    newIndex = Random.Range(0, count);
                }
            }
            m_nodeList.Add(m_nodeList[m_nodeList.Count - 1].GetComponent<Node>().m_linkedNodes[newIndex]);

            i++;


            if (i >= m_maxPathSize)
            {
                FindPath(m_nodeList[m_nodeList.Count - 1], m_entrynode);
                ended = true;
            }
            //need to implement some actual pathfinding back to the start when path reaches a certain length
            if (m_nodeList[m_nodeList.Count - 1] == m_entrynode)
            {
                ended = true;
            }
        }
        if (can_move)
            transform.position = m_posTo;
        m_speed = Random.Range(m_minSpeed, m_maxSpeed);
        m_posTo = m_nodeList[0].GetComponent<Node>().GetRandomPointTo();
    }


    struct pathStep
    {
        public GameObject node;
        public int steps;
    }
    void FindPath(GameObject startNode, GameObject endNode)
    {
        //this is quick and dirty and doesn't actually properly find the shortest path but it works for now!
        bool pathfound = false;
        List<pathStep> newPath = new List<pathStep>();
        pathStep toAdd;
        toAdd.node = startNode;
        toAdd.steps = 0;
        newPath.Add(toAdd);
        //int i = 0;

        List<GameObject> map = new List<GameObject>();
        map = startNode.GetComponent<Node>().MapNodes(map);
        //Debug.Log("map called");
        //for (int i = 0; i <map.Count; i++)
        //{
        //    Debug.Log(map[i].transform.position.ToString());
        //}
        while (!pathfound)
        {
            pathfound = true;
        }

        for (int i = 0; i < map.Count; i++)
        {
            m_nodeList.Add(map[i]);
            if (map[i] == endNode)
            {
                return;
            }
        }
    }

    public int getPathIndex()
    {
        return m_pathIndex;
    }
}
