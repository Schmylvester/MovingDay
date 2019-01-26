using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour {

    enum SHAPE
    {
        CIRCLE,
        RECTANGLE
    }

    [HideInInspector] public bool has_minigame_on;
    public bool m_canStopOn;
    public List<GameObject> m_linkedNodes;
    [SerializeField] float m_radius = 0.5f;
    [SerializeField] float m_width = 0.5f;
    [SerializeField] float m_height = 0.5f;
    [SerializeField] SHAPE m_shape;

    public Vector3 GetRandomPointTo()
    {
        //if there's a problem then we just default to the position of the node
        Vector3 output = transform.position;
        switch (m_shape)
        {
            case SHAPE.CIRCLE:
                output = new Vector3(Random.Range(transform.position.x - m_radius, transform.position.x + m_radius), Random.Range(transform.position.y - m_radius, transform.position.y + m_radius), 0.0f);
                break;
            case SHAPE.RECTANGLE:
                output = new Vector3(Random.Range(transform.position.x - (m_width/2), transform.position.x + (m_width/2)), Random.Range(transform.position.y - (m_height/2), transform.position.y + (m_height/2)), 0.0f);
                break;
        }
        return output;
    }
#if (UNITY_EDITOR)

    void OnDrawGizmos()
    {
        Gizmos.color = Color.white;

        switch (m_shape)
        {
            case SHAPE.CIRCLE:
                UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.forward, m_radius);
                break;
            case SHAPE.RECTANGLE:
                        UnityEditor.Handles.DrawWireCube(transform.position, new Vector3(m_width, m_height, 0.1f));
                break;
        }

        for (int i = 0; i < m_linkedNodes.Count; i++)
        {
            UnityEditor.Handles.DrawLine(transform.position , m_linkedNodes[i].transform.position);
            //Vector3 mid = (transform.position + m_linkedNodes[i].transform.position) / 2;
            //Vector3 offset = transform.position;
            //offset.x += 0.5f;
            //offset.y += 0.5f;
            //UnityEditor.Handles.DrawLine(mid, offset);
        }
    }

#endif

    public List<GameObject> MapNodes(List<GameObject> map)
    {
        for (int i = 0; i < m_linkedNodes.Count; i++)
        {
            bool onList = false;
            for (int j = 0; j < map.Count; j++)
            {
                if (map[j].transform.position == m_linkedNodes[i].transform.position)
                {
                    onList = true;
                }
            }

            if (!onList)
            {
                map.Add(m_linkedNodes[i]);
                map = m_linkedNodes[i].GetComponent<Node>().MapNodes(map);
            }
        }

        return map;
    }
}
