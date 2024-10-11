using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Assets.MainAssets.Scripts.Enums;

public class ScoreControl : MonoBehaviour
{
    [SerializeField] private int _score = 0;
    [SerializeField] private TextMeshProUGUI m_TextMeshPro;
    [SerializeField] private string _prefix = "Score";

    private void Start()
    {
        m_TextMeshPro = GetComponent<TextMeshProUGUI>();
    }

    public void Trigger(MoveCommand moveCommand)
    {
        int scoreToAdd = 0;

        if (moveCommand.isUndo == true)
        {
            scoreToAdd = -20;
        }
        else if (moveCommand.source.GetRule() == StackRule.Aside
            && moveCommand.target.GetRule() == StackRule.Base)
        {
            scoreToAdd = 10;
        }
        else if (moveCommand.source.GetRule() == StackRule.Aside
            && moveCommand.target.GetRule() == StackRule.Board)
        {
            scoreToAdd = 10;
        }
        else if (moveCommand.source.GetRule() == StackRule.Board
            && moveCommand.target.GetRule() == StackRule.Base)
        {
            scoreToAdd = 15;
        }
        else if (moveCommand.source.GetRule() == StackRule.Base
            && moveCommand.target.GetRule() == StackRule.Board)
        {
            scoreToAdd = -15;
        }
        else if (moveCommand.source.GetRule() == StackRule.Board
            && moveCommand.target.GetRule() == StackRule.Board)
        {
            scoreToAdd = 5;
        }

        _score += scoreToAdd;

        if (_score < 0)
        {
            _score = 0;
        }

        m_TextMeshPro.text = $"{_prefix}: {_score}";
    }
}
