using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootObjectControl : MonoBehaviour
{
    //float damage = StatusController.Instance.SkilDamage[2];

    void OnCollisionEnter2D(Collision2D col)
    {
        float damage = StatusController.Instance.SkilDamage[2];
        if (col.transform.tag == "Enemy")
        {
            col.gameObject.GetComponent<EnemyHp>().GainDamage(damage);
            gameObject.SetActive(false);
        }
        else if(col.transform.tag == "Boss")
        {
            col.gameObject.transform.Find("Canvas").Find("Health Slider").GetComponent<BossHp>().GainDamage(damage);
            gameObject.SetActive(false);
        }
    }

    void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }
}
