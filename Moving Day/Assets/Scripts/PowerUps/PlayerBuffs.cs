using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuffs : MonoBehaviour
{
    [SerializeField] GameObject[] particle_systems;
    float[] power_timers;

    private void Awake()
    {
        power_timers = new float[(int)Power_Ups.COUNT] { 0, 0, 0, 0, 0 };
    }

    private void Update()
    {
        for (int i = 0; i < particle_systems.Length; i++)
            if (power_timers[i] > 0)
                power_timers[i] -= Time.deltaTime;
            else if(particle_systems[i].activeSelf == true)
                particle_systems[i].SetActive(false);
    }

    public bool powerActive(Power_Ups power)
    {
        return power_timers[(int)power] > 0;
    }

    public void addPower(Power_Ups power, float time = 10)
    {
        power_timers[(int)power] = time;
        particle_systems[(int)power].SetActive(true);
    }
}
