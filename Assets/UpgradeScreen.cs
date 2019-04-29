using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class UpgradeScreen : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    [SerializeField]
    private GameObject explanationUpgrade;
    [SerializeField]
    private PlayerUpgrade playerUpgrade;
    private HealthBar healthBar;

    public PlayerUpgrade PlayerUpgrade { get => playerUpgrade; set => playerUpgrade = value; }

    public void Awake()
    {
        healthBar = FindObjectOfType<HealthBar>();
    }

    public void OnSelect(BaseEventData eventData)
    {
        explanationUpgrade.gameObject.SetActive(true);
        if (playerUpgrade != null)
        {
            healthBar.shotDamage = playerUpgrade.UpgradeCost;
        }
        else
        {
            healthBar.shotDamage = 0;
        }
    }

    public void OnDeselect(BaseEventData eventData)
    {
        explanationUpgrade.gameObject.SetActive(false);
    }

}
