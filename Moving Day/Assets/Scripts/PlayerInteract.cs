﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public GameObject grabbedObj;
    [SerializeField] private Transform grabObjectPos;
    [SerializeField] PlayerMovement movement;

    private float dropOverLapDelay;
    bool grabbed;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //delay so drop input isnt dected instantly (could be couroutine wait???)
        if (dropOverLapDelay > 0.25f)
        {
            InputManager iM = FindObjectOfType<InputManager>();
            if (iM.buttonUp(XboxButton.B, GetComponent<PlayerMovement>().GetPlayerID()))
            {
                DropObject();
            }
        }
        else
        {
            if (grabbed)
            {
                dropOverLapDelay += Time.deltaTime;
            }
        }
    }


    void DropObject()
    {
        if (grabbedObj != null)
        {
            grabbedObj.transform.parent = null;
            grabbedObj.GetComponent<Rigidbody>().AddForce(GetComponent<PlayerMovement>().GetPlayerForceDirection(), ForceMode.Impulse);
            grabbedObj.GetComponent<Rigidbody>().useGravity = true;


            grabbedObj = null;
            grabbed = false;
            dropOverLapDelay = 0;

            grabbedObj.GetComponent<ObjectData>().putDown();
            movement.resetSpeed();
        }
    }


    void GrabObject(GameObject _grab_gobj)
    {
        if (grabbedObj == null)
        {
            if (_grab_gobj.GetComponent<InteractObject>() != null)
            {
                grabbedObj = _grab_gobj;
                grabbedObj.transform.parent = transform;
                grabbedObj.transform.rotation = transform.rotation;

                grabbedObj.GetComponent<InteractObject>().SetGrabbedPos(grabObjectPos.position);
                grabbedObj.GetComponent<Rigidbody>().useGravity = false;
                grabbed = true;

                ObjectData data = grabbedObj.GetComponent<ObjectData>();
                data.pickedUp();
                switch (data.getWeight())
                {
                    case WeightClass.Heavy:
                        movement.ChangeSpeed(2, 0.1f);
                        break;
                    case WeightClass.Medium:
                        movement.ChangeSpeed(3);
                        break;
                }
            }
        }
    }

    void OnTriggerStay(Collider _col)
    {
        InputManager iM = FindObjectOfType<InputManager>();
        if (iM.buttonDown(XboxButton.B, GetComponent<PlayerMovement>().GetPlayerID()))
        {
            GrabObject(_col.gameObject);
        }
    }
}
