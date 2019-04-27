using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private GameObject scope, bullet, gun;
    [SerializeField, Range(1.0f, 20.0f)]
    private float runningSpeed = 1;
    private bool bulletFired = false;
    [SerializeField]
    private float fireTimeout = 5f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * Time.deltaTime * runningSpeed;

        if (Input.GetAxis("HorizontalAim") != 0 || Input.GetAxis("VerticalAim") != 0)
        {
            scope.transform.position = transform.position + new Vector3(Input.GetAxis("HorizontalAim"), 0, Input.GetAxis("VerticalAim"));

            gun.transform.LookAt(scope.transform);
            gun.transform.rotation = Quaternion.Euler(90, gun.transform.rotation.eulerAngles.y, gun.transform.rotation.eulerAngles.z);

        }
        else if (Input.GetAxis("HorizontalAim") == 0 && Input.GetAxis("VerticalAim") == 0)
        {
            scope.transform.position = transform.position;
        }

        if (Input.GetAxis("Fire1") > 0 && !bulletFired)
        {
            Instantiate(bullet, transform.position, gun.transform.rotation);
            bulletFired = true;
            StartCoroutine(ResetBulletFired());
        }
    }

    IEnumerator ResetBulletFired()
    {
        yield return new WaitForSeconds(fireTimeout);
        bulletFired = false;
    }
}
