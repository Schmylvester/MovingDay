using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameHUD : MonoBehaviour
{
    [SerializeField] private GameObject CountdownHUD;
    [SerializeField] private GameObject HUD;

    [SerializeField] private Text timer, countdown_text, events_text;

    private bool set_gui = false;

    private GameManager game_manager;

	// Use this for initialization
	void Awake ()
    {
        game_manager = FindObjectOfType<GameManager>();
        GameObject HUDS = GameObject.Find("HUD");
        foreach(Transform child in HUDS.GetComponentInChildren<Transform>())
        {
            if(child.name == "HUD")
            {
                HUD = child.gameObject;
            }
            else if(child.name == "Countdown HUD")
            {
                CountdownHUD = child.gameObject;
            }
        }
        GUIElements();
	}

    private void Update()
    {
        if (!set_gui)
        {
            GUIElements();
            set_gui = true;
        }

        if(game_manager.TimerMode() == "Countdown")
        {
            CountdownHUD.SetActive(true);
            HUD.SetActive(false);
        }
        else
        {
            CountdownHUD.SetActive(false);
            HUD.SetActive(true);
        }
    }

    private void OnGUI()
    {
        timer.text = game_manager.GetTimer();
        if (countdown_text.text != "0")
        {
            countdown_text.text = game_manager.GetTimer();
        }
        else
        {
            countdown_text.text = "GO!";
        }
        events_text.text = "";
    }

    void GUIElements()
    {
        GameGUIElements();

        //Countdown Elements
        foreach(Transform child in CountdownHUD.GetComponentInChildren<Transform>())
        {
            if(child.gameObject.name == "Text")
            {
                countdown_text = child.GetComponent<Text>();
            }
        }
    }

    void GameGUIElements()
    {
        foreach (Transform child in HUD.GetComponentInChildren<Transform>())
        {
            if (child.gameObject.name == "Timer")
            {
                timer = child.GetComponent<Text>();
            }
            else if(child.gameObject.name == "Event Text")
            {
                events_text = child.GetComponent<Text>();
            }

            //Set Player Score Text
            for (uint i = 0; i < game_manager.GetPlayerCount(); i++)
            {
                string title = "Player " + (i + 1).ToString() + " Score";
            }
            if (game_manager.GetPlayerCount() < 4)
            {
                for (uint i = (uint)game_manager.GetPlayerCount(); i < 4; i++)
                {
                    string title = "Player " + (i + 1).ToString() + " Score";
                    if (child.gameObject.name == title)
                    {
                        child.GetComponent<Text>().text = "";
                    }
                }
            }
        }
    }

    public void SetEventText(string new_text)
    {
        events_text.text = new_text;
    }
}
