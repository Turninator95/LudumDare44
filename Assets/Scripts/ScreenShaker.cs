using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShaker : MonoBehaviour
{
    [SerializeField]
    private float amplitude = 1;
    public float strength = 0, duration = 0;

    private Vector3 originalPosition;
    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (duration > 0)
        {
            duration -= Time.deltaTime;
            
        }else
        {
            strength = 0;
            duration = 0;
        }
        transform.position = originalPosition + new Vector3(Random.Range(-amplitude * strength, amplitude * strength), 0, Random.Range(-amplitude * strength, amplitude * strength));

        
    }
}
