using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillController : MonoBehaviour
{
    static int SkillSelected;
    float damage;

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log(SkillSelected);
        damage = gameObject.transform.root.Find("StatusController").GetComponent<StatusController>().SkilDamage[SkillSelected];
        Debug.Log(gameObject.transform.root);
        Debug.Log(damage);
        if (col.transform.tag == "Enemy")
        {
            col.gameObject.GetComponent<EnemyHp>().GainDamage(damage);
        }
        else if (col.transform.tag == "Boss")
        {
            col.gameObject.transform.Find("Canvas").Find("Health Slider").GetComponent<BossHp>().GainDamage(damage);
        }
    }

    public void Selected(int num)
    {
        SkillSelected = num;
        //Debug.Log(SkillSelected);
        //damage = StatusController.Instance.SkilDamage[SkillSelected];
        //Debug.Log(gameObject.transform.root);
        //damage = gameObject.transform.root.Find("StatusController").GetComponent<StatusController>().SkilDamage[SkillSelected];
        //Debug.Log(damage);
        //return SkillSelected;
    }
}
