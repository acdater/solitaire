using Assets.MainAssets.Scripts.Interfaces;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MovesControl : MonoBehaviour
{
    [SerializeField] private int _moves = 0;
    [SerializeField] private TextMeshProUGUI m_TextMeshPro;
    [SerializeField] private string _prefix = "Moves";

    private void Start()
    {
        m_TextMeshPro = GetComponent<TextMeshProUGUI>();
    }

    public void Trigger(MoveCommand moveCommand)
    {
        if (moveCommand.isUndo)
        {
            m_TextMeshPro.text = $"{_prefix}: {--_moves}";
        }
        else
        {
            m_TextMeshPro.text = $"{_prefix}: {++_moves}";
        }
    }
}
