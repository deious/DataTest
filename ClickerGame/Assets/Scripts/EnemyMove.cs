using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyMove : MonoBehaviour
{
    void Update()
    {
        GetComponent<Rigidbody2D>().AddForce(transform.right *-10f);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground") { return; }
        else if(col.gameObject.tag == "Player")
        {
            col.gameObject.transform.Find("Canvas").Find("Health Slider").GetComponent<HpControl>().GainDamage(30);
            GetComponent<Rigidbody2D>().AddForce(transform.right * 300f);
        }
        else
        {
            return;
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
