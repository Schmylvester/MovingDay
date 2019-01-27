using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraScript : MonoBehaviour {

	[SerializeField] List<GameObject> points;
	[SerializeField] Vector3 offset;
    CameraShake camera_shake;
    [SerializeField] float m_zoomFactor = 1;
    [SerializeField] float max_zoomDistance;
    [SerializeField] float min_zoomDistance;
    [SerializeField] float m_moveSpeed;
    [SerializeField] float m_zoomSpeed;
    Vector3 m_pointTo;

    // Use this for initialization
    void Start ()
    {
        camera_shake = FindObjectOfType<CameraShake>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		Vector3 midpoint = new Vector3(0,0,0);
		int count = 0;
		GameObject parent = transform.parent.gameObject;

		foreach (GameObject g in points)
		{
			midpoint += g.transform.position;
			count++;
		}

        midpoint = midpoint / count;

        midpoint.y = parent.transform.position.y;

        midpoint.z += offset.z;

        if (!camera_shake.isEarthquake())
        {
            float posStep = m_moveSpeed * Time.deltaTime;
            midpoint = Vector3.MoveTowards(parent.transform.position, midpoint, posStep);
            parent.transform.position = midpoint;
        }

        camera_shake.SetBasePoint(midpoint);

		//find the the two players that are furthest apart
		//get their distance and use that to calculate how far to zoom out

		float furthestDistance = 0;

		foreach (GameObject g in points) {
			foreach (GameObject h in points) {
				float distance = Vector3.Distance(g.transform.position, h.transform.position);
				if (distance > furthestDistance) {
					furthestDistance = distance;
				}
			}
		}

        if (furthestDistance < min_zoomDistance)
        {
            furthestDistance = min_zoomDistance;
        }

        if (furthestDistance > max_zoomDistance)
        {
            furthestDistance = max_zoomDistance;
        }

		Vector3 zoom = Vector3.zero;
		zoom.y += furthestDistance / m_zoomFactor;
		zoom.z -= furthestDistance / m_zoomFactor;
		this.transform.localPosition = zoom;
        //Debug.Log("This code is being run");

        m_pointTo = zoom;

        if (!camera_shake.isEarthquake())
        {
            GetComponent<Camera>().transform.LookAt(midpoint);
        }

        float step = m_zoomSpeed * Time.deltaTime;
        this.transform.localPosition = Vector3.MoveTowards(this.transform.localPosition, m_pointTo, step);
    }

    public void addPoint(GameObject _newPoint)
    {
        bool already_here = false;
        foreach (GameObject p in points)
        {
            if (p == _newPoint)
            {
                already_here = true;
            }
        }
        if (!already_here)
        {
            points.Add(_newPoint);
        }
    }

    public void removePoint(GameObject _removedPoint)
    {
        foreach (GameObject p in points)
        {
            if (p == _removedPoint)
            {
                points.Remove(_removedPoint);
                break;
            }
        }   
    }
}
