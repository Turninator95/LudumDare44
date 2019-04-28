using UnityEngine;
[System.Serializable, CreateAssetMenu(menuName = "Player Upgrades/Weapon")]
public class WeaponUpgrade : PlayerUpgrade
{
    [SerializeField]
    private Gun gunToEquip;
    public Gun GunToEquip { get => gunToEquip; set => gunToEquip = value; }
}
