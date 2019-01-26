using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempPlayer : MonoBehaviour
{
    [SerializeField] InputManager input;
    private void Update()
    {
        if (input.getAxis(Axis.Left_Vertical, 0) > 0)
            transform.Translate(Vector3.up * Time.deltaTime);
        if (input.getAxis(Axis.Left_Horizontal, 0) < 0)
            transform.Translate(Vector3.left * Time.deltaTime);
        if (input.getAxis(Axis.Left_Vertical, 0) < 0)
            transform.Translate(Vector3.down * Time.deltaTime);
        if (input.getAxis(Axis.Left_Horizontal, 0) > 0)
            transform.Translate(Vector3.right * Time.deltaTime);

    }
}
