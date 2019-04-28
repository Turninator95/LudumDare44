using UnityEngine;

[System.Serializable]
public class WeaponUpgrade : PlayerUpgrade
{
    [SerializeField]
    private Gun gunToEquip;
    public Gun GunToEquip { get => gunToEquip; set => gunToEquip = value; }
}
