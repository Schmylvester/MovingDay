using System.Collections.Generic;
using UnityEngine;

public class WinnerStandings : MonoBehaviour
{
    [SerializeField] GameObject[] winners_objects;
    [SerializeField] GameObject[] losers_objects;
    private void Start()
    {
        GameManager gameManager = FindObjectOfType<GameManager>();
        ObjectManager objectManager = FindObjectOfType<ObjectManager>();
        List<int> winners = gameManager.getWinners();
        List<int> losers = new List<int>();
        for (int i = 0; i < gameManager.GetPlayerCount(); i++)
            if (!winners.Contains(i))
                losers.Add(i);
        for (int i = 0; i < winners.Count; i++)
        {
            winners_objects[i].SetActive(true);
            winners_objects[i].GetComponentInChildren<SkinnedMeshRenderer>().material = objectManager.getMaterial(winners[i]);
        }
        for(int i = 0; i < losers.Count; i++)
        {
            losers_objects[i].SetActive(true);
            losers_objects[i].GetComponentInChildren<SkinnedMeshRenderer>().material = objectManager.getMaterial(losers[i]);
        }
    }
}
