using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public GameObject grabbedObj;
    [SerializeField] private Transform grabObjectPos;
    [SerializeField] PlayerMovement movement;
    [SerializeField] PlayerBuffs buffs;

    private float dropOverLapDelay;
    bool grabbed;


    [SerializeField] private Animator playerAnimator;

    private Vector3 gobjLocalPos;

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

	    if (grabbedObj != null)
	    {
	        grabbedObj.transform.localPosition = gobjLocalPos;
        }
    }


    public void DropObject()
    {
        if (grabbedObj != null)
        {
            grabbedObj.transform.parent = null;
            grabbedObj.GetComponent<Rigidbody>().AddForce(GetComponent<PlayerMovement>().GetPlayerForceDirection(), ForceMode.Impulse);
            grabbedObj.GetComponent<ObjectData>().putDown();
            movement.resetSpeed();

            grabbedObj = null;
            grabbed = false;
            dropOverLapDelay = 0;

            playerAnimator.SetBool("IsHolding", false);
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
                ObjectData objectData = grabbedObj.GetComponent<ObjectData>();
                if (buffs.powerActive(Power_Ups.Thief))
                {
                    objectData.setOwner(movement.GetPlayerID());
                }
                switch(objectData.getWeight())
                {
                    case WeightClass.Heavy:
                        movement.ChangeSpeed(1, 0.3f);
                        break;
                    case WeightClass.Medium:
                        movement.ChangeSpeed(2.5f);
                        break;
                }
                grabbed = true;

                gobjLocalPos = grabbedObj.transform.localPosition;

                playerAnimator.SetBool("IsHolding", true);
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


    public GameObject GetHeldObject()
    {
        if (grabbedObj != null)
        {
            return grabbedObj;
        }

        return null;
    }
}
