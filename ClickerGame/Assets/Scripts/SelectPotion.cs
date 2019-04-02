using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectPotion : MonoBehaviour
{
    Image potionImage;

    void Start()
    {
        potionImage = transform.Find("Image").GetComponent<Image>();

        gameObject.GetComponent<Button>().onClick.AddListener(Select);
    }

    void Select()
    {
        // Test용 코드 -> 실제 효과를 주는 코드로 변경(현재는 전부 "test" string)
        int index;
        int.TryParse(gameObject.name.Substring(gameObject.name.Length - 1, 1), out index);
        Debug.Log(GameManager.Instance.Potions[index].PotionEffect + " " + gameObject.name);

        transform.parent.GetComponent<CreatePotion>().potionImage.sprite = potionImage.sprite;

        transform.parent.parent.parent.parent.gameObject.SetActive(false);
    }
}
