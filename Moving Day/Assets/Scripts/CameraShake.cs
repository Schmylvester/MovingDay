using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private Transform camera;

    [SerializeField] private float shake_duration = 3.0f;

    [SerializeField] private float shake_amount = 0.7f;

    private float shake_decrease_timer = 0.7f;
    private float decreaseFactor = 1.0f;

    Vector3 originalPos;

    void Awake()
    {
        if (camera == null)
        {
            camera = FindObjectOfType<Camera>().transform;
        }
    }

    public void SetShake(float duration, float amount)
    {
        shake_duration = duration;
        shake_amount = amount;
        shake_decrease_timer = shake_amount;
    }

    void OnEnable()
    {
        originalPos = camera.localPosition;
    }

    void Update()
    {
        Debug.Log(originalPos + "!");
        if (shake_duration > 0)
        {
            camera.localPosition = originalPos + Random.insideUnitSphere * shake_amount;

            shake_duration -= Time.deltaTime * decreaseFactor;

            if (shake_duration <= shake_decrease_timer)
            {
                shake_amount -= (1 * Time.deltaTime);
            }
        }
        else
        {
            shake_duration = 0f;
            camera.localPosition = originalPos;
        }
    }

    public void SetBasePoint(Vector3 pos)
    {
        originalPos = pos;
    }

    public bool isEarthquake()
    {
        if(shake_amount > 0 && shake_duration > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
