using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public bool isKnocked;
    public float knockTimer;

    public Vector3 knockForceDir;
    //private float knockImmunityTimer;

    [SerializeField] private Animator playerAnimator;

    private float knockSlowDown;

    private Quaternion lerpRotation;

    private float knockForceMultiplier;


    // Use this for initialization
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {
        if (isKnocked)
        {
            Debug.Log("IS KNOCKED");
            if (knockTimer < 1)
            {
                knockTimer += Time.deltaTime;
                knockForceMultiplier -= knockTimer / 10;

                transform.Translate(knockForceDir * Time.deltaTime / 3 * knockForceMultiplier, Space.World);

                transform.rotation = Quaternion.Lerp(transform.rotation, lerpRotation, Time.deltaTime * 7.5f);
            }
            else
            {
                isKnocked = false;
                playerAnimator.SetBool("KnockOver", false);
            }
        }
    }


    public bool GetIsKnocked()
    {
        return isKnocked;
    }


    public void KnockYou(Vector3 dirForce, WeightClass weight)
    {
        if (!isKnocked)
        {
            knockForceDir = dirForce;
            knockTimer = 0;
            isKnocked = true;
            knockSlowDown = 1;

            playerAnimator.SetBool("KnockOver", true);
        }

        knockForceMultiplier = 1;

        switch (weight)
        {
            case WeightClass.Light:
                knockForceMultiplier = 3f;
                break;

            case WeightClass.Medium:
                knockForceMultiplier = 5f;
                break;

            case WeightClass.Heavy:
                knockForceMultiplier = 7f;
                break;
        }

    }


    void OnCollisionStay(Collision col)
    {
        if (col.gameObject.GetComponent<PlayerCollision>() != null)
        {
            Vector3 dir = col.transform.position - transform.position;
            dir = dir.normalized;

            WeightClass kg = WeightClass.None;

            if (GetComponent<PlayerInteract>().GetHeldObject() != null)
            {
                kg = GetComponent<PlayerInteract>().GetHeldObject().GetComponent<ObjectData>().getWeight();
            }

            col.gameObject.GetComponent<PlayerCollision>().KnockYou(dir, kg);

            lerpRotation = Quaternion.LookRotation(dir, Vector3.up);

            Debug.Log("knock yo");
        }
    }
}
