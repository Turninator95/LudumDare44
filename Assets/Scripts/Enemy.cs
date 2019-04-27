using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("References"), SerializeField]
    private GameObject gun;
    [SerializeField]
    private Gun equippedGun;
    [Header("Actions")]
    [SerializeField]
    private List<EnemyActions> actions = new List<EnemyActions>();
    private int actionIndex;
    [SerializeField]
    private float actionTimeout = 2f;
    [Header("Movement")]
    [SerializeField]
    private float movementSpeed = 1f;
    [SerializeField, Header("Ammo")]
    private int initialAmmo = 20;
    [SerializeField]
    private int maxAmmo = 100;
    public int currentAmmo;
    private bool timeoutActive = false;


    #region Properties
    public float MovementSpeed { get => movementSpeed; set => movementSpeed = value; }
    public int CurrentAmmo { get => currentAmmo; set => currentAmmo = value; }
    public Gun EquippedGun { get => equippedGun; set => equippedGun = value; }
    public GameObject Gun { get => gun; set => gun = value; }
    #endregion

    private void Start()
    {
        gun.GetComponentInChildren<SpriteRenderer>().sprite = equippedGun.GunSprite;
        actionIndex = 0;
        currentAmmo = initialAmmo;
    }

    private void Update()
    {
        if (!timeoutActive)
        {
            actions[actionIndex].Execute(this);
        }
    }

    public void ActionCompleted()
    {
        if (actionIndex < actions.Count - 1)
        {
            actionIndex++;
        }
        else
        {
            actionIndex = 0;
        }
        timeoutActive = true;
        StartCoroutine(ResetTimeout());
    }

    private IEnumerator ResetTimeout()
    {
        yield return new WaitForSeconds(actionTimeout);
        timeoutActive = false;
    }

    public void ProcessDamage(int damage)
    {
        currentAmmo -= damage;
        if (currentAmmo <= 0)
        {
            Debug.Log("it ded");
        }

    }

}
