using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayerFollow : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _followSpeed = 5f;

    void Update()
    {
        Vector3 newpos = new Vector3(_target.position.x, _target.position.y, -10f);
        transform.position = Vector3.Slerp(transform.position, newpos, _followSpeed * Time.deltaTime);
    }
}
