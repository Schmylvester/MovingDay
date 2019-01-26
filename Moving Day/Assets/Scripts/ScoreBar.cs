using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBar : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] RectTransform[] bars;
    ushort playerCount = 0;

    private void Start()
    {
        playerCount = (ushort)gameManager.GetPlayerCount();
        for (ushort i = 0; i < playerCount; i++)
            bars[i].gameObject.SetActive(true);
    }

    public void scoreUpdated()
    {
        int scoreSum = 0;
        for (ushort i = 0; i < playerCount; i++)
        {
            scoreSum += gameManager.GetPlayerScore(i);
        }
        if (scoreSum != 0)
        {
            float scorePortion = 0;
            for (ushort i = 0; i < playerCount; i++)
            {
                scorePortion += (float)gameManager.GetPlayerScore(i) / scoreSum;
                bars[i].localScale = new Vector3(scorePortion, 1, 1);
            }
        }
    }
}
