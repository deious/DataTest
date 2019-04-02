using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHp : MonoBehaviour
{
    public float hp;

    public void GainDamage(float damage)
    {
        hp -= damage;

        if (hp <= 0)
        {
            DungeonGameManager.Instance.Gold += 10;
            Destroy(gameObject.transform.root.gameObject);
        }
    }
}