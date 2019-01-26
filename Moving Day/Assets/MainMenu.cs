using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    InputManager iM;
    // Use this for initialization
    void Start () {
        iM = FindObjectOfType<InputManager>();
    }
	
	// Update is called once per frame
	void Update () {
        if (iM.isButtonPressed(XboxButton.Start, 0))
        {
            SceneManager.LoadScene("TruckSpawn");
        }
	}
}
