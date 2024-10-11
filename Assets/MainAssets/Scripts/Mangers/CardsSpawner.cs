using Assets.MainAssets.Scripts.Cards;
using Assets.MainAssets.Scripts.Enums;
using Assets.MainAssets.Scripts.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Stack = Assets.MainAssets.Scripts.Stacks.Stack;

public class CardsSpawner : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip shuffleClip;

    public Sprite[] clubs;
    public Sprite[] diamands;
    public Sprite[] hearts;
    public Sprite[] spades;
    public Sprite back;

    public Card cardPrefab;
    public List<Stack> baseStacks = new List<Stack>();
    public List<Stack> boardStacks = new List<Stack>();
    public Stack mainStack;
    public Stack asideStack;

    // Start is called before the first frame update
    void Start()
    {
        var cards = new List<Card>();

        foreach(var suitValue in Enum.GetValues(typeof(Suit)))
        {
            foreach(var rankValue in Enum.GetValues(typeof(Ranks)))
            {
                var prefab = Instantiate(cardPrefab);
                prefab.suit = (Suit)suitValue;
                prefab.rank = (Ranks)rankValue;

                SetSpriteTo(prefab);
                cards.Add(prefab);
            }
        }

        cards.Shuffle();
        SetCardsToPositions(cards);
    }

    private void SetCardsToPositions(List<Card> cards)
    {
        foreach(var boardStack in boardStacks)
        {
            Card lastCard = null;

            for(int i = 0; i < boardStack.initialCount; i++)
            {
                var card = cards.First();

                lastCard = card;
                card.SetUpBack();
                boardStack.Add(card);
                cards.Remove(card);
            }

            lastCard?.SetUpFront();
        }

        foreach(var card in cards)
        {
            mainStack.Add(card);
        }

        cards.Clear();

        audioSource.PlayOneShot(shuffleClip);
    }


    // Update is called once per frame
    void Update()
    {

    }

    private void SetSpriteTo(Card prefab)
    {
        if (prefab.suit == Suit.Club)
        {
            AssignRankTo(prefab, clubs);
        }
        else if (prefab.suit == Suit.Heart)
        {
            AssignRankTo(prefab, hearts);
        }
        else if (prefab.suit == Suit.Diamond)
        {
            AssignRankTo(prefab, diamands);
        }
        else 
        {
            AssignRankTo(prefab, spades);
        }
    }

    private void AssignRankTo(Card prefab, Sprite[] cardsSprites)
    {
        prefab.SetSprite(cardsSprites[(int)prefab.rank]);
    }
}
