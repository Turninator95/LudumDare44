using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Gun")]
public class Gun : ScriptableObject
{
    [SerializeField]
    private int costPerShot = 1, shotsPerSecond = 1, projectileSpeed = 1, projectileDamage = 1;
    [SerializeField]
    private float randomizedAngle = 5f;
    [SerializeField]
    private GameObject projectile;
    [SerializeField]
    private Sprite gunSprite;
    [SerializeField]
    private float screenShakeStrength = 0.1f, screenShakeDuration = 0.05f;
    [SerializeField]
    private AudioClip soundEffect;
    [SerializeField]
    private bool automaticFire = false;
    [SerializeField]
    private float[] shotsAndDirections = new float[] { };
    #region Properties
    public int CostPerShot { get => costPerShot; set => costPerShot = value; }
    public GameObject Projectile { get => projectile; set => projectile = value; }
    public int ProjectileSpeed { get => projectileSpeed; set => projectileSpeed = value; }
    public Sprite GunSprite { get => gunSprite; set => gunSprite = value; }
    public int ProjectileDamage { get => projectileDamage; set => projectileDamage = value; }
    public float ScreenShakeStrength { get => screenShakeStrength; set => screenShakeStrength = value; }
    public float ScreenShakeDuration { get => screenShakeDuration; set => screenShakeDuration = value; }
    public AudioClip SoundEffect { get => soundEffect; set => soundEffect = value; }
    public bool AutomaticFire { get => automaticFire; set => automaticFire = value; }
    public int ShotsPerSecond { get => shotsPerSecond; set => shotsPerSecond = value; }
    public float[] ShotsAndDirections { get => shotsAndDirections; set => shotsAndDirections = value; }
    public float RandomizedAngle { get => randomizedAngle; set => randomizedAngle = value; }
    #endregion
}
