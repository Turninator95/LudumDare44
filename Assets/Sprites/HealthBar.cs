using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class HealthBar : MonoBehaviour
{
    public Image healthBar, healthBar2;
    public float maxHealth = 100f;
    [Range(0.1f, 100f)]
    public float health;
    public float shotDamage = 10f;

    void Start()
    {
    //    healthBar = GetComponent<Image>();
    //    healthBar2 = GetComponent<Image>();
        health = maxHealth;
    }

    void Update()
    {
        healthBar.fillAmount = health / maxHealth;
        healthBar2.fillAmount = (health - shotDamage) / maxHealth;
    }
}
