using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinnerStandings : MonoBehaviour
{
    [SerializeField] GameObject[] standers;
    private void Start()
    {
        GameManager gameManager = FindObjectOfType<GameManager>();
        ObjectManager objectManager = FindObjectOfType<ObjectManager>();
        for (int i = 0; i < gameManager.GetPlayerCount(); i++)
            standers[i].SetActive(true);
        standers[0].GetComponentInChildren<SkinnedMeshRenderer>().material =
        objectManager.getMaterial(gameManager.getWinner());
        List<int> losers = new List<int>();
        for (int i = 0; i < 4; i++)
            if (i != gameManager.getWinner())
                losers.Add(i);
        for (int i = 1; i < gameManager.GetPlayerCount(); i++)
        {
            standers[i].GetComponentInChildren<SkinnedMeshRenderer>().material =
            objectManager.getMaterial(losers[i - 1]);
        }
        standers[0].GetComponent<Animator>().SetBool("Jumping", true);
    }

}
