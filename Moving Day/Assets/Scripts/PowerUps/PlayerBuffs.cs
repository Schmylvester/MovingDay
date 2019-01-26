using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuffs : MonoBehaviour
{
    [SerializeField] PowerUpEffect power_up;
    float[] power_timers;

    private void Awake()
    {
        power_timers = new float[(int)Power_Ups.COUNT] { 0, 0, 0, 0, 0 };
    }

    private void Update()
    {
        for (int i = 0; i < power_timers.Length; i++)
            if (power_timers[i] > 0)
                power_timers[i] -= Time.deltaTime;
    }

    public bool powerActive(Power_Ups power)
    {
        return power_timers[(int)power] > 0;
    }

    public void addPower(Power_Ups power, float time = 10)
    {
        power_timers[(int)power] = time;
        power_up.gameObject.SetActive(true);
        power_up.enablePower(power);
    }
}
