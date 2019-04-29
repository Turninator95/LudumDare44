using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable, CreateAssetMenu(menuName = "Game Settings")]
public class GameSettings : ScriptableObject
{
    [SerializeField]
    private DifficultyLevel difficultyLevel;
    private int levelIndex = 0; 
    #region Properties
    public DifficultyLevel DifficultyLevel { get => difficultyLevel; set => difficultyLevel = value; }
    public int LevelIndex { get => levelIndex; set => levelIndex = value; }
    #endregion
}
