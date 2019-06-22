using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHp : MonoBehaviour
{
    public float hp;
    Slider MyHp;
    public Slider hpUI; //  보스 HP 표시용 슬라이더

    public void Start()
    {
        MyHp = hpUI.GetComponent<Slider>();
        MyHp.value = hp;
    }

    public void GainDamage(float damage)
    {
        hp -= damage;
        MyHp.value = hp;

        if (hp <= 0)
        {
            DungeonManager.Instance.CallGameClear();
            Debug.Log("출력 성공");
            Destroy(gameObject.transform.root.gameObject, 0.5f);
        }
    }
}
