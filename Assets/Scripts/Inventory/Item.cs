using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName ="Scriptable object/Item")]
public class Item : ScriptableObject
{
    [Header("Gameplay")]
    public ItemType type;
    public ActionType actionType;
    public int id;

    [Header("UI")]
    public bool stackable=true;

    [Header("Both")]
    public Sprite image;

    public enum ItemType
    {
        Tool,
        Weapon,
        Potion,
        Quest
    }

    public enum ActionType
    {
        Break,
        Use,
        Attack,
        HandOver
    }
}
