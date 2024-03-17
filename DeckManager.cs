using KRC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    public List<Cards> AllCards = new List<Cards>();

    private int currentIndex = 0;
    public int startingHandSize = 3;

    public int maxHandSize;
    private HandManager handManager;
    public int currentHandSize;

    void Start()
    {
        Cards[] cards = Resources.LoadAll<Cards>("CardData");
        AllCards.AddRange(cards);

        handManager = FindAnyObjectByType<HandManager>();
        maxHandSize = handManager.maxHandSize;

        
        for (int i = 0; i < startingHandSize; i++)
        {
            DrawCard(handManager);
        }
    }

    private void Update()
    {
        if (handManager != null)
        {
            currentHandSize = handManager.cardsInHand.Count;
        }
    }

    public void DrawCard(HandManager handManager)
    {
        if (AllCards.Count == 0) return;

        if (currentHandSize < maxHandSize)
        {
            Cards NextCard = AllCards[currentIndex];
            handManager.AddCardToHand(NextCard);
            currentIndex = (currentIndex + 1) % AllCards.Count;
        }
        
    }

}
