using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KRC
{
    [CreateAssetMenu(fileName = "New Card", menuName = "card")]
    public class Cards:ScriptableObject
    {
        public string cardName;
        public List<CardType> cardType;
        public int Cost;
        public int Dmg;
        public Sprite cardSprite;
        public List<DamageType> damageType;


        public enum CardType
        {
            emotional,
            physical,
        }

        public enum DamageType
        {
            emotional,
            physical,
        }

    }
}

