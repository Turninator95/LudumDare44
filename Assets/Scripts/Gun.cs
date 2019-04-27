using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Gun")]
public class Gun : ScriptableObject
{
    [SerializeField]
    private int costPerShot = 1;
    [SerializeField]
    private int projectileSpeed = 1;
    [SerializeField]
    private GameObject projectile;
    [SerializeField]
    private Sprite gunSprite;
    #region Properties
    public int CostPerShot { get => costPerShot; set => costPerShot = value; }
    public GameObject Projectile { get => projectile; set => projectile = value; }
    public int ProjectileSpeed { get => projectileSpeed; set => projectileSpeed = value; }
    #endregion
}
