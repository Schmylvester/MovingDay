using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraScript : MonoBehaviour {

	[SerializeField] List<GameObject> points;
	[SerializeField] Vector3 offset;

    CameraShake camera_shake;

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
            parent.transform.position = midpoint;
        }

        camera_shake.SetBasePoint(midpoint);
        Debug.Log(midpoint);

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

		Vector3 zoom = Vector3.zero;
		zoom.y += furthestDistance / 2;
		zoom.z -= furthestDistance / 2;
		this.transform.localPosition = zoom;

        GetComponent<Camera>().transform.LookAt(midpoint);

	}

    public void addPoint(GameObject _newPoint)
    {
        points.Add(_newPoint);
    }

    public void removePoint(GameObject _removedPoint)
    {
        foreach (GameObject p in points)
        {
            if (p == _removedPoint)
            {
                points.Remove(_removedPoint);
            }
        }
            
    }
}
