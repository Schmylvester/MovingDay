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
                transform.Translate(knockForceDir * Time.deltaTime, Space.World);
               // transform.eulerAngles = Vector3.Lerp(transform.rotation.eulerAngles, lerpRotation, Time.deltaTime * 3);
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


    public void KnockYou(Vector3 dirForce)
    {
        if (!isKnocked)
        {
            knockForceDir = dirForce;
            knockTimer = 0;
            isKnocked = true;
            knockSlowDown = 0;

            playerAnimator.SetBool("KnockOver", true);
        }
    }


    void OnCollisionStay(Collision col)
    {
        if (col.gameObject.GetComponent<PlayerCollision>() != null)
        {
            Vector3 dir = col.transform.position - transform.position;
            dir = dir.normalized;

            col.gameObject.GetComponent<PlayerCollision>().KnockYou(dir);
            lerpRotation = Quaternion.LookRotation(dir, Vector3.up);

            Debug.Log("knock yo");
        }
    }
}
