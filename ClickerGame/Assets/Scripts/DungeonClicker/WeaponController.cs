using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    float Damage;

    void OnCollisionEnter2D(Collision2D col)
    {
        Damage = DungeonManager.Instance.characterLists[DungeonManager.Instance.cc.selected].AttackDamage;

        if (col.transform.tag == "Enemy")
        {
            col.gameObject.GetComponent<EnemyHp>().GainDamage(Damage);
        }
        else if(col.transform.tag == "Boss")
        {
            DungeonManager.Instance.BossInjured(Damage);
        }
    }
}
