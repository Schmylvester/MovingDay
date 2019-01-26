using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempPlayer : MonoBehaviour
{
    [SerializeField] private int playerID;
    [SerializeField] float moveSpeed;
    [SerializeField] float speedUpRate;
    [SerializeField] float rotationSpeed;

    private float currentSpeed;

    private float startMoveSpeed;
    private float startSpeedUpRate;
    
    void Start()
    {
        startMoveSpeed = moveSpeed;
        startSpeedUpRate = speedUpRate;
    }

    void Update()
    {
        InputManager iM = FindObjectOfType<InputManager>();
        Vector3 dir = new Vector3(iM.getAxis(Axis.Left_Horizontal, playerID), 0, iM.getAxis(Axis.Left_Vertical, playerID));

        //speed up player
        if (dir != Vector3.zero)
        {
            //set rotation to look at move direction
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * rotationSpeed);
            
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
            currentSpeed = 0;
        }

        transform.Translate(dir * Time.deltaTime * currentSpeed, Space.World);
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
        moveSpeed = _max_move_speed;
        speedUpRate = _speed_up_rate;
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

}