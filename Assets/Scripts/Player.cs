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
    private float fireTimeout = 5f;
    [SerializeField]
    private float scopeDistance = 2;

    [SerializeField]
    private int initialAmmo = 20, maxAmmo = 100, costPerShot = 1;
    private int currentAmmo;





    // Start is called before the first frame update
    void Start()
    {
        currentAmmo = initialAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        ProcessMovementInput();
        ProcessAimingInput();
        ProcessShooting();

        if (currentAmmo <= 0)
        {
            Debug.Log("YOU DED SON");
        }
    }

    private void ProcessMovementInput()
    {
        transform.position = transform.position + 
            new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) 
            * Time.deltaTime 
            * runningSpeed;
    }
    private void ProcessAimingInput()
    {
        if (Input.GetAxis("HorizontalAim") != 0 || Input.GetAxis("VerticalAim") != 0)
        {
            scope.transform.position = gun.transform.position + 
                new Vector3(Input.GetAxis("HorizontalAim") * scopeDistance, 0, Input.GetAxis("VerticalAim")*scopeDistance);
            gun.transform.LookAt(scope.transform);
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
                Instantiate(bullet, gun.transform.position, gun.transform.rotation);
                bulletFired = true;
                StartCoroutine(ResetBulletFired());
                currentAmmo -= costPerShot;
                Debug.Log(currentAmmo);
            }
        }
    }

    private IEnumerator ResetBulletFired()
    {
        yield return new WaitForSeconds(fireTimeout);
        Debug.Log("You can shoot again!!!!!");
        bulletFired = false;
    }
}
