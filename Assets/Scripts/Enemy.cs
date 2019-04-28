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
    [SerializeField]
    private List<EnemyActions> rageActions = new List<EnemyActions>();
    private int actionIndex;
    [SerializeField]
    private float actionTimeout = 2f, rageActionTimeout = 0.1f;
    [Header("Movement")]
    [SerializeField]
    private float movementSpeed = 1f, rageMovementSpeed = 3f;
    [SerializeField, Header("Ammo")]
    private int initialAmmo = 20;
    [SerializeField]
    private int maxAmmo = 100;
    public int currentAmmo;
    private bool timeoutActive = false;
    private AudioSource audioSource;
    private GameManager gameManager;
    [SerializeField]
    public bool takeDamageWhenFiring = true;

    GameObject player;
    [SerializeField]
    int additionalScoreMin = 2;
    [SerializeField]
    float duration = 5;
    [SerializeField]
    GameObject pickUpObj;


    #region Properties
    public float MovementSpeed { get => movementSpeed; set => movementSpeed = value; }
    public int CurrentAmmo { get => currentAmmo; set => currentAmmo = value; }
    public Gun EquippedGun { get => equippedGun; set => equippedGun = value; }
    public GameObject Gun { get => gun; set => gun = value; }
    #endregion


    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        gameManager.EnemySpawned(this);
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        gun.GetComponentInChildren<SpriteRenderer>().sprite = equippedGun.GunSprite;
        actionIndex = 0;
        currentAmmo = initialAmmo;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (!timeoutActive)
        {
            actions[actionIndex].Execute(this);
        }
        gun.transform.LookAt(player.transform);
        if (player.transform.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
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
        audioSource.pitch *= Random.Range(0.8f, 1.2f);
        audioSource.Play();
        currentAmmo -= damage;
        if (currentAmmo <= 0)
        {
            gameManager.EnemyDestroyed(this);
            Debug.Log("it ded");
            SpawnPickups();
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (actions[actionIndex].GetType() == typeof(MoveAction) && collision.collider.tag != "Bullet")
        {
            (actions[actionIndex] as MoveAction).ChangeMovementDirection(this);
        }
    }
    private void SpawnPickups ()
    {
        if(pickUpObj != null)
        {
            MoneyPickup tmpMoneyPickup = GameObject.Instantiate(pickUpObj).GetComponent<MoneyPickup>();
            tmpMoneyPickup.transform.position = transform.position;
            tmpMoneyPickup.lifetime = duration;
            Player tmpPlayer = player.GetComponent<Player>();
            int tmpMax = tmpPlayer.MaxAmmo - tmpPlayer.CurrentAmmo;
            tmpMax = Mathf.Clamp(tmpMax, additionalScoreMin +1,  tmpPlayer.MaxAmmo);
            tmpMoneyPickup.bonusLifes = Mathf.FloorToInt(Random.Range(additionalScoreMin, tmpMax));

        }
    }
    public void Enrage()
    {
        actions = rageActions;
        actionTimeout = rageActionTimeout;
        movementSpeed = rageMovementSpeed;
    }
}
