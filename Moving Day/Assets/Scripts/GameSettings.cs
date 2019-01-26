using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour {

    private static GameSettings instance;

    public int m_minutes;
    public float m_seconds;
    public int m_players;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }else
        {
            DestroyObject(gameObject);
        }
    }

    // Use this for initialization
    void Start () {
        DontDestroyOnLoad(this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
