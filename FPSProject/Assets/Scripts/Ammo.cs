using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Ammo : MonoBehaviour
{
    public float speed;
    public float damage;
    private Vector3 direction;

    private Gun gun;

    private void Start()
    {
        gun = FindObjectOfType<Gun>();
    }

    public void Shoot(Vector3 _direction)
    {
        _direction.x *= speed;
        _direction.y *= speed;
        _direction.z *= speed;
        this.direction = _direction;

    }

    private void Update()
    {
        transform.Translate(direction);
        Invoke("DestroyAmmo", 1f);
    }

    public void DestroyAmmo()
    {
        gun.ReturnObject(this);
    }

    private void OnCollisionEnter(Collision other)
    {
            
    }
}