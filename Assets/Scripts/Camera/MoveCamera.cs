using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField] Transform _cameraPos;

    private void Update()
    {
        transform.position = _cameraPos.position;
    }
}
