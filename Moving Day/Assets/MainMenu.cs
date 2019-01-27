using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    InputManager iM;
    [SerializeField] MenuToggle[] menu_items;
    int selected = 0;
    [SerializeField] Transform selecter;

    // Use this for initialization
    void Start()
    {
        iM = FindObjectOfType<InputManager>();
        menu_items[0].setSelected(true);
    }

    // Update is called once per frame
    void Update()
    {
        AxisState state = AxisState.Null;
        float vert = iM.getAxisAndState(Axis.D_Vertical, 0, ref state);
        if (vert > 0 && state == AxisState.Triggered_This_Frame)
        {
            changeSelect(1);
        }
        if (vert < 0 && state == AxisState.Triggered_This_Frame)
        {
            changeSelect(-1);
        }
        vert = iM.getAxisAndState(Axis.Left_Vertical, 0, ref state);
        if (vert < 0 && state == AxisState.Triggered_This_Frame)
        {
            changeSelect(1);
        }
        if(vert > 0 && state == AxisState.Triggered_This_Frame)
        {
            changeSelect(-1);
        }

        if (iM.isButtonPressed(XboxButton.A, 0) && selected == 2)
        {
            SceneManager.LoadScene("HouseScorer");
        }
    }

    void changeSelect(int dir)
    {
        menu_items[selected].setSelected(false);
        selected += dir;
        if (selected > 2)
            selected = 0;
        else if (selected < 0)
            selected = 2;
        menu_items[selected].setSelected(true);
        selecter.SetParent(menu_items[selected].transform, false);
    }
}
