using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    [SerializeField] Material[] player_mats;
    
    public Material getMaterial(int i)
    {
        return player_mats[i];
    }
}