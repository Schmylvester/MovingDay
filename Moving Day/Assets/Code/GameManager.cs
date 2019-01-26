using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool has_ended { get; set; }

    [SerializeField] private bool has_started = false;

    [SerializeField] private int clock_mins = 5;
    private float clock_secs = 0.0f;

    private float countdown_secs = 3.0f;

    private List<int> scores = new List<int>();

	// Use this for initialization
	void Awake ()
    {
        has_ended = false;
        SetGame();
	}
	
	// Update is called once per frame
	void Update ()
    {
        StartGame();
		if(has_started && !has_ended)
        {
            GameClock();
            GetPlayerScore(0);
        }
        else if(has_ended)
        {
            EndGame();
        }

        Debug.Log(GetTimer());
	}

    public string GetTimer()
    {
        float secs = (float)Mathf.Floor(clock_secs);
        return clock_mins.ToString() + ":" + secs.ToString("00");
    }

   void GameClock()
    {
        clock_secs -= Time.deltaTime;

        if (clock_secs <= 0)
        {
            if (clock_mins > 0)
            {
                clock_secs = 59.9f;
                clock_mins--;
            }
            else
            {
                clock_secs = 0;
                has_ended = true;
            }
        }
    }

    public void SetGame(int mins = 5, float secs = 0.0f, int player_count = 1)
    {
        SetPlayerCount(player_count);
        clock_mins = mins;
        clock_secs = secs;
    }

    void StartGame()
    {
        if (has_started) return;

        if (countdown_secs > 0)
        {
            countdown_secs -= Time.deltaTime;
        }
        else
        {
            has_started = true;
        }
    }

    void EndGame()
    {
        float high_score = float.MinValue;
        int winner_id = -1;

        for(uint i = 0; i < scores.Capacity; i++)
        {
            if(scores[(int)i] >= high_score)
            {
                high_score = scores[(int)i];
                winner_id = (int)i;
            }
        }

        Debug.Log("WINNER - " + winner_id);
    }

    public int GetPlayerScore(int id)
    {
        return scores[id];
    }

    public void SetPlayerCount(int player_count = 1)
    {
        scores.Capacity = player_count;
    }
}
