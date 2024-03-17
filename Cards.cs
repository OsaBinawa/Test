using System.Collections.Generic;
using UnityEngine;

namespace KRC
{
    [CreateAssetMenu(fileName = "New Card", menuName = "Card")]
    public class Card : ScriptableObject
    {
        public string cardName;
        public List<CardType> cardTypes;
        public int cost;
        public int damage;
        public Sprite cardSprite;
        public List<DamageType> damageTypes;

        [System.Serializable]
        public enum CardType
        {
            Emotional,
            Physical
        }

        [System.Serializable]
        public enum DamageType
        {
            Emotional,
            Physical
        }
    }
}
