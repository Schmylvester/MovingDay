using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private int playerID;

    [SerializeField] float default_max;
    [SerializeField] float default_accel;

    [SerializeField] float moveSpeed;
    [SerializeField] float speedUpRate;
    [SerializeField] float rotationSpeed;
    [SerializeField] private Animator playerAnimator;

    private Vector3 lastDirection;
    private float currentSpeed;
    private float startMoveSpeed;
    private float startSpeedUpRate;

    [SerializeField] float jumpMax;
    private bool grounded;
    [SerializeField] private Transform groundPoint;
    private bool jumping;
    private float jumpTarget;

    private float jumpSpeed = 0;

    private bool collided = false;
    float colTime = 0;
    private Vector3 colDir = Vector3.zero;

    void Start ()
    {
        moveSpeed = default_max;
        speedUpRate = default_accel;
        startMoveSpeed = moveSpeed;
        startSpeedUpRate = speedUpRate;

        GameObject.Find("Main Camera").GetComponent<CameraScript>().addPoint(this.gameObject);
    }


    void Update()
    {
        //movement stuff --
        InputManager iM = FindObjectOfType<InputManager>();

        Vector3 dir = new Vector3(iM.getAxis(Axis.Left_Horizontal, playerID), 0,
            iM.getAxis(Axis.Left_Vertical, playerID));

        dir = dir.normalized;
        lastDirection = dir;

        //speed up player
        if (dir != Vector3.zero && !GetComponent<PlayerCollision>().GetIsKnocked())
        {
            //set rotation to look at move direction
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir),
                Time.deltaTime * rotationSpeed);
            playerAnimator.SetBool("Walking", true);

            if (currentSpeed < moveSpeed)
            {
                currentSpeed += speedUpRate;
            }
            else
            {
                currentSpeed = moveSpeed;
            }
        }
        else
        {
            playerAnimator.SetBool("Walking", false);
            currentSpeed = 0;
        }

        //collision check
        if (collided)
        {
            colTime += Time.deltaTime;
            if (colTime > 0.05f)
            {
                collided = false;
                colTime = 0;
            }
            else
            {
                transform.Translate(colDir * Time.deltaTime, Space.World);
            }
        }
        else
        {
            transform.Translate(dir * Time.deltaTime * currentSpeed, Space.World);

        }


        //
        //jumping stuff
        grounded = Physics.Raycast(groundPoint.transform.position, -Vector3.up, 0.1f);
        if (grounded && !jumping)
        {
            playerAnimator.SetBool("Jumping", false);

            if (iM.buttonUp(XboxButton.A, GetComponent<PlayerMovement>().playerID))
            {
                jumping = true;
                grounded = false;

                jumpTarget = transform.position.y + 0.5f;

                jumpSpeed = 2.5f;

                playerAnimator.SetBool("Jumping", true);


            }
        }
        else
        {
            if (jumping)
            {            
                jumpSpeed -= 0.1f;
                transform.Translate(transform.up * Time.deltaTime * jumpSpeed, Space.World);

                if (transform.position.y >= jumpTarget)
                {
                    jumpSpeed += 0.1f;
                    jumping = false;
                }
            }
            else
            {
                jumpSpeed += 0.1f;
                transform.Translate(-transform.up * Time.deltaTime * jumpSpeed, Space.World);
            }
        }        
    }


    /// <summary>
    /// Set new maximum move speed for the player
    /// </summary>
    /// <param name="_max_move_speed">new max move speed</param>
    public void ChangeSpeed(float _max_move_speed)
    {
        moveSpeed = _max_move_speed;
    }

    /// <summary>
    /// Overload: Set new max move speed and speed up rate for the player
    /// </summary>
    /// <param name="_max_move_speed">new max move speed</param>
    /// <param name="_speed_up_rate">new speed up rate</param>
    public void ChangeSpeed(float _max_move_speed, float _speed_up_rate)
    {
        Debug.Log("Changing speed");
        moveSpeed = _max_move_speed;
        speedUpRate = _speed_up_rate;
    }

    public void resetSpeed()
    {
        moveSpeed = default_max;
        speedUpRate = default_accel;
    }

    /// <summary>
    /// Resets max move speed and speed up rate to default;
    /// </summary>
    void ResetSpeedsToDefault()
    {
        moveSpeed = startMoveSpeed;
        speedUpRate = startSpeedUpRate;

        if (currentSpeed > moveSpeed)
            currentSpeed = moveSpeed;
    }

    public void SetID(int id)
    {
        playerID = id;
    }

    /// <summary>
    /// returns player id
    /// </summary>
    /// <returns></returns>
    public int GetPlayerID()
    {
        return playerID;
    }

    bool areEightsBetterThanNines()
    {
        return true;
    }

    public Vector3 GetPlayerForceDirection()
    {
        return (lastDirection.normalized * currentSpeed) * 2;
    }


    void OnCollisionEnter(Collision col)
    {
        if (GetComponent<PlayerInteract>().GetHeldObject() != col.gameObject && col.gameObject.GetComponent<ObjectData>() == null && col.gameObject.GetComponent<PlayerMovement>() == null)
        {
            collided = true;
            currentSpeed = 0;
            colDir = col.transform.position - transform.forward;
        }
    }

    void OnCollisionStay(Collision col)
    {
        if (GetComponent<PlayerInteract>().GetHeldObject() != col.gameObject && col.gameObject.GetComponent<ObjectData>() == null && col.gameObject.GetComponent<PlayerMovement>() == null)
        {
            collided = true;
            currentSpeed = 0;
            colDir = col.transform.position - transform.forward;
        }
    }
}
