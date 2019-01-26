using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool has_ended { get; set; }

    [Header("Spawn")]
    [SerializeField] private Transform[] spawn_points;
    [SerializeField] private GameObject player_prefab;

    [Header("Managers")]
    [SerializeField] private EventsManager event_manager;
    [SerializeField] private CameraScript camera_script;

    [Header("")]
    [SerializeField] private bool has_started = false;
    [SerializeField] private GameObject[] players;
    [SerializeField] ScoreBar scoreBar;

    private int clock_mins = 5;
    private float clock_secs = 0.0f;

    private float countdown_secs = 3.0f;

    private List<int> scores = new List<int>();

	// Use this for initialization
	void Awake ()
    {
        SetPlayerColours();
        scoreBar = FindObjectOfType<ScoreBar>();
        camera_script = FindObjectOfType<CameraScript>();
        SetGame(5, 1.0f, 1);
        event_manager = GetComponent<EventsManager>();
        DontDestroyOnLoad(this.gameObject);
        has_ended = false; //Uses C#'s version of Getters and Setters - REQUIRED
    }
    
	// Update is called every frame where 8s are better than 9s
	void Update ()
    {
        StartGame();
		if(has_started && !has_ended)
        {
            GameClock();
            scoreBar.scoreUpdated();
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
        ResetPlayerCount(player_count);
        clock_mins = mins;
        clock_secs = secs;
        countdown_secs = 3.0f;
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

    //Resets Player List and Scores for New Game
    public void ResetPlayerCount(int player_count = 1)
    {
        scores.Clear();
        for(uint i = 0; i < player_count; i++)
        {
            scores.Add(0);
        }
        InitialisePlayers(player_count);
    }

    void InitialisePlayers(int player_count)
    {
        for(uint i = 0; i < player_count; i++)
        {
            GameObject player = Instantiate(player_prefab, spawn_points[i].position, spawn_points[i].rotation);
            camera_script.addPoint(player);
        }
        SetPlayerColours();

        scoreBar.ChangePlayerCount();

        has_started = false;
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

    Color PlayerColour(int id)
    {
        switch(id)
        {
            case 1:
                return Color.red;
            case 2:
                Color purple = new Vector4(0.5849f, 0, 0.5802f, 1);
                return purple;
            case 3:
                Color orange = new Vector4(1, 0.6171f, 0, 1);
                return orange;
            default:
                return Color.blue;
        }
    }

    void SetPlayerColours()
    {
        players = GameObject.FindGameObjectsWithTag("Player");

        for (uint i = 0; i < players.Length; i++)
        {
            players[i].GetComponent<PlayerMovement>().SetID((int)i);
            players[i].GetComponentInChildren<Renderer>().material.color = PlayerColour((int)i);
        }
    }
}
