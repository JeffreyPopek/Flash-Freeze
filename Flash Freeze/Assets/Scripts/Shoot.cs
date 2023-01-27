using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] Transform firePoint;
    [SerializeField] GameObject iceMagicPrefab;

    void Update()
    {
        //fire1 is left click
        if (Input.GetButtonDown("Fire1"))
        {
            ShootProjectile();
        }
    }

    void ShootProjectile()
    {
        Instantiate(iceMagicPrefab, firePoint.position, firePoint.rotation);
    }
}

