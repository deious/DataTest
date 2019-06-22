using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillController : MonoBehaviour
{
    static int SkillSelected;
    float Damage;

    void OnCollisionEnter2D(Collision2D col)
    {
        Damage = DungeonManager.Instance.characterLists[DungeonManager.Instance.cc.selected].SkilDamage[SkillSelected];

        if (col.transform.tag == "Enemy")
        {
            col.gameObject.GetComponent<EnemyHp>().GainDamage(Damage);
        }
        else if (col.transform.tag == "Boss")
        {
            DungeonManager.Instance.BossInjured(Damage);
        }
    }

    public void Selected(int num)
    {
        SkillSelected = num;
    }
}
