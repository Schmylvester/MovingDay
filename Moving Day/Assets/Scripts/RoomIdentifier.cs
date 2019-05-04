using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomIdentifier : MonoBehaviour
{

    [SerializeField] Transform[] RoomMins;
    [SerializeField] Transform[] RoomMaxs;

    public Vector3 getSpotInRoom(float y)
    {
        int idx = Random.Range(0, RoomMins.Length);
        float x = Random.Range(RoomMins[idx].position.x + 0.3f, RoomMaxs[idx].position.x - 0.3f);
        float z = Random.Range(RoomMins[idx].position.z + 0.3f, RoomMaxs[idx].position.z - 0.3f);
        return new Vector3(x, y, z);
    }

    public int InRoom(Vector3 position)
    {
        for (int i = 0; i < RoomMaxs.Length; i++)
        {
            if (position.x >= RoomMins[i].position.x
                && position.x < RoomMaxs[i].position.x
                && position.z >= RoomMins[i].position.z
                && position.z < RoomMaxs[i].position.z)
            {
                return i;
            }
        }
        return -1;
    }
}
