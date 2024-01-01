using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotater : MonoBehaviour
{
    [SerializeField] private Vector3 _rotation;

    private void Update()
    {
        transform.Rotate(_rotation * Time.deltaTime);
    }
}
