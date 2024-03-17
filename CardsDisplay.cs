using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using KRC;
using TMPro;
using System;
public class CardsDisplay : MonoBehaviour
{

    public Cards CardData;
    public Image CardImage;
    public TMP_Text CardName;
    public TMP_Text Cost;
    public TMP_Text Damage;
    public Image[] typeImages;


    private Color[] typecolor =
    {
        Color.black, //Emotional
        Color.green, //physical
       
    };
    private Color[] Cardcolor =
    {
        new Color(0.4339623f, 0.09579921f, 0.3979303f), //Emotional
        new Color(0.23f,0.41f,0.50f) //physical
       
    };

    void Start()
    {
        UpdateCardDisplay();
    }

    private void UpdateCardDisplay()
    {
        CardImage.color = Cardcolor[(int)CardData.cardType[0]];
        if (CardData != null)
        {
            CardName.text = CardData.cardName;
            Cost.text = CardData.Cost.ToString();
            Damage.text = CardData.Dmg.ToString();
            
        }
        else
        {
            Debug.LogWarning("CardData is null. Make sure it's assigned properly.");
        }
        for (int i = 0; i < typeImages.Length; i++) 
        {
            if (i < CardData.cardType.Count)
            {
                typeImages[i].gameObject.SetActive(true);
                typeImages[i].color = typecolor[(int)CardData.cardType[i]];
            }
            else
            {
                typeImages[i].gameObject.SetActive(false);
            }
        }
    }

}
