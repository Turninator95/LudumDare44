using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable, CreateAssetMenu(menuName = "Game Settings")]
public class GameSettings : ScriptableObject
{
    [SerializeField]
    private DifficultyLevel difficultyLevel;
    #region Properties
    public DifficultyLevel DifficultyLevel { get => difficultyLevel; set => difficultyLevel = value; }
    #endregion
}
