using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsManager : MonoBehaviour
{
    [SerializeField] private bool earthquake_s = false;
    [SerializeField] private GameObject fire;

    [SerializeField] private Vector2 lower_bounds, upper_bounds;

    private bool display_text = false;

    private GameHUD game_hud;

    private float clock_seconds = 0, event_time = 0;

    private CameraShake camera_shake;

	// Use this for initialization
	void Start ()
    {
        event_time = Random.Range(5.0f, 60.0f);
        camera_shake = FindObjectOfType<CameraShake>();
        game_hud = FindObjectOfType<GameHUD>();
        game_hud.SetEventText("");
	}

    private void Update()
    {
        if(earthquake_s)
        {
            Earthquake();
        }
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
        int num = (int)Random.Range(0, 3);
        Debug.Log(num);

        switch(num)
        {
            case 0:
                Earthquake();
                break;
            case 1:
                Train();
                break;
            case 2:
                Fire();
                break;
            default:
                Debug.Log("UH OH");
                break;
        }
        clock_seconds = 0;
        event_time = Random.Range(15.0f, 60.0f);
    }

    void Earthquake()
    {
        if(camera_shake)
        camera_shake.SetShake(5, 0.7f);
        game_hud.SetEventText("EARTHQUAKE!");
        foreach(Rigidbody rb in FindObjectsOfType<Rigidbody>())
        {
            if (rb.gameObject.name != "Train")
            {
                float rand_up = Random.Range(-10.0f, 10.0f);
                float rand_left = Random.Range(-10.0f, 10.0f);
                rb.transform.position += Vector3.forward * (rand_up * Time.deltaTime);
                rb.transform.position += Vector3.left * (rand_left * Time.deltaTime);
            }
        }
    }

    void Train()
    {
        TrainEmitter[] train_em = FindObjectsOfType<TrainEmitter>();

        if (train_em.Length > 0)
        {
            int rand_num = (int)Random.Range(0, train_em.Length);
            train_em[rand_num].SpawnTrain();
        }
        game_hud.SetEventText("CHOO! CHOO!");
    }

    void Fire()
    {
        float new_x = Random.Range(lower_bounds.x, upper_bounds.x);
        float new_z = Random.Range(lower_bounds.y, upper_bounds.y);
        Vector3 pos = new Vector3(new_x, fire.transform.position.y, new_z);
        Instantiate(fire, pos, fire.transform.rotation);
        game_hud.SetEventText("HELP! FIRE! Move your stuff! QUICK!");
    }
}
