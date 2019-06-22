using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Damage;            // 공격 데미지
    public float Hp;                // 체력
    public float Speed;             // 움직임 속도
    public float Gold;              // 사망시 주는 골드
    public float AntiElasticity;    // 반탄력

    void Start()
    {
        StartCoroutine(Move());
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            DungeonManager.Instance.Injured(Damage);
            GetComponent<Rigidbody2D>().AddForce(transform.right * AntiElasticity);
        }
        else if(col.gameObject.tag == "Weapon")
        {
            GetComponent<Rigidbody2D>().AddForce(transform.right * AntiElasticity);
        }
    }

    IEnumerator Move()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.02f);
            GetComponent<Rigidbody2D>().AddForce(transform.right * -Speed * Time.timeScale);
        }
    }
}
