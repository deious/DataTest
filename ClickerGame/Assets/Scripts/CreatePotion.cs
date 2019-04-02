using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreatePotion : MonoBehaviour
{
    public Image potionImage;

    ColorBlock btnColor;
    Color nColor, hColor, pColor, dColor;
    int fontSize;
    Vector2 piAnchorMax, ptAnchorMin;

    void Awake()
    {
        btnColor.normalColor = new Color32(255, 255, 255, 150);
        btnColor.highlightedColor = new Color32(235, 235, 235, 150);
        btnColor.pressedColor = new Color32(210, 210, 210, 150);
        btnColor.disabledColor = new Color32(200, 200, 200, 128);
        btnColor.colorMultiplier = 1.0f;
        btnColor.fadeDuration = 0.1f;

        fontSize = 50;

        piAnchorMax = new Vector2(0.5f, 1f);
        ptAnchorMin = new Vector2(0.5f, 0f);
    }

    void Start()
    {
        // Create no potion
        GameObject noPotion = new GameObject("PotionBtn0");
        noPotion.layer = LayerMask.NameToLayer("UI");

        // Add image, button, layout element, select potion script component to no potion
        Image noImage = noPotion.AddComponent<Image>();
        Button noButton = noPotion.AddComponent<Button>();
        noButton.colors = btnColor;
        LayoutElement noPotionLayout = noPotion.AddComponent<LayoutElement>();
        SelectPotion noSelectPotion = noPotion.AddComponent<SelectPotion>();

        // Create no potion image
        GameObject noPotionImage = new GameObject("Image");
        noPotionImage.layer = LayerMask.NameToLayer("UI");

        // Add image component to no potion image
        Image noPImage = noPotionImage.AddComponent<Image>();
        noPImage.sprite = GameManager.Instance.Potions[0].potionSprite;
        noPImage.preserveAspect = true;

        // Create no potion text
        GameObject noPotionText = new GameObject("Text");
        noPotionText.layer = LayerMask.NameToLayer("UI");

        // Add text component to no potion text
        Text noPText = noPotionText.AddComponent<Text>();
        noPText.text = "적용 안 함";
        noPText.font = GameManager.Instance.mainFont;
        noPText.fontSize = fontSize;
        noPText.alignment = TextAnchor.MiddleCenter;
        noPText.color = Color.black;

        // Establish relationship between transforms
        noPImage.transform.SetParent(noPotion.transform, false);
        noPText.transform.SetParent(noPotion.transform, false);
        noPotion.transform.SetParent(transform, false);

        // Adjust no potion image transform
        RectTransform npirt = noPotionImage.GetComponent<RectTransform>();
        npirt.anchorMin = Vector2.zero;
        npirt.anchorMax = piAnchorMax;
        npirt.sizeDelta = Vector2.zero;

        // Adjust no potion text transform
        RectTransform nptrt = noPotionText.GetComponent<RectTransform>();
        nptrt.anchorMin = ptAnchorMin;
        nptrt.anchorMax = Vector2.one;
        nptrt.sizeDelta = Vector2.zero;

        for (int i = 1; i < GameManager.Instance.Potions.Length; i++)
        {
            // Create potion
            GameObject potion = new GameObject("PotionBtn" + i);
            potion.layer = LayerMask.NameToLayer("UI");

            // Add image, button, layout element, select potion script component to potion
            Image image = potion.AddComponent<Image>();
            Button button = potion.AddComponent<Button>();
            button.colors = btnColor;
            LayoutElement potionLayout = potion.AddComponent<LayoutElement>();
            SelectPotion selectPotion = potion.AddComponent<SelectPotion>();

            // Create potion image
            GameObject potionImage = new GameObject("Image");
            potionImage.layer = LayerMask.NameToLayer("UI");

            // Add image component to potion image
            Image pImage = potionImage.AddComponent<Image>();
            pImage.sprite = GameManager.Instance.Potions[i].potionSprite;
            pImage.preserveAspect = true;

            // Create potion text
            GameObject potionText = new GameObject("Text");
            potionText.layer = LayerMask.NameToLayer("UI");

            // Add text component to potion text
            Text pText = potionText.AddComponent<Text>();
            pText.text = "x" + GameManager.Instance.Potions[i].potionCount;
            pText.font = GameManager.Instance.mainFont;
            pText.fontSize = fontSize;
            pText.alignment = TextAnchor.MiddleCenter;
            pText.color = Color.black;

            // Establish relationship between transforms
            pImage.transform.SetParent(potion.transform, false);
            pText.transform.SetParent(potion.transform, false);
            potion.transform.SetParent(transform, false);

            // Adjust potion image transform
            RectTransform pirt = potionImage.GetComponent<RectTransform>();
            pirt.anchorMin = Vector2.zero;
            pirt.anchorMax = piAnchorMax;
            pirt.sizeDelta = Vector2.zero;

            // Adjust potion text transform
            RectTransform ptrt = potionText.GetComponent<RectTransform>();
            ptrt.anchorMin = ptAnchorMin;
            ptrt.anchorMax = Vector2.one;
            ptrt.sizeDelta = Vector2.zero;
        }
    }
}
