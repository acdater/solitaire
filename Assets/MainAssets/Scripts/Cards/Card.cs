using Assets.MainAssets.Scripts.Enums;
using Assets.MainAssets.Scripts.Interfaces;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.MainAssets.Scripts.Cards
{
    public class Card : MonoBehaviour
    {
        public Card dependentCard;
        public AudioSource audioSource;
        public Sprite mainSprite;
        public Sprite backSprite;

        public Suit suit;
        public Ranks rank;
        public CardFlipState flipState = CardFlipState.FrontUp;
        public bool isFlipped = false;
        public bool isInteractive;

        public IStackable currentStack;
        

        public void Flip()
        {
            if (isFlipped == true)
            {
                SetUpFront();
            }
            else
            {
                SetUpBack();
            }
        }

        public void SetUpFront()
        {
            isFlipped = false;
            flipState = CardFlipState.FrontUp;
            GetComponent<SpriteRenderer>().sprite = mainSprite;
        }

        public void SetUpBack()
        {
            isFlipped = true;
            flipState = CardFlipState.BackUp;
            GetComponent<SpriteRenderer>().sprite = backSprite;
        }

        public void Move(IStackable target, bool undo = false)
        {
            audioSource.Play();

            currentStack.Remove(this);
            target.Add(this, undo);
            currentStack = target;

            if (dependentCard == null)
            {
                return;
            }
            else
            {
                dependentCard.Move(target);
            }
        }

        public void SetSprite(Sprite sprite)
        {
            mainSprite = sprite;
            GetComponent<SpriteRenderer>().sprite = mainSprite;
        }
    }
}

