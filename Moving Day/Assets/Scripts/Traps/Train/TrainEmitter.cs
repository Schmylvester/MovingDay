using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainEmitter : MonoBehaviour
{
    [SerializeField] private GameObject train;

    [SerializeField] private bool spawn = false;

    CameraShake camera_shake;

    private void Awake()
    {
        camera_shake = FindObjectOfType<CameraShake>();
    }

    private void Update()
    {
        if(spawn)
        {
            SpawnTrain();
            spawn = false;
        }
    }

    public void SpawnTrain()
    {
        camera_shake.SetShake(7.0f, 0.075f);
        Instantiate(train, transform.position, transform.rotation);
    }
}
