using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsManager : MonoBehaviour
{
    private float clock_seconds = 0, event_time = 0;

    private CameraShake camera_shake;

	// Use this for initialization
	void Start ()
    {
        event_time = Random.Range(5, 60);
        camera_shake = FindObjectOfType<CameraShake>();
	}

    public void EventChecker(float dt)
    {
        clock_seconds += dt;
        if(clock_seconds >= event_time)
        {
            Events();
        }
        Debug.Log("Event time: " + event_time);
    }

    void Events()
    {
        int num = (int)Random.Range(0, 2);
        Debug.Log(num);

        switch(num)
        {
            case 0:
                Earthquake();
                break;
            case 1:
                Train();
                break;
            default:
                Debug.Log("UH OH");
                break;
        }
        clock_seconds = 0;
        event_time = Random.Range(15, 60);
    }

    void Earthquake()
    {
        if(camera_shake)
        camera_shake.SetShake(5, 0.7f);
        Debug.Log("EARTHQUAKE!");
    }

    void Train()
    {
        TrainEmitter[] train_em = FindObjectsOfType<TrainEmitter>();

        if (train_em.Length > 0)
        {
            int rand_num = (int)Random.Range(0, train_em.Length);
            train_em[rand_num].SpawnTrain();
        }
    }
}
