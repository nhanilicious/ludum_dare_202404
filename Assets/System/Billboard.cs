using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    [SerializeField] private Transform _camera;

    private void Start()
    {
        if (_camera == null)
        {
            _camera = Camera.main.transform;
        }
    }

    void LateUpdate()
    {
        transform.LookAt(transform.position + _camera.forward);
    }
}
