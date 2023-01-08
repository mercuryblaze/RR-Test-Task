using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NumberCounter : MonoBehaviour
{
    [SerializeField] private int _countFPS = 5;
    [SerializeField] private float _duration = 1f;

    private Coroutine _countingCoroutine;

    public void UpdateText(int value, int newValue, TextMeshProUGUI text)
    {
        if (_countingCoroutine != null)
        {
            StopCoroutine(_countingCoroutine);
        }
        _countingCoroutine = StartCoroutine(CountText(value, newValue, text));
    }

    public IEnumerator CountText(int value, int newValue, TextMeshProUGUI text)
    {
        WaitForSeconds wait = new WaitForSeconds(1f / _countFPS);
        int previousValue = value;
        int stepAmount;

        if (newValue - previousValue < 0)
        {
            stepAmount = Mathf.FloorToInt((newValue - previousValue) / (_countFPS * _duration));
        }
        else
        {
            stepAmount = Mathf.CeilToInt((newValue - previousValue) / (_countFPS * _duration));
        }

        if (previousValue < newValue)
        {
            while (previousValue < newValue)
            {
                previousValue += stepAmount;

                if (previousValue > newValue)
                {
                    previousValue = newValue;
                }

                text.SetText(previousValue.ToString("0"));

                yield return wait;
            }
        }
        else
        {
            while (previousValue > newValue)
            {
                previousValue += stepAmount;

                if (previousValue < newValue)
                {
                    previousValue = newValue;
                }

                text.SetText(previousValue.ToString("0"));

                yield return wait;
            }
        }
    }
}