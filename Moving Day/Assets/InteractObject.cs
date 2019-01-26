using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractObject : MonoBehaviour
{
    [SerializeField] private Transform grabPoint;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// set hold object position
    /// </summary>
    /// <param name="_hands_pos">hand position for reference</param>
    public void SetGrabbedPos(Vector3 _hands_pos)
    {
        Vector3 offset = grabPoint.transform.position - transform.position;
        transform.position = _hands_pos + offset;
    }
}

