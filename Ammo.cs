using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ammo : MonoBehaviour
{
    public int Maxammo;
    [HideInInspector]
    public int ammo;

    public int MaxBag;
    public Text AmmoText;
    public Text AmmoBagText;

    private int subC;

    public float FireRate = 15f;
    private float NextTimeToFire = 0f;

    [SerializeField] private Transform pfBulletProjectile;
    [SerializeField] private Transform spawnBulletPosition;
    [SerializeField] private Transform SphereDebug;

    [SerializeField] private float bulletvelocity;

    private int MaxPistolBag;

    private void Start()
    {
        ammo = Maxammo;

        MaxPistolBag = MaxBag;

        pfBulletProjectile.GetComponent<BulletProjectile>().speed = bulletvelocity;
    }
    // Update is called once per frame
    void Update()
    {
        AmmoText.text = ammo.ToString(); //manda o valor da variavél para o texto na tela
        AmmoBagText.text = MaxBag.ToString();

        if (Input.GetKeyDown(KeyCode.R)  && MaxBag > 0)
        {

            if(MaxBag <= 30)
            {
                int subB = (ammo - MaxBag) * -1;
                ammo += subB;
                MaxBag -= subB;
            }
            if((MaxBag + ammo) < 30)
            {
                ammo = MaxBag + ammo;

                MaxBag -= MaxBag;
            }
            else
            {
                int sub = ammo - Maxammo;
                sub = sub * -1;
                ammo += sub;
                MaxBag -= sub;
            }

        }

        if (Input.GetButton("Fire1") && Time.time >= NextTimeToFire && ammo > 0) //atira
        {
            NextTimeToFire = Time.time + 1f / FireRate;

            ammo--;

            Vector3 aimDir = (SphereDebug.position - spawnBulletPosition.position).normalized;
            Instantiate(pfBulletProjectile, spawnBulletPosition.position, Quaternion.LookRotation(aimDir, Vector3.up));

        }
    }

    public void GiveAmmo(int Ammoamount)
    {

        if (MaxBag + Ammoamount > MaxPistolBag)
        {
            MaxBag = MaxPistolBag;
        }
        else
        {
            MaxBag +=Ammoamount; 
        }

    }
}
