using System.Collections;   
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerWeaponController : MonoBehaviour
{
    public List<WeaponController> startingWeapons = new List<WeaponController>();
    public Transform weaponParentSocket;
    public Transform defaultWeaponPosition;
    public Transform aimingPosition;

    public int activeWeaponIndex { get; private set; }

    private WeaponController[] weaponSlots = new WeaponController[5];

    private void Start()
    {
        activeWeaponIndex = -1;

        foreach(WeaponController staringWeapon in startingWeapons)
        {
            AddWeapon(staringWeapon);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            switchWeapon(0);
            ammoEvent.setVisible(true);
            updateAmmoText.setVisible(true);
        }
    }

    private void switchWeapon(int p_weaponIndex)
    {
        if (p_weaponIndex != activeWeaponIndex && p_weaponIndex >= 0)
        {
            weaponSlots[p_weaponIndex].gameObject.SetActive(true);
            activeWeaponIndex = p_weaponIndex;
        }
    }

    private void AddWeapon(WeaponController p_weaponPrefab)
    {
        weaponParentSocket.position = defaultWeaponPosition.position;

        //Añadir arma al jugador pero no mostrarla
        for (int i = 0; i < weaponSlots.Length; i++)
        {
            if (weaponSlots[i] == null)
            {
                WeaponController weaponClone = Instantiate(p_weaponPrefab, weaponParentSocket);
                weaponClone.owner = gameObject;
                weaponClone.gameObject.SetActive(false);

                weaponSlots[i] = weaponClone;
                return;
            }
        }


    }

}
