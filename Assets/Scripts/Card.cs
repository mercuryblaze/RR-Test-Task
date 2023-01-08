using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    private Effect _effect;

    [SerializeField] private Image _mainImage;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _descriptionText;
    [SerializeField] private TextMeshProUGUI _priceText;
    [SerializeField] private TextMeshProUGUI _attackText;
    [SerializeField] private TextMeshProUGUI _hpText;
    [SerializeField] private TextMeshProUGUI[] _stats = new TextMeshProUGUI[3];
    [SerializeField] private NumberCounter _numberCounter;

    private Hand _hand;

    public bool IsHowered;
    public bool IsSelected;

    private Vector3 _targetPosition;
    private float _targetAngle;
    private float _targetScale;
    private float _angle;
    private float _scale = 1f;

    private void Update()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, _targetPosition, Time.deltaTime * 10f);
        _angle = Mathf.Lerp(_angle, _targetAngle, Time.deltaTime * 10f);
        _scale = Mathf.Lerp(_scale, _targetScale, Time.deltaTime * 10f);
        transform.localEulerAngles = new Vector3(0, 0, _angle);
        transform.localScale = Vector3.one * _scale;
    }

    public void Init(Effect effect, Hand hand)
    {
        _effect = effect;
        _mainImage.sprite = effect.Sprite;
        _nameText.text = effect.Name;
        _descriptionText.text = effect.Description;
        _priceText.text = effect.Price.ToString();
        _attackText.text = effect.Attack.ToString();
        _hpText.text = effect.HP.ToString();

        _hand = hand;
    }

    public void SetPose(Vector3 position, float angle, float scale)
    {
        _targetPosition = position;
        _targetAngle = angle;
        _targetScale = scale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _hand.EnterCard(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _hand.ExitCard(this);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _hand.PointerDownCard(this);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _hand.PointerUpCard(this);
    }

    public int SetRandomStats()
    {
        int rndStat = Random.Range(0, 3);
        int rndValue = Random.Range(-2, 10);

        for (int i = 0; i < 1; i++)
        {
            _numberCounter.UpdateText(int.Parse(_stats[rndStat].GetComponent<TextMeshProUGUI>().text), rndValue, _stats[rndStat]);

            if (rndStat == 2 && rndValue <= 0)
            {
                return -1;
            }
        }

        return 0;
    }
}
