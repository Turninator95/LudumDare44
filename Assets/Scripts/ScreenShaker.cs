using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShaker : MonoBehaviour
{
    [SerializeField]
    private float amplitude = 1;
    public float strength = 0, duration = 0;

    Transform target;
    public int dampingFrames = 5;

    List<Vector3> positions = new List<Vector3>();

    private Vector3 originalPosition;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Scope").GetComponent<Transform>();
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
       

        Vector3 tmpV3 = Vector3.zero;
        positions.Add(target.position);
        if (positions.Count >= dampingFrames)
        {
            foreach (var item in positions)
            {
                tmpV3 += item;
            }
            tmpV3 /= positions.Count;
            positions.RemoveAt(0);
        }

        originalPosition = new Vector3(tmpV3.x, transform.position.y, tmpV3.z);
        transform.position = originalPosition + new Vector3(Random.Range(-amplitude * strength, amplitude * strength), 0, Random.Range(-amplitude * strength, amplitude * strength));
    }
}
