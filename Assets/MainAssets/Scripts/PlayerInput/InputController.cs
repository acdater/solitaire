using Assets.MainAssets.Scripts.Cards;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField] private MoveManager _moveManager;

    private void Start()
    {
        if (_moveManager == null)
        {
            _moveManager = this.GetComponent<MoveManager>();
        }
    }

    void Update()
    {
        CheckForMouseInput();
        CheckForTouchInput();
    }

    private void CheckForTouchInput()
    {
        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Ended)
            {
                var position = touch.position;
                CastRayTo(position);
            }
        }
    }


    private void CheckForMouseInput()
    {
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            var position = Input.mousePosition;
            CastRayTo(position);
        }
    }
    private void CastRayTo(Vector3 position)
    {
        var worldpoint = Camera.main.ScreenToWorldPoint(position);

        var hit = Physics2D.Raycast(worldpoint, Vector2.zero);

        if (hit.collider == null) return;

        var resetItem = hit.collider.gameObject.GetComponent<ResetItem>();

        if (resetItem != null)
        {
            resetItem.TriggerReset();
        }

        var card = hit.collider.gameObject.GetComponent<Card>();

        if (card == null) return;

        _moveManager.TryMove(card);
    }
}
