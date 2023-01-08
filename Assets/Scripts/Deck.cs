using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Deck : MonoBehaviour
{
    [SerializeField] private List<Effect> _effects = new List<Effect>();
    [SerializeField] private List<Effect> _unusedEffects = new List<Effect>();
    [SerializeField] private List<Effect> _usedEffects = new List<Effect>();

    [SerializeField] private Hand _hand;

    private void Start()
    {
        for (int i = 0; i < _effects.Count; i++)
        {
            Effect effect = Instantiate(_effects[i]);
            _unusedEffects.Add(effect);
        }

        RandomSort(_unusedEffects);

        int randomNumberOfCards = Random.Range(4, 7);

        SetCardsToHand(randomNumberOfCards);
    }

    void SetCardsToHand(int number)
    {
        for (int i = 0; i < number; i++)
        {
            _hand.AddCard(_unusedEffects[0]);
            _unusedEffects.RemoveAt(0);
        }
    }

    public void RandomSort(List<Effect> effects)
    {
        for (int i = 0; i < effects.Count; i++)
        {
            Effect oldValue = effects[i];
            int newIndex = Random.Range(0, effects.Count);
            effects[i] = effects[newIndex];
            effects[newIndex] = oldValue;
        }
    }
}