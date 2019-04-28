using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Bullet : MonoBehaviour
{
    public string ignoreTag;
    public float speed = 1;
    public int damage = 1;
    private AudioSource audioSource;
    public AudioClip audioClip;

    // Start is called before the first frame update
    void Start()
    {
        if (audioClip != null)
        {
            audioSource = GetComponent<AudioSource>();
            audioSource.pitch *= Random.Range(1.0f, 1.5f);
            audioSource.PlayOneShot(audioClip);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * speed;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != ignoreTag)
        {
            if (other.tag == "Player")
            {
                other.gameObject.GetComponent<Player>().ProcessDamage(damage);
                Destroy(gameObject);
            }

            else if (other.tag == "Enemy")
            {
                other.gameObject.GetComponent<Enemy>().ProcessDamage(damage);
                Destroy(gameObject);
            }

            else if (other.tag == "Bullet")
            {
                return;
            }

            else if (other.tag == "Block")
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().ProcessDamage(-damage);
                Destroy(gameObject);
            }

            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        
    }
}
