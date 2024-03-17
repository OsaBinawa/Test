using KRC;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    #region Fields

    public List<Card> AllCards = new List<Card>();

    private int currentIndex = 0;
    public int startingHandSize = 3;

    public int maxHandSize;
    private HandManager handManager;
    public int currentHandSize;

    #endregion

    #region Unity Methods

    private void Start()
    {
        LoadCards();

        FindHandManager();

        DrawStartingHand();
    }

    private void Update()
    {
        if (handManager != null)
        {
            currentHandSize = handManager.cardsInHand.Count;
        }
    }

    #endregion

    #region Private Methods

    private void LoadCards()
    {
        Card[] cards = Resources.LoadAll<Card>("CardData");
        AllCards.AddRange(cards);
    }

    private void FindHandManager()
    {
        handManager = FindObjectOfType<HandManager>();
       
