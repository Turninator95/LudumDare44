using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("References"),SerializeField]
    private GameObject scope;
    [SerializeField]
    private GameObject bullet, gun;
    [SerializeField, Range(1.0f, 20.0f)]
    [Header("Variables")]
    private float runningSpeed = 1;
    private bool bulletFired = false;
    [SerializeField]
    private float fireTimeout = 5f, bulletSpeed = 5f;
    [SerializeField]
    private float blockMaxTime = 0.5f, scopeDistance = 2;

    private float blockTimePassed = 0;

    [SerializeField]
    private int initialAmmo = 20, maxAmmo = 100, costPerShot = 1;
    private int currentAmmo;


    private Rigidbody rigidbody;
    private GameObject blockObject; 


    // Start is called before the first frame update
    void Start()
    {
        currentAmmo = initialAmmo;
        rigidbody = gameObject.GetComponent<Rigidbody>();
        blockObject = GameObject.Find("BlockObj");
        if (scope == null)
        {
            scope = GameObject.Find("Scope");
        }

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
        
        rigidbody.velocity = 
            new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"))
             * runningSpeed;

    }
    private void ProcessAimingInput()
    {
        if (Input.GetAxis("HorizontalAim") != 0 || Input.GetAxis("VerticalAim") != 0)
        {
            scope.transform.position = gun.transform.position + 
                new Vector3(Input.GetAxis("HorizontalAim") * scopeDistance, 0, Input.GetAxis("VerticalAim")*scopeDistance);
            gun.transform.LookAt(scope.transform);
            if(gun.transform.eulerAngles.y > 0 && gun.transform.eulerAngles.y < 180)
            {
                gun.transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                gun.transform.localScale = new Vector3(1, 1, 1);
            }
            //gun.transform.rotation = Quaternion.Euler(90, gun.transform.rotation.eulerAngles.y, gun.transform.rotation.eulerAngles.z);

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
                Instantiate(bullet, gun.transform.GetChild(1).transform.position, gun.transform.rotation).GetComponent<Bullet>().speed = bulletSpeed;

                bulletFired = true;
                StartCoroutine(ResetBulletFired());
                currentAmmo -= costPerShot;
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
}
