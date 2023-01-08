using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum CharacterType
{
    Player,
    Enemy
}

[CreateAssetMenu(fileName = nameof(Effect), menuName = "Effects/" + nameof(Effect))]
public class Effect : ScriptableObject
{
    public CharacterType TargetCharacterType;

    public string Name;
    public string Description;
    public int Price;
    public int Attack;
    public int HP;
    public Sprite Sprite;

}
