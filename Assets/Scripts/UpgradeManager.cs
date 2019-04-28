using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradeManager : MonoBehaviour
{
    private PlayerStatus playerStatus;
    private List<WeaponUpgrade> weaponUpgrades = new List<WeaponUpgrade>();
    private WeaponUpgrade currentWeaponUpgrade;
    private AmmoDropUpgrade ammoDropUpgrade;
    private MaxAmmoUpgrade maxAmmoUpgrade;
    [SerializeField]
    private TMP_Text gunText;
    private HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        healthBar = FindObjectOfType<HealthBar>();
        playerStatus = Resources.Load<PlayerStatus>("PlayerStatus");
        ammoDropUpgrade = Resources.Load<AmmoDropUpgrade>("PlayerUpgrades/AmmoDrop");
        maxAmmoUpgrade = Resources.Load<MaxAmmoUpgrade>("PlayerUpgrades/MaxAmmo");
        weaponUpgrades.AddRange(Resources.LoadAll<WeaponUpgrade>("PlayerUpgrades/Guns"));

        int index; 

        do
        {
           index = Random.Range(0, weaponUpgrades.Count);
        } while (weaponUpgrades[index].GunToEquip == playerStatus.EquippedGun);

        currentWeaponUpgrade = weaponUpgrades[index];
        gunText.text = $"Cost: {currentWeaponUpgrade.UpgradeCost}\n{currentWeaponUpgrade.GunToEquip.name}";
        healthBar.maxHealth = playerStatus.MaxAmmo;
        healthBar.health = playerStatus.CurrentAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UnlockMaxHealthUpgrade()
    {
        if (playerStatus.CurrentAmmo > maxAmmoUpgrade.UpgradeCost)
        {
            playerStatus.CurrentAmmo -= maxAmmoUpgrade.UpgradeCost;
            playerStatus.PlayerUpgrades.Add(maxAmmoUpgrade);
        }
    }
    public void UnlockAmmoDropUpgrade()
    {
        if (playerStatus.CurrentAmmo > ammoDropUpgrade.UpgradeCost)
        {
            playerStatus.CurrentAmmo -= ammoDropUpgrade.UpgradeCost;
            playerStatus.PlayerUpgrades.Add(ammoDropUpgrade);
        }
    }
    public void UnlockWeaponUpgrade()
    {
        if (playerStatus.CurrentAmmo > currentWeaponUpgrade.UpgradeCost)
        {
            playerStatus.CurrentAmmo -= currentWeaponUpgrade.UpgradeCost;
            playerStatus.EquippedGun = currentWeaponUpgrade.GunToEquip;
        }
    }
}
