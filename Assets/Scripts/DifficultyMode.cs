using UnityEngine;

[CreateAssetMenu(menuName = "Difficulty Mode"), System.Serializable]
public class DifficultyMode : ScriptableObject
{
    [SerializeField]
    private DifficultyLevel difficultyLevel;
    [SerializeField]
    private int playerInitialAmmo, playerMaxAmmo;

    #region Properties
    public DifficultyLevel DifficultyLevel { get => difficultyLevel; set => difficultyLevel = value; }
    public int PlayerInitialAmmo { get => playerInitialAmmo; set => playerInitialAmmo = value; }
    public int PlayerMaxAmmo { get => playerMaxAmmo; set => playerMaxAmmo = value; }
    #endregion
}
