using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class UpgradeScreen : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    public GameObject explanationUpgrade;

    public void Start()
    {
        
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
