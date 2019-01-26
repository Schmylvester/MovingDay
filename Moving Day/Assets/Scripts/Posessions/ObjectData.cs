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
    bool in_house = false;

    private void Start()
    {
        manager = FindObjectOfType<GameManager>();
    }

    public void setOwner(int to, Material mat)
    {
        m_owner = to;
        foreach (MeshRenderer _renderer in GetComponentsInChildren<MeshRenderer>())
            _renderer.material = mat;
    }

    public WeightClass getWeight()
    {
        return m_weight;
    }

    public void putDown()
    {
        if (isInHouse())
            manager.ChangePlayerScore(m_score, m_owner);
    }
    public void pickedUp()
    {
        if (isInHouse())
            manager.ChangePlayerScore(-m_score, m_owner);
        in_house = true;
    }

    bool isInHouse()
    {
        return in_house;
    }
}
