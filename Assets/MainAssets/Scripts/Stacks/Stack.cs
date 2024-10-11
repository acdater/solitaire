using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.MainAssets.Scripts.Interfaces;
using Assets.MainAssets.Scripts.Cards;
using System;
using Assets.MainAssets.Scripts.Enums;
using System.Linq;
using UnityEngine.Events;
using Unity.VisualScripting;

namespace Assets.MainAssets.Scripts.Stacks
{
    public class Stack : MonoBehaviour, IStackable
    {
        [SerializeField] private List<Card> _cards;
        private float _offsetY = 0.25f;
        private float _offsetZ = 0.1f;

        public Action<Stack, Card> onAddCardToStack;
        public Action<Stack, Card> onRemoveCardFromStack;

        public bool doOffset = false;
        public int initialCount = -1;
        public Card lastCard = null;
        public StackRule rule;

        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {

        }

        public List<Card> GetCards { get =>  _cards; }

        public void Add(Card card, bool undo = false)
        {
            card.currentStack = this;

            card.transform.position = transform.position + 
                (doOffset 
                    ? (Vector3.down * (_cards.Count * _offsetY))
                    : Vector3.zero);

            card.transform.position = card.transform.position + (Vector3.back * ((_cards.Count + 1) * _offsetZ));

            _cards.Add(card);

            onAddCardToStack?.Invoke(this, card);
            

            if (undo && lastCard != null && rule == StackRule.Board)
            {
                lastCard.Flip();
            }

            if (lastCard != null && lastCard.flipState == CardFlipState.FrontUp && rule == StackRule.Board)
            {
                lastCard.dependentCard = card;
            }

            lastCard = card;
            lastCard.isInteractive = true;
        }

        public void Remove(Card card)
        {
            _cards.Remove(card);
            lastCard = _cards.LastOrDefault();

            if (rule == StackRule.Base)
            {
                onRemoveCardFromStack?.Invoke(this, card);
            }

            if (lastCard != null)
            {
                lastCard.isInteractive = true;
                lastCard.dependentCard = null;
                lastCard.SetUpFront();
            }
        }

        public StackRule GetRule()
        {
            return rule;
        }

        public bool CanAdd(Card card)
        {
            if (rule == StackRule.Aside)
            {
                return true;
            }
            
            if (rule == StackRule.Base)
            {
                if (lastCard == null)
                {
                    return card.rank == 0;
                }

                var hasSameSuit = card.suit == lastCard.suit;
                var hasRightRank = (card.rank - lastCard.rank) == 1;

                return hasSameSuit && hasRightRank;
            }
            
            if (rule == StackRule.Board) 
            {
                if (lastCard == null)
                {
                    return card.rank == Ranks.King;
                }

                var hasRightSuit = false;
                var hasRightRank = (lastCard.rank - card.rank) == 1;

                if (card.suit == Enums.Suit.Heart || card.suit == Enums.Suit.Diamond)
                {
                    hasRightSuit = lastCard.suit == Enums.Suit.Club || lastCard.suit == Suit.Spade;
                }
                else
                {
                    hasRightSuit = lastCard.suit == Suit.Heart || lastCard.suit == Suit.Diamond;
                }

                return hasRightSuit && hasRightRank;
            }
            
            return false;
        }
    }
}
