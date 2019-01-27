using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinnerText : MonoBehaviour
{
    [SerializeField] GameObject[] winnerTexts;
    List<int> winner;
    bool up;
    float move_range = 10;
    float init_pos;
    float speed = 10;
    [SerializeField] UnityEngine.UI.Text tie_text;

    void Start()
    {
        init_pos = transform.position.y;
        winner = FindObjectOfType<GameManager>().getWinners();
        UnityEngine.UI.Text text = GetComponent<UnityEngine.UI.Text>();
        if (winner.Count == 1)
        {
            winnerTexts[1].SetActive(false);
            switch (winner[0])
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
        else
        {
            Debug.Log("Setting tie text active");
            winnerTexts[0].SetActive(false);
            tie_text.text = "";
            foreach(int i in winner)
            {
                tie_text.text += numToWord(i) + '\n';
            }
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

    string numToWord(int i)
    {
        switch (i)
        {
            case 0:
                return "One";
            case 1:
                return "Two";
            case 2:
                return "Three";
            case 3:
                return "Four";
            default:
                break;
        }
        return "";
    }
}
