using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBall : MonoBehaviour {

    [SerializeField] TrailRenderer trail;
    [SerializeField] float orbitRange;
    [SerializeField] float orbitSpeed;
    Vector3 plane;
    bool clockwise;

    private void OnEnable()
    {        
        plane = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
        clockwise = Random.Range(0, 2) == 0;
        plane.Normalize();
    }

    private void Update()
    {
        orbit();
    }

    void orbit()
    {
        Vector3 dir_to_origin = (transform.position - transform.parent.position);
        dir_to_origin.Normalize();
        Vector3 dir_to_move = Vector3.Cross(dir_to_origin, plane);
        if (!clockwise)
            dir_to_move *= -1;
        transform.position += dir_to_move * Time.deltaTime * orbitSpeed;
        dir_to_origin = (transform.position - transform.parent.position);
        dir_to_origin.Normalize();
        transform.position = transform.parent.position + (dir_to_origin * orbitRange);
    }

    public void setColour(Color col)
    {
        trail.startColor = (col);
        trail.endColor = Color.clear;
    }
}
