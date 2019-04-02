using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetDefaultPotion : MonoBehaviour
{
    Image image;

    void Start()
    {
        image = GetComponent<Image>();

        image.sprite = GameManager.Instance.Potions[0].potionSprite;
        image.preserveAspect = true;

    }
}
