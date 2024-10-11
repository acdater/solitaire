using Assets.MainAssets.Scripts.Cards;
using Assets.MainAssets.Scripts.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MoveManager : MonoBehaviour
{
    [SerializeField] private CardsSpawner _spawner;
    private System.Collections.Stack _commands = new Stack();

    public UnityEvent<MoveCommand> MovePerformed;

    private void Start()
    {
        if (_spawner == null)
        {
            _spawner = this.GetComponent<CardsSpawner>();
        }
    }

    public bool TryMove(Card card)
    {
        if (card.isInteractive == false)
        {
            return false;
        }

        var stack = GetAvailableStack(card);

        if (stack != null)
        {
            var command = new MoveCommand(card.currentStack, stack, card);

            if (card.currentStack.GetRule() != Assets.MainAssets.Scripts.Enums.StackRule.Main)
            {
                _commands.Push(command);
            }

            command.Execute();
            MovePerformed?.Invoke(command);
        }

        return stack != null;
    }

    public void UndoAction()
    {
        if (_commands.Count == 0) 
        { 
            return; 
        }

        var lastCommand = _commands.Pop() as MoveCommand;
        lastCommand.Undo();
        MovePerformed?.Invoke(lastCommand);
    }

    private IStackable GetAvailableStack(Card card)
    {
        IStackable stack = null;

        if (card.currentStack.GetRule() == Assets.MainAssets.Scripts.Enums.StackRule.Main)
        {
            return _spawner.asideStack;
        }

        // 1st check base stacks

        foreach (var baseStack in _spawner.baseStacks) 
        {
            if (baseStack.CanAdd(card))
            {
                return baseStack;
            }
        }

        // 2nd check board stacks
        foreach (var boardStack in _spawner.boardStacks)
            {
            if (boardStack.CanAdd(card))
            {
                return boardStack;
            }
        }

        return stack;
    }
}
