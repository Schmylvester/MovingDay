using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeightClass
{
    None,
    Light,
    Medium,
    Heavy,
}

public class ObjectData : MonoBehaviour
{
    GameManager manager;
    [SerializeField] WeightClass m_weight = WeightClass.Light;
    [SerializeField] int m_score;
    int m_owner;

    private void Start()
    {
        manager = FindObjectOfType<GameManager>();
    }

    public int getOwner()
    {
        return m_owner;
    }

    public void setOwner(int to)
    {
        m_owner = to;
        foreach (MeshRenderer _renderer in GetComponentsInChildren<MeshRenderer>())
            _renderer.material = FindObjectOfType<ObjectManager>().getMaterial(to);
    }

    public WeightClass getWeight()
    {
        return m_weight;
    }

    public void putDown()
    {
        int room = FindObjectOfType<RoomIdentifier>().InRoom(transform.position);
        if (room != -1)
            manager.ChangePlayerScore(m_score, m_owner, room);
    }
    public void pickedUp()
    {
        int room = FindObjectOfType<RoomIdentifier>().InRoom(transform.position);
        if (room != -1)
            manager.ChangePlayerScore(-m_score, m_owner, room);
    }

    public void objectBurned()
    {
        int room = FindObjectOfType<RoomIdentifier>().InRoom(transform.position);
        if (room != -1)
            manager.ChangePlayerScore(-m_score, m_owner, room);
        Destroy(gameObject);
    }
}
