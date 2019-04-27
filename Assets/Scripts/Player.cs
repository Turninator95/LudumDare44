﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("References"),SerializeField]
    private GameObject scope;
    [SerializeField]
    private Gun equippedGun;
    [SerializeField]
    private GameObject gun;
    [SerializeField, Range(1.0f, 20.0f)]
    [Header("Variables")]
    private float runningSpeed = 1;
    private bool bulletFired = false;
    [SerializeField]
    private float fireTimeout = 5f;
    [SerializeField]
    private float blockMaxTime = 0.5f, scopeDistance = 2;
    private float blockTimePassed = 0;

    [SerializeField]
    private int initialAmmo = 20, maxAmmo = 100, costPerShot = 1;
    private int currentAmmo;

    private Rigidbody rigidbody;
    private GameObject blockObject;
    private ScreenShaker screenShaker;
    private bool gamePadWasUsed = false;


    // Start is called before the first frame update
    void Start()
    {
        screenShaker = Camera.main.GetComponent<ScreenShaker>();
        if (screenShaker == null)
        {
            screenShaker = Camera.main.gameObject.AddComponent<ScreenShaker>();
        }
        currentAmmo = initialAmmo;
        rigidbody = gameObject.GetComponent<Rigidbody>();
        blockObject = GameObject.Find("BlockObj");
        if (scope == null)
        {
            scope = GameObject.Find("Scope");
        }
        gun.GetComponentInChildren<SpriteRenderer>().sprite = equippedGun.GunSprite;
    }

    // Update is called once per frame
    void Update()
    {
        ProcessMovementInput();
        ProcessAimingInput();
        ProcessShooting();
        ProcessBlocking();

        if (currentAmmo <= 0)
        {
            Debug.Log("YOU DED SON");
        }
    }

    private void ProcessMovementInput()
    {
        rigidbody.velocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * runningSpeed;
    }

    private void ProcessAimingInput()
    {
        if (Input.GetAxis("HorizontalAim") != 0 || Input.GetAxis("VerticalAim") != 0)
        {
            gamePadWasUsed = true;
            scope.transform.position = gun.transform.position + 
                new Vector3(Input.GetAxis("HorizontalAim") * scopeDistance, 0, Input.GetAxis("VerticalAim") * scopeDistance);
            gun.transform.LookAt(scope.transform);
            if(gun.transform.eulerAngles.y > 0 && gun.transform.eulerAngles.y < 180)
            {
                gun.transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                gun.transform.localScale = new Vector3(1, 1, 1);
            }

        }
        else if (!gamePadWasUsed)
        {
            Vector3 mousePos = Vector3.zero;
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            mousePos.y = gun.transform.position.y;
            Vector3 tmpGunPos = gun.transform.position;
            tmpGunPos.Scale(new Vector3(1, 0, 1));

            if (Vector3.Distance(mousePos, tmpGunPos) > scopeDistance)
            {
                scope.transform.position = gun.transform.position;
                scope.transform.LookAt(mousePos);
                scope.transform.position += scope.transform.forward * scopeDistance;
                scope.transform.eulerAngles = Vector3.zero;

            }
            else
            {
                scope.transform.position = mousePos;
            }
            
            gun.transform.LookAt(scope.transform);
            if (gun.transform.eulerAngles.y > 0 && gun.transform.eulerAngles.y < 180)
            {
                gun.transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                gun.transform.localScale = new Vector3(1, 1, 1);
            }
        }
        else if (Input.GetAxis("HorizontalAim") == 0 && Input.GetAxis("VerticalAim") == 0)
        {
            scope.transform.position = transform.position;
        }
    }
    private void ProcessShooting()
    {
        if (Input.GetAxis("Fire1") > 0 && !bulletFired)
        {
            if (currentAmmo - costPerShot > 0)
            {
                Bullet bullet = Instantiate(equippedGun.Projectile, gun.transform.GetChild(1).transform.position, gun.transform.rotation).GetComponent<Bullet>();
                bullet.damage = equippedGun.ProjectileDamage;
                bullet.speed = equippedGun.ProjectileSpeed;
                bullet.ignoreTag = tag;
                bullet.audioClip = equippedGun.SoundEffect;

                bulletFired = true;
                StartCoroutine(ResetBulletFired());
                ProcessDamage(costPerShot);
                screenShaker.strength += equippedGun.ScreenShakeStrength;
                screenShaker.duration += equippedGun.ScreenShakeDuration;
                Debug.Log(currentAmmo);
            }
        }
    }
    private void ProcessBlocking()
    {
        bool setActive = false;
        if (Input.GetAxis("Fire2") > 0)
        {
            if(blockTimePassed < blockMaxTime)
            {
                blockTimePassed += Time.deltaTime;
                setActive = true;
                blockObject.transform.position = scope.transform.position;
                blockObject.transform.rotation = gun.transform.rotation;
            }
        }
        else
        {
            
            blockTimePassed -= Time.deltaTime;
            blockTimePassed = Mathf.Clamp(blockTimePassed, 0, blockMaxTime);
        }
        blockObject.SetActive(setActive);

    }

    private IEnumerator ResetBulletFired()
    {
        yield return new WaitForSeconds(fireTimeout);
        Debug.Log("You can shoot again!!!!!");
        bulletFired = false;
    }
    public void ProcessDamage(int damage)
    {
        currentAmmo -= damage;
        if (currentAmmo <= 0)
        {
            Debug.Log("We ded");
        }
    }
}
