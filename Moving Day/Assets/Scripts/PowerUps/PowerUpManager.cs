using UnityEngine;

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
    [SerializeField] GameObject power_up_prefab;
    [SerializeField] Sprite[] sprites;

    private void Awake()
    {
        spawnPower(new Vector3(Random.Range(-8.0f, 8.0f), Random.Range(-4.0f, 4.0f)));
        spawnPower(new Vector3(Random.Range(-8.0f, 8.0f), Random.Range(-4.0f, 4.0f)));
        spawnPower(new Vector3(Random.Range(-8.0f, 8.0f), Random.Range(-4.0f, 4.0f)));
        spawnPower(new Vector3(Random.Range(-8.0f, 8.0f), Random.Range(-4.0f, 4.0f)));
        spawnPower(new Vector3(Random.Range(-8.0f, 8.0f), Random.Range(-4.0f, 4.0f)));
    }

    public void spawnPower(Vector3 pos, int power = -1)
    {
        if (power == -1)
            power = Random.Range(0, (int)Power_Ups.COUNT);
        GameObject power_up_instance = Instantiate(power_up_prefab, pos, new Quaternion());
        power_up_instance.GetComponent<PowerUp>().setPower(power, sprites[power]);
    }
}