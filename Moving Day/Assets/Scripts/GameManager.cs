using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool has_ended { get; set; }

    [SerializeField] private bool has_started = false;
    [SerializeField] private EventsManager event_manager;
    [SerializeField] ScoreBar scoreBar;

    private int clock_mins = 5;
    private float clock_secs = 1.0f;

    private float countdown_secs = 3.0f;

    private List<int> scores = new List<int>();

	// Use this for initialization
	void Awake ()
    {
        SetGame();
        event_manager = GetComponent<EventsManager>();
        DontDestroyOnLoad(this.gameObject);
        has_ended = false; //Uses C#'s version of Getters and Setters - REQUIRED
    }
	
	// Update is called once per frame
	void Update ()
    {
        StartGame();
		if(has_started && !has_ended)
        {
            GameClock();
            event_manager.EventChecker(Time.deltaTime);
        }
        else if(has_ended)
        {
            EndGame();
        }
	}

    public string GetTimer()
    {
        float secs = (float)Mathf.Floor(clock_secs);

        string timer = countdown_secs.ToString("0");

        if(has_started)
        {
            timer = clock_mins.ToString() + ":" + secs.ToString("00");
        }

        return timer;
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

    public void SetGame(int mins = 5, float secs = 1.0f, int player_count = 1)
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

        for(uint i = 0; i < scores.Count; i++)
        {
            if(scores[(int)i] >= high_score)
            {
                high_score = scores[(int)i];
                winner_id = (int)i;
            }
        }

        Debug.Log("WINNER - " + winner_id);
    }

    public void ChangePlayerScore(int value, int id)
    {
        scores[id] += value;
        scoreBar.scoreUpdated();
    }

    public int GetPlayerScore(int id)
    {
        return scores[id];
    }

    //Resets Score List for New Game
    public void SetPlayerCount(int player_count = 1)
    {
        scores.Clear();
        for(uint i = 0; i < player_count; i++)
        {
            scores.Add(0);
        }
    }

    public int GetPlayerCount()
    {
        return scores.Count;
    }

    public string TimerMode()
    {
        if(countdown_secs > 0)
        {
            return "Countdown";
        }
        else
        {
            return "Timer";
        }
    }
}
