using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinnerText : MonoBehaviour
{
    int winner;
    bool up;
    float move_range = 10;
    float init_pos;
    float speed = 10;

    void Start()
    {
        init_pos = transform.position.y;
        winner = FindObjectOfType<GameManager>().getWinner();
        UnityEngine.UI.Text text = GetComponent<UnityEngine.UI.Text>();
        switch (winner)
        {
            case 0:
                text.text = "One";
                text.color = Color.red;
                break;
            case 1:
                text.text = "Two";
                text.color = Color.blue;
                break;
            case 2:
                text.text = "Three";
                text.color = new Color(0.6f, 0, 1);
                break;
            case 3:
                text.text = "Four";
                text.color = new Color(1, 0.6f, 0);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(up)
        {
            transform.Translate(Vector3.up * Time.deltaTime * speed);
            if (transform.position.y > init_pos + move_range)
                up = false;
        }
        else
        {
            transform.Translate(Vector3.down * Time.deltaTime * speed);
            if (transform.position.y < init_pos - move_range)
                up = true;
        }
    }
}
