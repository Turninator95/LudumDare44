using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class UpgradeScreen : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    public GameObject explanationUpgrade;
    private HealthBar healthBar;

    public void Start()
    {
        healthBar = FindObjectOfType<HealthBar>();
    }

    public void OnSelect(BaseEventData eventData)
    {
        explanationUpgrade.gameObject.SetActive(true);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        explanationUpgrade.gameObject.SetActive(false);
    }

}
