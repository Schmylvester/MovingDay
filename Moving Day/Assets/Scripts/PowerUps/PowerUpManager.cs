using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum Power_Ups
{
    Speed_Boost,    //They move more fastly
    Big_Strong,     //They lose less speed when moving objects
    Juggernaut,     //Objects can be easily pushed out of the way
    Weeble,         //Can't be knocked down
    Thief,          //They can interect with other people's stuff and it becomes their stuff

    COUNT
}


public class PowerUpManager : MonoBehaviour
{
    [SerializeField] Transform m_camera;
    [SerializeField] GameObject power_up_prefab;
    [SerializeField] Sprite[] sprites;
    [SerializeField] Transform[] min_max_points;

    [SerializeField] GameObject[] power_ups_3d;

    private void Awake()
    {
        StartCoroutine(spawner());
    }

    IEnumerator spawner()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(0.6f, 0.7f));
            float x = Random.Range(min_max_points[0].position.x, min_max_points[1].position.x);
            float z = Random.Range(min_max_points[0].position.z, min_max_points[1].position.z);
            spawnPower(new Vector3(x, 0, z));
        }
    }

    public void spawnPower(Vector3 pos, int power = -1)
    {
        if (power == -1)
            power = Random.Range(0, (int)Power_Ups.COUNT);

        GameObject power_up_instance = Instantiate(power_ups_3d[power], pos + new Vector3(0,0.5f,0), transform.rotation);
        power_up_instance.GetComponent<PowerUp>().setPower(power, sprites[power]);

        // GameObject power_up_instance = Instantiate(power_up_prefab, pos, m_camera.rotation);
        // power_up_instance.GetComponent<PowerUp>().setPower(power, sprites[power]);
    }
}