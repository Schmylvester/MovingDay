﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideAudio : MonoBehaviour {

    

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision collision)
    {
        if(!collision.gameObject.CompareTag("Player"))
            GetComponent<AudioSource>().Play();
    }
}
