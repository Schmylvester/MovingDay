using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    [SerializeField] GameObject[] object_prefabs;
    [SerializeField] Material[] player_mats;

    private void Start()
    {
        StartCoroutine(spawn());
    }

    IEnumerator spawn()
    {
        while (true)
        {
            spawnObject(Random.Range(0, 4), Random.Range(0, object_prefabs.Length));
            yield return new WaitForSeconds(0.42f);
        }
    }

    void spawnObject(int player, int _object)
    {
        GameObject instance = Instantiate(object_prefabs[_object]);
        instance.GetComponent<ObjectData>().setOwner(player, player_mats[player]);
    }
}