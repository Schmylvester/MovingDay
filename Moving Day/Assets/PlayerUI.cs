using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private GameObject canvasGobj;
    [SerializeField] private Text boost;

    private float showTime = 1.5f;
    private float showTimer = 0;
    private Color lerpColor;

	// Use this for initialization
	void Start ()
	{
        boost.color = Color.clear;
	}

    private void Update()
    {
        canvasGobj.transform.LookAt(canvasGobj.transform.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);

        if (showTimer > 0)
        {
            showTimer += Time.deltaTime;
            boost.color = Color.Lerp(boost.color, lerpColor, Time.deltaTime * 5);

            if (showTimer > showTime)
            {
                showTimer = 0;
            }
        }
        else
        {
            boost.color = Color.Lerp(boost.color, Color.clear, Time.deltaTime * 3);
        }
    }

    public void SetBoostUI(string _power)
    {
        showTimer = 0.1f;
        lerpColor = FindObjectOfType<GameManager>().PlayerColour(GetComponent<PlayerMovement>().GetPlayerID());
        boost.text = _power;
    }
}
