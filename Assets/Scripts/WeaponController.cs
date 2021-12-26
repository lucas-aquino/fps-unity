using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ObjectPool))]
public class WeaponController : MonoBehaviour
{

    [Header("References")]
    public Transform weaponMuzzle;

    [Header("General")]
    public LayerMask hittableLayers;
    public GameObject bulletHolePrefab;

    public ObjectPool bulletHolePool;

    [Header("Shoot Parameter")]
    public float fireRage = 200;
    public float recoilForce = 4f; // retroceso del arma
    public float fireRate = 0.6f;
    public int maxAmmo = 8;
    public int currentAmmo { get; private set; }

    private float lastTimeShoot = Mathf.NegativeInfinity;

    [Header("Reload Parameters")]
    public float reloadTime = 1.5f;

    [Header("Sound & Visual")]
    public GameObject flashEffect;
    public GameObject[] shootSound;
    public GameObject reloadSound;

    public GameObject owner { set; get; }

    private bool canReload = true;

    private Transform cameraPlayerTransform;

    private void Awake()
    {
        currentAmmo = maxAmmo;
    }

    private void Start()
    {
        cameraPlayerTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;
        bulletHolePool = GetComponent<ObjectPool>();
    }

    private void FixedUpdate()
    {
        ControlListener();
        transform.localPosition = Vector3.Lerp(transform.localPosition, Vector3.zero, Time.deltaTime * 5f);
        ammoEvent.newValue(currentAmmo);
        updateAmmoText.updateAmmo(currentAmmo, maxAmmo);
    }

    private void ControlListener()
    {
        if (Input.GetKey(KeyCode.Mouse0))
            TryShoot();
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (canReload && currentAmmo < maxAmmo) 
                StartCoroutine(Reload());
        }

    }

    private bool TryShoot()
    {

        if (lastTimeShoot + fireRate < Time.time)
        {
            if (currentAmmo >= 1)
            {
                HandleShoot();
                currentAmmo--;
                return true;
            }
        }
        return false;
    }

    private void HandleShoot()
    {
        AddFlash();
        AddRecoil();
        AddSoundShoot();
        
        RaycastHit[] hits;

        hits = Physics.RaycastAll(cameraPlayerTransform.position, cameraPlayerTransform.forward, fireRage, hittableLayers);

        foreach(RaycastHit hit in hits)
        {
            if (!hit.collider.CompareTag("NonHitiable"))
            {
                //GameObject bulletHoleClone = Instantiate(bulletHolePrefab, hit.point + hit.normal * 0.001f, Quaternion.LookRotation(hit.normal));
                //Destroy(bulletHoleClone, 5f);
                GameObject bulletHole = bulletHolePool.Spawn(hit.point + hit.normal * 0.001f, Quaternion.LookRotation(hit.normal));


            }
        }

        lastTimeShoot = Time.time;
    }

    private void AddRecoil()
    {
        transform.Rotate(-recoilForce, 0f, 0f);
        transform.position = transform.position - transform.forward * (recoilForce / 50f);
    }

    private void AddFlash()
    {
        GameObject flashClone = Instantiate(flashEffect, weaponMuzzle.position, Quaternion.Euler(weaponMuzzle.transform.forward), transform);
        Destroy(flashClone, 1f);
    }
    private void AddSoundShoot()
    {
        GameObject soundShootClone = Instantiate(shootSound[Random.Range(0, shootSound.Length)], weaponMuzzle.position, Quaternion.Euler(weaponMuzzle.forward), transform);
        Destroy(soundShootClone, 1f);
    }
    IEnumerator Reload()
    {
        //TODO empezar la animacion de recarga
        canReload = false;
        Debug.LogWarning("Recargando...");
        Destroy(Instantiate(reloadSound, transform.position, Quaternion.identity, transform), reloadTime);
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmo;
        Debug.LogWarning("Recargado");
        canReload = true;
        //TODO terminar la animacio de recarga
    }

}
