using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    private Camera mainCam;
    private Vector3 mousePos;

    [SerializeField] private GameObject spell;
    [SerializeField] private Transform spellTransform;

    [SerializeField] private bool canFire;
    private float timer;
    [SerializeField] private float timeBetweenFiring;


    private void Awake()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }


    void Update()
    {
        //Magic can't hit ice spike box collider
        Physics2D.IgnoreLayerCollision(8, 9);

        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        Vector3 rotation = mousePos - transform.position;

        //Radians to degrees
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, rotZ);

        if (!canFire)
        {
            timer += Time.deltaTime;

            if (timer > timeBetweenFiring)
            {
                canFire = true;
                timer = 0;
            }
        }

        if (Input.GetMouseButton(0) && canFire)
        {
            ShootSpell();
        }
    }


    public void ShootSpell()
    {
        canFire = false;
        Instantiate(spell, spellTransform.position, Quaternion.identity);
        FindObjectOfType<AudioManager>().Play("PlayerSpell");
    }


}



