using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayerFollow : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private Transform _ghostTransform;
    [SerializeField] private float _followSpeed = 5f;

    private Transform _target;

    private void Awake()
    {
        _target = _playerTransform;
    }

    private void Start()
    {
        GameInput.Instance.OnSwitchAction += GameInput_OnSwitchAction;
    }

    private void GameInput_OnSwitchAction(object sender, System.EventArgs e)
    {
        if(_target == _playerTransform)
        {
            _target = _ghostTransform;
        }
        else
        {
            _target = _playerTransform;
        }
    }

    void Update()
    {
        Vector3 newpos = new Vector3(_target.position.x, _target.position.y, -10f);
        transform.position = Vector3.Slerp(transform.position, newpos, _followSpeed * Time.deltaTime);
    }
}
