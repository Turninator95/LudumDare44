using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("References"), SerializeField]
    private GameObject scope;
    [SerializeField]
    private GameObject gun;
    [SerializeField, Range(1.0f, 20.0f)]
    [Header("Variables")]
    private float runningSpeed = 1;
    private bool gunReady = true;
    [SerializeField]
    private float fireTimeout = 5f;
    [SerializeField]
    private float blockMaxTime = 0.5f, scopeDistance = 2;
    private float blockTimePassed = 0;

    private Rigidbody rigidbody;
    private GameObject blockObject;
    private ScreenShaker screenShaker;
    private bool gamePadWasUsed = false;
    private AudioSource audioSource;
    private HealthBar healthBar;
    private GameManager gameManager;
    private PlayerStatus playerStatus;

    public PlayerStatus PlayerStatus { get => playerStatus; set => playerStatus = value; }

    // Start is called before the first frame update
    void Start()
    {
        FindReferences();
        foreach (PlayerUpgrade playerUpgrade in playerStatus.PlayerUpgrades)
        {
            if (playerUpgrade.GetType() == typeof(MaxAmmoUpgrade))
            {
                playerStatus.MaxAmmo++;
            }
        }
        UpdateHealthBar();
        gun.GetComponentInChildren<SpriteRenderer>().sprite = playerStatus.EquippedGun.GunSprite;
    }

    // Update is called once per frame
    void Update()
    {
        ProcessMovementInput();
        ProcessAimingInput();
        ProcessShooting();
        ProcessBlocking();

        if (playerStatus.CurrentAmmo <= 0)
        {
            gameManager.GameOver();
            Destroy(gameObject);
            Debug.Log("YOU DED SON");
        }

        UpdateHealthBar();
    }

    private void FindReferences()
    {
        gameManager = FindObjectOfType<GameManager>();
        healthBar = FindObjectOfType<HealthBar>();

        audioSource = GetComponent<AudioSource>();
        screenShaker = Camera.main.GetComponent<ScreenShaker>();

        if (screenShaker == null)
        {
            screenShaker = Camera.main.gameObject.AddComponent<ScreenShaker>();
        }

        rigidbody = gameObject.GetComponent<Rigidbody>();
        blockObject = GameObject.Find("BlockObj");
        if (scope == null)
        {
            scope = GameObject.Find("Scope");
        }
        playerStatus = Resources.Load<PlayerStatus>("PlayerStatus");
    }

    private void UpdateHealthBar()
    {
        healthBar.maxHealth = playerStatus.MaxAmmo;
        healthBar.health = playerStatus.CurrentAmmo;
        healthBar.shotDamage = playerStatus.EquippedGun.CostPerShot;
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
            scope.transform.position = gun.transform.position + new Vector3(Input.GetAxis("HorizontalAim") * scopeDistance, 0, Input.GetAxis("VerticalAim") * scopeDistance);
            gun.transform.LookAt(scope.transform);
            if (gun.transform.eulerAngles.y > 0 && gun.transform.eulerAngles.y < 180)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1);
            }

            if (scope.transform.position.x > transform.position.x)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            gun.transform.LookAt(scope.transform);

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

            if (scope.transform.position.x > transform.position.x)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            gun.transform.LookAt(scope.transform);
        }
        else if (Input.GetAxis("HorizontalAim") == 0 && Input.GetAxis("VerticalAim") == 0)
        {
            scope.transform.position = transform.position;
        }
    }

    private void ProcessShooting()
    {
        if (Input.GetAxis("Fire1") > 0 && gunReady)
        {
            if (playerStatus.CurrentAmmo - playerStatus.EquippedGun.CostPerShot > 0)
            {
                for (int i = 0; i < playerStatus.EquippedGun.ShotsAndDirections.Length; i++)
                {
                    float angle = playerStatus.EquippedGun.ShotsAndDirections[i];
                    Bullet bullet = Instantiate(playerStatus.EquippedGun.Projectile, gun.transform.GetChild(1).transform.position, gun.transform.rotation).GetComponent<Bullet>();
                    bullet.damage = playerStatus.EquippedGun.ProjectileDamage;
                    bullet.speed = playerStatus.EquippedGun.ProjectileSpeed;
                    bullet.bulletSource = tag;
                    bullet.transform.Rotate(new Vector3(0, angle + Random.Range(-playerStatus.EquippedGun.RandomizedAngle, playerStatus.EquippedGun.RandomizedAngle), 0));
                    if (i == 0)
                    {
                        bullet.audioClip = playerStatus.EquippedGun.SoundEffect;
                    }
                    else
                    {
                        bullet.audioClip = null;
                    }
                }
                gunReady = false;

                if (playerStatus.EquippedGun.AutomaticFire)
                {
                    fireTimeout = (float)1 / playerStatus.EquippedGun.ShotsPerSecond;
                    StartCoroutine(ResetBulletFired());
                }

                ProcessDamage(playerStatus.EquippedGun.CostPerShot);
                screenShaker.strength += playerStatus.EquippedGun.ScreenShakeStrength;
                screenShaker.duration += playerStatus.EquippedGun.ScreenShakeDuration;
                Debug.Log($"{name} has {playerStatus.CurrentAmmo} ammo left.");
            }
        }
        else if (Input.GetAxis("Fire1") == 0)
        {
            gunReady = true;
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
        gunReady = true;
    }

    public void ProcessDamage(int damage)
    {
        audioSource.pitch *= Random.Range(0.8f, 1.2f);
        audioSource.Play();

        if (playerStatus.CurrentAmmo - damage <= playerStatus.MaxAmmo)
        {
            playerStatus.CurrentAmmo -= damage;
        }

        UpdateHealthBar();

        if (playerStatus.CurrentAmmo <= 0)
        {
            Debug.Log("We ded");
            gameManager.GameOver();
        }
    }

    public void StageCompleted()
    {
        foreach (PlayerUpgrade playerUpgrade in playerStatus.PlayerUpgrades)
        {
            if (playerUpgrade.GetType() == typeof(AmmoDropUpgrade))
            {
                ProcessDamage(-1);
            }
        }
    }
}
