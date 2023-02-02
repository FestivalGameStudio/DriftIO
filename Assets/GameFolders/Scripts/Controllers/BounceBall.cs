using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceBall : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float baseFollowSpeed;
    [SerializeField] private float speedCoefficientForDistance;

    private float _followSpeed;

    private void LateUpdate()
    {
        float distance = Vector3.Distance(transform.position, target.position);
        _followSpeed = baseFollowSpeed + distance * speedCoefficientForDistance;
        transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * _followSpeed);
    }
}
