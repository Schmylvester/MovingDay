using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetFlagColours : MonoBehaviour
{
    [SerializeField] MeshRenderer[] flags;
    ObjectManager objectManager;

    private void Start()
    {
        objectManager = FindObjectOfType<ObjectManager>();
    }

    public void setFlagColours(int[] rooms)
    {
        for (int i = 0; i < rooms.Length; i++)
            Debug.Log(rooms[i]);
        for (int i = 0; i < flags.Length; i++)
            if (rooms[i] != -1)
            {
                flags[i].material = objectManager.getMaterial(rooms[i]);
            }
    }
}
