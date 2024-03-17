using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using KRC;
using TMPro;

public class CardsDisplay : MonoBehaviour
{

    public Cards cardData;
    public Image cardImage;
    public TMP_Text cardName;
    public TMP_Text cost;
    public TMP_Text damage;
    public Image[] typeImages;

    private Color[] typeColors =
    {
        Color.black, // Emotional
        Color.green // Physical
    }
    private Color[] cardColors =
   (
        new Color(0.4339623f, 0.09579921f, 0.3979303f), // Emotional
        new Color(0.23f, 0.41f, 0.50f) // Physical
    }

    void Start()
   {
        UpdateCardDisplay();
    }

    private void UpdateCardDisplay()
   {
        cardImage.color = cardColors[(int)cardData.cardType[0]];
        if (cardData != null)
       {
            cardName.text = cardData.cardName;
            cost.text = cardData.Cost.ToString();
            damage.text = cardData.Dmg.ToString();
        }
        else
       {
            Debug.LogWarning("CardData is null. Make sure it's assigned properly.");
        }
        for (int i = 0; i < typeImages.Length; i++) 
       {
            typeImages[i].gameObject.SetActive(i < cardData.cardType.Count);
            if (i < cardData.cardType.Count)
           {
                typeImages[i].color = typeColors[(int)cardData.cardType[i]];
            }
        }
    }

}

