﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideShadow : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {

        foreach (var item in GetComponentsInChildren<SpriteRenderer>())
        {
            if (item.name.Contains("Shadow"))
            {
                spriteRenderer = item;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Wall")
        {
            spriteRenderer.enabled = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Wall")
        {
            spriteRenderer.enabled = true;
        }
    }
}
