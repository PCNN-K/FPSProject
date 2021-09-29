using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



public class Ammo : MonoBehaviour
{
    public float speed;
    public float damage;
    private float timer;
    private float waitingTime;
    private Vector3 direction;

    public Action OnDestroyCallback;

    private void Start()
    {
        timer = 0.0f;
        waitingTime = 3.0f;
        damage = 10.0f;
    }

    public float GetDamage
    {
        get
        {
            return damage;
        }
    }

    public void Shoot(Vector3 _direction)
    {
        _direction.x *= speed;
        _direction.y *= speed;
        _direction.z *= -speed;
        this.direction = _direction;

    }

    private void Update()
    {
        timer += Time.deltaTime;
        if(timer > waitingTime)
        {
            OnDestroyCallback.Invoke();
            timer = 0;
        }
        transform.Translate(direction);
    }


    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            OnDestroyCallback.Invoke();
        }
        else
        {
            OnDestroyCallback.Invoke();
        }
    }
}