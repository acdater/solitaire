using Assets.MainAssets.Scripts.Cards;
using Assets.MainAssets.Scripts.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Stack = Assets.MainAssets.Scripts.Stacks.Stack;

public class WinManager : MonoBehaviour
{
    [SerializeField] private CardsSpawner _spawner;
    Dictionary<Stack, List<Card>> _stacksOfCards = new Dictionary<Stack, List<Card>>();

    public UnityEvent PlayerWon;

    private void Start()
    {
        foreach(var stack in _spawner.baseStacks)
        {
            stack.onAddCardToStack += AddCardToBase;
            stack.onRemoveCardFromStack += RemoveCardFromBase;

            _stacksOfCards[stack] = new List<Card>();
        }
    }


    public void AddCardToBase(Stack stack, Card card)
    {
        if (!_stacksOfCards.ContainsKey(stack))
        {
            _stacksOfCards.Add(stack, new List<Card>() { card });
        }

        _stacksOfCards[stack].Add(card);

        CheckIfPlayerWon();
    }

    public void RemoveCardFromBase(Stack stack, Card card)
    {
        if (!_stacksOfCards.ContainsKey(stack))
        {
            return;
        }

        _stacksOfCards[stack].Remove(card);
    }

    private void CheckIfPlayerWon()
    {
        var playerWon = true;

        foreach (var cards in _stacksOfCards.Values) 
        {
            playerWon = playerWon && cards.Count >= 0;// == Enum.GetValues(typeof(Ranks)).Length;
        }

        if (playerWon)
        {
            PlayerWon?.Invoke();
        }
    }
}
