using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsManager : MonoBehaviour
{
    [SerializeField] private bool fire_s, earthquake_s;
    [SerializeField] private GameObject small_fire, medium_fire, large_fire;

    [SerializeField] private Vector2 lower_bounds, upper_bounds;

    [SerializeField] AudioClip m_earthQuakeSound;

    private bool display_text = false, is_quake = false;

    private GameHUD game_hud;

    private float clock_seconds = 0, event_time = 0;

    private CameraShake camera_shake;

	// Use this for initialization
	void Start ()
    {
        event_time = Random.Range(5.0f, 30.0f);
        camera_shake = FindObjectOfType<CameraShake>();
        game_hud = FindObjectOfType<GameHUD>();
        game_hud.SetEventText("");
        Debug.Log(event_time);
	}

    private void Update()
    {
        if(fire_s)
        {
            Fire();
            fire_s = false;
        }
        if(earthquake_s)
        {
            InitialiseEarthquake();
            is_quake = true;
            earthquake_s = false;
        }

        if(is_quake)
        {
            UpdateEarthquake();
        }

        if(camera_shake)
        {
            if (!camera_shake.isEarthquake())
            {
                is_quake = false;
            }
        }
    }

    public void EventChecker(float dt)
    {
        clock_seconds += dt;
        if(clock_seconds >= event_time)
        {
            Events();
        }
    }

    void Events()
    {
        int num = (int)Random.Range(0, 5);

        switch(num)
        {
            case 0:
            case 3:
                InitialiseEarthquake();
                earthquake_s = true;
                break;
            case 1:
                Train();
                break;
            case 2:
            case 4:
                Fire();
                break;
            default:
                Debug.Log("UH OH");
                break;
        }
        clock_seconds = 0;
        event_time = Random.Range(15.0f, 30.0f);
        Debug.Log(event_time);
    }

    void InitialiseEarthquake()
    {
        AudioSource source = GetComponent<AudioSource>();
        source.clip = m_earthQuakeSound;
        source.Play();
        if(camera_shake)
        camera_shake.SetShake(5f, 0.7f);
        game_hud.SetEventText("EARTHQUAKE!");
    }

    void UpdateEarthquake()
    {
        foreach (Rigidbody rb in FindObjectsOfType<Rigidbody>())
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
        int rand = Random.Range(0, 15);
        GameObject fire;

        switch(rand)
        {
            case 0:
            case 1:
            case 2:
            case 4:
            {
                fire = medium_fire;
                break;
            }
            case 3:
            {
                fire = large_fire;
                break;
            }
            default:
            {
                fire = small_fire;
                break;
            }
        }

        Vector2 upper = upper_bounds;
        Vector2 lower = lower_bounds;

        ModifyPositions(fire, ref upper, ref lower);

        float new_x = Random.Range(lower.x, upper.x);
        float new_z = Random.Range(lower.y, upper.y);
        Vector3 pos = new Vector3(new_x, fire.transform.position.y, new_z);
        Instantiate(fire, pos, fire.transform.rotation);
        game_hud.SetEventText("HELP! FIRE!");
    }
    
    void ModifyPositions(GameObject fire, ref Vector2 upper, ref Vector2 lower)
    {
        if (fire == large_fire)
        {
            lower = new Vector2(0, 0);
            upper = new Vector2(0, 0);
        }
        else if (fire == medium_fire)
        {
            lower.x += 0.36f;
            lower.y -= 0.9f;
            upper.x -= 2.6f;
            upper.y -= 4;
        }
        else
        {
            lower.x += 0.65f;
            lower.y += 0.77f;
            upper.x -= 0.7f;
            upper.y -= 0.5f;
        }
    }
}
