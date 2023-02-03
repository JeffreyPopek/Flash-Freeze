using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSprite : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 500;

    void Update()
    {
        transform.Rotate(new Vector3(0, 0, -rotationSpeed) * Time.deltaTime);
    }
}
