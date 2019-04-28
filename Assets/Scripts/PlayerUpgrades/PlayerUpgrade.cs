using UnityEngine;

[System.Serializable]
public abstract class PlayerUpgrade : ScriptableObject
{
    [SerializeField]
    private int upgradeCost = 1;
    #region Properties
    public int UpgradeCost { get => upgradeCost; set => upgradeCost = value; }
    #endregion
    public abstract void Process(); 
}
