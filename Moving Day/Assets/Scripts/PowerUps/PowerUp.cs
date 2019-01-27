using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {

    [SerializeField] SpriteRenderer m_sprite;
    int index;
    float timer = 10;

	public void setPower(int set, Sprite _sprite)
    {
        index = set;
        m_sprite.sprite = _sprite;
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
            Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerBuffs>().addPower((Power_Ups)(index));
            Destroy(gameObject);
        }
    }
}
