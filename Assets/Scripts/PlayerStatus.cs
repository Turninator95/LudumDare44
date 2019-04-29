using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player Status"), System.Serializable]
public class PlayerStatus : ScriptableObject
{
    [SerializeField]
    private Gun equippedGun;
    [SerializeField]
    private int initialAmmo = 20, maxAmmo = 100, currentAmmo;
    [SerializeField]
    private List<PlayerUpgrade> playerUpgrades = new List<PlayerUpgrade>();
    #region Properties
    public Gun EquippedGun { get => equippedGun; set => equippedGun = value; }
    public int InitialAmmo { get => initialAmmo; set => initialAmmo = value; }
    public int MaxAmmo { get => maxAmmo; set => maxAmmo = value; }
    public int CurrentAmmo { get => currentAmmo; set => currentAmmo = value; }
    public List<PlayerUpgrade> PlayerUpgrades { get => playerUpgrades; set => playerUpgrades = value; }
    #endregion
}
