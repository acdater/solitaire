using Assets.MainAssets.Scripts.Cards;
using Assets.MainAssets.Scripts.Extensions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] private CardsSpawner _spawner;
    [SerializeField] private GameObject _winPopup;

    public void ResetAsideToMain()
    {
        var mainCards = _spawner.mainStack.GetCards.ToList();
        var asideCards = _spawner.asideStack.GetCards.ToList();

        foreach (var card in asideCards)
        {
            _spawner.asideStack.Remove(card);
        }

        foreach (var card in mainCards)
        {
            _spawner.mainStack.Remove(card);
        }

        var combined = mainCards.Concat(asideCards).ToList();
        combined.Shuffle();

        foreach (var card in combined)
        {
            _spawner.mainStack.Add(card);
        }
    }

    public void ShowWinPopup()
    {
        _winPopup.SetActive(true);
    }
}
