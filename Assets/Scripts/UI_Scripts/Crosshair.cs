using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using UnityEngine;
using UnityEngine.UI;

public class Crosshair : MonoBehaviour
{
    [Header("Crosshair Setting")]
    [Range(1, 10)] public int thickness = 2;
    [Range(2, 50)] public int size = 7;
    [Range(1, 100)]public int gap = 5;

    [Header("Dynamic Setting")]
    public bool setDynamic = false;
    [Range(1, 10)]public float maxSize;
    public float speed;
    private float restingSize;
    private float currentSize;

    [Header("Reticle Lines")]
    public RectTransform topLine;
    public RectTransform rightLine;
    public RectTransform bottomLine;
    public RectTransform leftLine;



    private RectTransform crosshair;

    private void Awake()
    {
        crosshair = GetComponent<RectTransform>();
        currentSize = gap;
    }

    private void Update()
    {
        setGap();
        setSize();
        crosshairDynamic();
    }

    private void setGap()
    {
        crosshair.sizeDelta = new Vector2(gap, gap);
    }

    private void setSize()
    {
        topLine.sizeDelta = new Vector2(thickness, size);
        rightLine.sizeDelta = new Vector2(size, thickness);
        bottomLine.sizeDelta = new Vector2(thickness, size);
        leftLine.sizeDelta = new Vector2(size, thickness);
    }

    private void crosshairDynamic()
    {
        if (setDynamic)
        {
            restingSize = gap;
            if (isMoving) currentSize = Mathf.Lerp(currentSize, gap * maxSize, Time.deltaTime * speed);
            else currentSize = Mathf.Lerp(currentSize, restingSize, Time.deltaTime * speed);

            crosshair.sizeDelta = new Vector2(currentSize, currentSize);
        }
    }


    private bool isMoving
    {
        get {
            if (Input.GetAxis("Horizontal") != 0 ||
                Input.GetAxis("Vertical") != 0 ) return true;
            else return false;
        }
    }
}