using Assets.MainAssets.Scripts.Cards;
using Assets.MainAssets.Scripts.Interfaces;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class MoveCommand
{
    public IStackable source;
    public IStackable target;
    public Card card;
    public bool isUndo = false;

    public MoveCommand(IStackable source, IStackable target, Card card)
    {
        this.source = source;
        this.target = target;
        this.card = card;
    }

    public void Execute()
    {
        isUndo = false;
        card.Move(target);
    }

    public void Undo()
    {
        isUndo = true;
        card.Move(source, true);
    }
}
