using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Copyrights © Slver Studios 2020 All rights reserved.

public class Colorchange : MonoBehaviour
{
    private Color OrginalMat;

    private void Start()
    {
        OrginalMat = GetComponent<Renderer>().material.color;

    }

    public void OnTriggerEnter2D(Collider2D human)
    {
        //Debug.Log(human.gameObject.tag + "script2");

        if (human.gameObject.CompareTag("Player"))
        {

            transform.GetComponent<Renderer>().material.color = OrginalMat;

        }

        if (human.gameObject.CompareTag("iPlayer"))
        {

            transform.GetComponent<Renderer>().material.color = Color.red;
        }

    }
}