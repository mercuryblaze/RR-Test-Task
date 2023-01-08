using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    [SerializeField] protected List<Card> _cards = new List<Card>();
    public List<Card> Cards => _cards;

    [SerializeField] private Card _cardPrefab;
    [SerializeField] private float _distanceBetween = 25f;
    [SerializeField] private float _angleBetween = 20f;

    private Card _howeredCard;

    private void Update()
    {
        SetCardsPositions();
    }

    public void AddCard(Effect effect)
    {
        Card newCard = Instantiate(_cardPrefab, transform);
        newCard.Init(effect, this);
        _cards.Add(newCard);
    }

    void SetCardsPositions()
    {
        float totalWidth = 0f;
        for (int i = 0; i < _cards.Count; i++)
        {
            if (_cards[i].IsHowered)
                totalWidth += _distanceBetween * 2f;
            else
                totalWidth += _distanceBetween;
        }
        float angleOffset = _cards.Count * 0.5f * _angleBetween;
        float offset = -totalWidth / 2f;
        for (int i = 0; i < _cards.Count; i++)
        {
            float scale = 1f;
            float x = offset;
            if (_cards[i].IsHowered)
            {
                offset += _distanceBetween * 2f;
                x += _distanceBetween / 2f;
                scale = 1.4f;
            }
            else
            {
                offset += _distanceBetween;
            }
            float angle = i * _angleBetween - angleOffset;
            if (_cards[i].IsHowered)
            {
                angle = 0f;
            }

            float y = Mathf.Abs(x) * -0.5f;

            if (_cards[i].IsSelected)
            {
                Vector3 position = transform.InverseTransformPoint(Input.mousePosition);
                _cards[i].SetPose(position, 0f, 1.2f);
            }
            else
            {
                _cards[i].SetPose(new Vector3(x, y, 0f), -angle, scale);
            }
        }
    }

    public void ResetCardsOrder()
    {
        for (int i = 0; i < _cards.Count; i++)
        {
            _cards[i].transform.SetSiblingIndex(i);
        }
    }

    public void EnterCard(Card card)
    {
        if (_howeredCard && _howeredCard.IsSelected) return;

        card.IsHowered = true;
        card.transform.SetAsLastSibling();
        _howeredCard = card;
    }

    public void ExitCard(Card card)
    {
        if (_howeredCard && _howeredCard.IsSelected) return;

        card.IsHowered = false;
        ResetCardsOrder();
        if (_howeredCard == card)
        {
            _howeredCard = null;
        }
    }

    public void PointerDownCard(Card card)
    {
        card.IsSelected = true;
    }

    public void PointerUpCard(Card card)
    {
        card.IsSelected = false;
    }
}