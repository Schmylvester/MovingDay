using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private Transform camera;

    [SerializeField] private float shake_duration = 3f;

    [SerializeField] private float shake_amount = 0.7f;

    private float shake_decrease_timer = 0.7f;
    private float decreaseFactor = 1.0f;

    Vector3 originalPos;

    void Awake()
    {
        if (camera == null)
        {
            camera = GetComponent(typeof(Transform)) as Transform;
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
        if (shake_duration > 0)
        {
            camera.localPosition = originalPos + Random.insideUnitSphere * shake_amount;

            shake_duration -= Time.deltaTime * decreaseFactor;

            if(shake_duration <= shake_decrease_timer)
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
}
