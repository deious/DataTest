using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpControl : MonoBehaviour
{
    public float hp;
    public Transform myHp; //  자신의 슬라이더
    Slider MyHp;
    public Slider hpUI; //  좌측 상단 표시용 슬라이더
    public GameObject characterController;
    public Text GameOver;
    
    public void Start()
    {
        MyHp = myHp.GetComponent<Slider>();
        MyHp.value = hp;
    }

    public void GainDamage(float damage)
    {
        hp -= damage;
        MyHp.value = hp;

       if(hp <= 0)
        {
            CharacterController.characterCount--;
            gameObject.transform.root.gameObject.layer = 11;
            if(CharacterController.characterCount == 0)
            {
                GameOver.gameObject.SetActive(true);
                gameObject.transform.root.GetComponent<Animator>().SetTrigger("Die_t");
                StartCoroutine(Delay());
                Destroy(gameObject.transform.root.gameObject, 0.5f);
            }

            characterController.GetComponent<CharacterController>().DeathChange();
            gameObject.transform.root.GetComponent<Animator>().SetTrigger("Die_t");
            Destroy(gameObject.transform.root.gameObject, 0.5f);
        }
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.485f);
        Time.timeScale = 0.0f;
    }
}
