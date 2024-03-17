using KRC;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HandManager : MonoBehaviour, ICardHandler
{
    public DeckManager deckManager;
    public GameObject cardPrefab;
    public Transform handTransform;
    public float fanSpread = 5f;
    public float cardSpacing = 5f;
    public float verticalSpacing = 10f;
    public int maxHandSize = 7;
    private EnemyAi enemyAi;
    private List<GameObject> cardsInHand = new List<GameObject>();

    private void Start()
    {
        enemyAi = FindObjectOfType<EnemyAi>();
    }

    public void OnCardSelect(BaseEventData eventData)
    {
        CardSelectEventData cardEvent = eventData as CardSelectEventData;
        if (cardEvent != null && cardEvent.selectedCard != null)
        {
            AddCardToHand(cardEvent.selectedCard.Data);
        }
    }

    public void AddCardToHand(Cards cardData)
    {
        if (cardsInHand.Count < maxHandSize)
        {
            GameObject newCard = Instantiate(cardPrefab, handTransform);
            newCard.GetComponent<CardsDisplay>().Initialize(cardData, this);
            cardsInHand.Add(newCard);
            ArrangeCards();
        }
    }

    private void ArrangeCards()
    {
        int cardCount = cardsInHand.Count;
        if (cardCount > 0)
        {
            for (int i = 0; i < cardCount; i++)
            {
                float rotationAngle = fanSpread * (i - (cardCount - 1) / 2f);
                float horizontalOffset = i * (cardSpacing - (i - (cardCount - 1) / 2f));
                float verticalOffset = i * (verticalSpacing - (i - (cardCount - 1) / 2f));

                cardsInHand[i].transform.localRotation = Quaternion.Euler(0f, 0f, rotationAngle);
                cardsInHand[i].transform.localPosition = new Vector3(horizontalOffset, verticalOffset, 0f);
            }
        }
    }

    public void UseCard(int index)
    {
        if (index >= 0 && index < cardsInHand.Count)
        {
            Cards cardData = cardsInHand[index].GetComponent<CardsDisplay>().CardData;
            int dmg = cardData.Dmg;
            DealDamageToEnemy(dmg);
            RemoveCardFromHand(index);
        }
    }

    private void RemoveCardFromHand(int index)
    {
        Destroy(cardsInHand[index]);
        cardsInHand.RemoveAt(index);
        ArrangeCards();
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

public interface ICardHandler
{
    void OnCardSelect(BaseEventData eventData);
    void UseCard(int index);
}
