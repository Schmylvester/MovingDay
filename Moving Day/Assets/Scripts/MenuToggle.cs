using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MenuToggle : MonoBehaviour
{
    public enum Button
    {
        Players,
        Time,
        Start
    }
    [SerializeField] ushort min;
    [SerializeField] ushort max;
    [SerializeField] Text text;
    [SerializeField] GameSettings settings;
    [SerializeField] int increment;
    [SerializeField] Button button;
    bool selected;
    InputManager input;
    int val;

    private void Start()
    {
        input = FindObjectOfType<InputManager>();
    }

    private void Update()
    {
        if (selected)
        {
            AxisState state = AxisState.Null;
            float hz = input.getAxisAndState(Axis.D_Horizontal, 0, ref state);
            if (hz > 0 && state == AxisState.Triggered_This_Frame)
            {
                val += increment;
            }
            else if (hz < 0 && state == AxisState.Triggered_This_Frame)
            {
                val -= increment;
            }
            hz = input.getAxisAndState(Axis.Left_Horizontal, 0, ref state);
            if (hz > 0 && state == AxisState.Triggered_This_Frame)
            {
                val += increment;
            }
            else if (hz < 0 && state == AxisState.Triggered_This_Frame)
            {
                val -= increment;
            }

            if (val < min)
                val = max;
            if (val > max)
                val = min;

            valueChanged();
        }
    }

    public void setSelected(bool to)
    {
        selected = to;
    }

    void valueChanged()
    {
        if (button == Button.Time)
        {
            settings.m_minutes = val / 60;
            settings.m_seconds = val % 60;
            text.text = (val / 60).ToString() + ":";
            if(val % 60 < 10)
            {
                text.text += "0";
            }
            text.text += val % 60;
        }
        else if(button == Button.Players)
        {
            settings.m_players = val;
            text.text = val.ToString();
        }
    }
}
