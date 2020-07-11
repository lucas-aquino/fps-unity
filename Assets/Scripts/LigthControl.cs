using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LigthControl : MonoBehaviour
{

    Light lantern;


    private void Awake()
    {
        lantern = GetComponent<Light>();
        lantern.enabled = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            lantern.enabled = !lantern.enabled;
        }
    }




}
