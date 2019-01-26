using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameHUD : MonoBehaviour
{
    [SerializeField] private GameObject CountdownHUD;
    [SerializeField] private GameObject HUD;

    [SerializeField] private Text timer, countdown_text, events_text;

    private bool show_event_text = false;
    private bool set_gui = false;

    private GameManager game_manager;

    private float event_timer = 0;

    // Use this for initialization
    void Awake ()
    {
        game_manager = FindObjectOfType<GameManager>();
        GameObject HUDS = GameObject.Find("HUD");
        foreach(Transform child in HUDS.GetComponentInChildren<Transform>())
        {
            if(child.name == "Game HUD")
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
            if (CountdownHUD)
            {
                CountdownHUD.SetActive(false);
                HUD.SetActive(true);
            }
        }

        if(show_event_text)
        {
            event_timer += Time.deltaTime;
        }

        if(event_timer > 3.5)
        {
            events_text.text = "";
            show_event_text = false;
            event_timer = 0;
        }
    }

    private void OnGUI()
    {
        if (timer)
        {
            timer.text = game_manager.GetTimer();
        }
        if (countdown_text.text != "0")
        {
            countdown_text.text = game_manager.GetTimer();
        }
        else
        {
            countdown_text.text = "GO!";
        }
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
        }
    }

    public void SetEventText(string new_text)
    {
        events_text.text = new_text;
        show_event_text = true;
    }
}
