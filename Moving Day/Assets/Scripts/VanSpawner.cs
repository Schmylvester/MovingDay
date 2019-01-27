using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanSpawner : MonoBehaviour
{
    [SerializeField] Transform[] spawn_transforms;
    [SerializeField] GameObject van_prefab;
    [SerializeField] Vector2 spawn_delay;

    public IEnumerator startSpawningVans()
    {
        while (gameObject.activeSelf)
        {
            Transform spawn = spawn_transforms[Random.Range(0, spawn_transforms.Length)];
            Instantiate(van_prefab, spawn.position, spawn.rotation);
            yield return new WaitForSeconds(Random.Range(spawn_delay.x, spawn_delay.y));
        }
    }
}
