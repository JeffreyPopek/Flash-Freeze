using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] Transform firePoint;
    [SerializeField] GameObject iceMagicPrefab;

    [SerializeField] private int fireRate = 1;

    private bool allowMagic = true;

    void Update()
    {
        //fire1 is left click
        if (Input.GetButtonDown("Fire1") && allowMagic)
        {
            StartCoroutine(ShootProjectile());
            //ShootProjectile();
        }
    }

    IEnumerator ShootProjectile()
    {
        allowMagic = false;
        Instantiate(iceMagicPrefab, firePoint.position, firePoint.rotation);
        yield return new WaitForSeconds(fireRate);
        allowMagic = true;   
    }

}

