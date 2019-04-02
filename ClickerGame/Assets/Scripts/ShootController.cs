using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour
{
    public Shoot shoot;
    //public int selected = 0;
    GameObject gameObject;
    bool UseSkil = true;

    // 발사 후 이동 시킬 것
    public void ShootObject()
    {
        if (UseSkil)
        {
            StartCoroutine(CoolTime());
        }
        else
        {
            return;
        }
        /*gameObject = Instantiate(shoot.shootPrefab[CharacterController.selected], transform.position, transform.rotation);
        Destroy(gameObject, 1.0f);*/
    }

    public void Update()
    {
        if (gameObject != null)
        {
            gameObject.transform.Translate(Vector3.right / 10.0f);
        }
    }

    IEnumerator CoolTime()
    {
        UseSkil = false;
        yield return new WaitForSeconds(1.0f);
        gameObject = Instantiate(shoot.shootPrefab[CharacterController.selected], transform.position, transform.rotation);
        Destroy(gameObject, 0.65f);
        UseSkil = true;
    }

    [System.Serializable]
    public class Shoot
    {
        public GameObject[] shootPrefab;
    }
}
