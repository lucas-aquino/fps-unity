using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SliderAmmoController : MonoBehaviour
{

    Slider slider;

    public int currentValue;
    public int maxValue = 8;
    [Range(0.01f, 0.9f)]public float animationSpeed = 0.1f;

    private void Start()
    {
        this.gameObject.SetActive(false);
        slider = GetComponent<Slider>();
        ammoEvent.newValue = changeValue;
        ammoEvent.maxValue = setMaxValue;
        ammoEvent.setVisible = setVisible;
        currentValue = maxValue;
    }

    private void changeValue(int value)
    {
        currentValue = value;
    }

    private void setMaxValue(int value)
    {
        maxValue = value;
    }

    private void setVisible(bool visible)
    {
        this.gameObject.SetActive(visible);
    }

    private void FixedUpdate()
    {
        currentValue = Mathf.Clamp(currentValue, 0, maxValue);
        slider.value = Mathf.Lerp(slider.value, (float)currentValue / (float)maxValue, 0.1f);
    }


}


public static class ammoEvent
{
    public static Action<int> newValue;
    public static Action<int> maxValue;
    public static Action<bool> setVisible;
}

