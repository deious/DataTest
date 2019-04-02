using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHp : MonoBehaviour
{
    public float hp;
    Slider MyHp;
    public Slider hpUI; //  좌측 상단 표시용 슬라이더
    public Transform StageClear;

    public void Start()
    {
        MyHp = hpUI.GetComponent<Slider>();
        MyHp.value = hp;
    }

    public void GainDamage(float damage)
    {
        hp -= damage;
        //Debug.Log(MyHp);
        MyHp.value = hp;

        if (hp <= 0)
        {
            DungeonGameManager.Instance.Gold += 100;
            StageClear.gameObject.SetActive(true);
            //gameObject.transform.root.GetComponent<Animator>().SetTrigger("Die_t");     // 나중에 추가 될 보스 사망 모션 추가
            StartCoroutine(Delay());
            Destroy(gameObject.transform.root.gameObject, 0.5f);
        }
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.35f);
        Time.timeScale = 0.0f;
    }
}
