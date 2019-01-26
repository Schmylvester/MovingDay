using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBar : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] RectTransform[] bars;
    ushort playerCount = 0;
    bool score_happened = false;
    float[] target_scale;

    private void Start()
    {
        playerCount = (ushort)gameManager.GetPlayerCount();
        target_scale = new float[playerCount];
    }

    private void Update()
    {
        for (ushort i = 0; i < playerCount; i++)
        {
            bars[i].localScale += new Vector3(Time.deltaTime * (target_scale[i] - bars[i].localScale.x), 0, 0);
        }
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
            if (!score_happened)
            {
                score_happened = true;
                for (ushort i = 0; i < playerCount; i++)
                    bars[i].gameObject.SetActive(true);
            }
            float scorePortion = 0;
            for (ushort i = 0; i < playerCount; i++)
            {
                scorePortion += (float)gameManager.GetPlayerScore(i) / scoreSum;
                target_scale[i] = scorePortion;
            }
        }
    }
}
