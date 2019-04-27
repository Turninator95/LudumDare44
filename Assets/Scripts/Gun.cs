using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Gun")]
public class Gun : ScriptableObject
{
    [SerializeField]
    private int costPerShot = 1, projectileSpeed = 1, projectileDamage = 1;
    [SerializeField]
    private GameObject projectile;
    [SerializeField]
    private Sprite gunSprite;
    #region Properties
    public int CostPerShot { get => costPerShot; set => costPerShot = value; }
    public GameObject Projectile { get => projectile; set => projectile = value; }
    public int ProjectileSpeed { get => projectileSpeed; set => projectileSpeed = value; }
    public Sprite GunSprite { get => gunSprite; set => gunSprite = value; }
    public int ProjectileDamage { get => projectileDamage; set => projectileDamage = value; }
    #endregion
}
