using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameDatas", menuName = "Game / Datas")]
public class GameDataSO : ScriptableObject
{
    [Header("Difficulty Game Settings")]
    public Difficulty difficulty;
    public int rows;
    public int columns;

    [Header("Card Background Image")]
    public Sprite background;

    [Header("Grid Layout Variables")]
    public int preferredTopBottomPadding;
    public Vector2 spacing;
}
