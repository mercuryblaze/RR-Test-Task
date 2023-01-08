using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class CardsSwitcher : MonoBehaviour
{
    [SerializeField] private Hand _hand;
    [SerializeField] private UnityEngine.UI.Button _button;

    private int cardNumber = 0;

    public void SwitchCards()
    {
        for (int i = 0; i < 1; i++)
        {
            int value = _hand.Cards[cardNumber].SetRandomStats();

            if (value == -1)
            {
                _button.enabled = false;

                Destroy(_hand.Cards[cardNumber].gameObject, 0.5f);
                StartCoroutine(StartAfterDestroy(cardNumber));

                if (cardNumber != 0)
                {
                    cardNumber--;
                }

                return;
            }

            cardNumber++;

            if (cardNumber == _hand.Cards.Count)
            {
                cardNumber = 0;
            }
        }
    }

    private IEnumerator StartAfterDestroy(int cardNumber)
    {
        float timer = 0f;

        while (timer < 0.5f) 
        {
            yield return null;
            timer += Time.unscaledDeltaTime;
        }

        _hand.Cards.RemoveAt(cardNumber);
        _button.enabled = true;
    }
}