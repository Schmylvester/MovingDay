using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpEffect : MonoBehaviour
{

    [SerializeField] Color[] power_colours;
    [SerializeField] PlayerBuffs player;
    PowerUpBall[] balls;
    bool[] powers_active;

    private void Awake()
    {
        balls = GetComponentsInChildren<PowerUpBall>();
        powers_active = new bool[(int)Power_Ups.COUNT];
        for (int i = 0; i < (int)Power_Ups.COUNT; i++)
            powers_active[i] = false;
    }

    private void Update()
    {
        bool any_power = false;
        for (int i = 0; i < (int)Power_Ups.COUNT; i++)
        {
            if (player.powerActive((Power_Ups)i))
                any_power = true;
            else
                powers_active[i] = false;
        }
        if (!any_power)
            gameObject.SetActive(false);
        else
        {
            List<int> powers_list = new List<int>();
            for(int i = 0; i < powers_active.Length; i++)
            {
                if (powers_active[i])
                    powers_list.Add(i);
            }
            for(int i = 0; i < balls.Length; i++)
            {
                balls[i].setColour(power_colours[i % powers_list.Count]);
            }
        }
    }

    public void enablePower(Power_Ups power)
    {
        powers_active[(int)power] = true;
    }
}
