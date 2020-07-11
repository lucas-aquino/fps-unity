using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;
using System;

public class AmmoStateController : MonoBehaviour
{
    public Text textComponent;

    public GameObject parent;

    public int[] textSize;

    private void Start()
    {
        parent.gameObject.SetActive(false);
        updateAmmoText.updateAmmo += updateAmmo;
        updateAmmoText.setVisible += setVisible;
    }

    private void setVisible(bool visible)
    {
        parent.gameObject.SetActive(visible);
    }

    private void updateAmmo(int currentAmmo, int maxAmmo)
    {
        textComponent.text = $"" +
            $"<color=#ffffffaa><size={textSize[0]}>{currentAmmo}</size></color>" +
            $"<color=#aaaaaaaa><size={textSize[1]}>/{maxAmmo}</size></color>";
    }

}

public static class updateAmmoText
{
    public static Action<int, int> updateAmmo;
    public static Action<bool> setVisible;
}
