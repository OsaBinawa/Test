using KRC;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class HandManager : MonoBehaviour
{
    public DeckManager deckManager;
    public GameObject cardPrefab;
    public Transform handTransform;
    public float fanSpread = 5f;
    public List<GameObject> cardsInHand = new List<GameObject>();
    public float cardSpacing = 5f;
    public float verticalSpacing = 10f;
    public int maxHandSize;
    private EnemyAi enemyAi;

    void Start()
    {
        enemyAi = FindObjectOfType<EnemyAi>();
    }

    public void AddCardToHand(Cards cardData)
    {
        if (cardsInHand.Count < maxHandSize)
        {
            GameObject newCard = Instantiate(cardPrefab, handTransform.position, Quaternion.identity, handTransform);

            cardsInHand.Add(newCard);
            UpdateHandVisual();

            newCard.GetComponent<CardsDisplay>().CardData = cardData;
        }
    }

    private void Update()
    {
        //UpdateHandVisual();
    }

    private void UpdateHandVisual()
    {
        int cardCount = cardsInHand.Count;
        if (cardCount == 1)
        {
            cardsInHand[0].transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            cardsInHand[0].transform.localPosition = new Vector3(0f, 0f, 0f);
            return;
        }  
        
       for  (int i=0; i < cardCount; i++)
        {
            float rotationAngle = (fanSpread * (i - (cardCount-1) / 2f));
            cardsInHand[i].transform.localRotation = Quaternion.Euler(0f, 0f, rotationAngle);

            float HorizontalOffset = i* (cardSpacing - (i - (cardCount - 1) / 2f));
            float VerticalOffset = i * (verticalSpacing- (i - (cardCount - 1) / 2f));
            cardsInHand[i].transform.localPosition = new Vector3(HorizontalOffset, VerticalOffset, 0f);
        }
    }
    public void UseCard(int index)
    {
        if (index >= 0 && index < cardsInHand.Count)
        {
            Cards cardData = cardsInHand[index].GetComponent<CardsDisplay>().CardData;
            int dmg = cardData.Dmg; 
            DealDamageToEnemy(dmg);
        }
    }
    private void DealDamageToEnemy(int dmg)
    {
        if (enemyAi != null)
        {
            enemyAi.TakeDamage(dmg);
            Debug.Log("Damage dealt to enemy: " + dmg); 
        }
        else
        {
            Debug.LogError("No EnemyAi instance found!");
        }
    }
}
