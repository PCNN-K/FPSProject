using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Ammo : MonoBehaviour
{
    public float speed;
    public float damage;
    private Vector3 direction;

    [SerializeField]
    private Gun gun;

    private void Start()
    {
        // gun = GameObject.Find("MainPlayer").transform.Find("AK-47").gameObject.GetComponent<Gun>();
        gun = FindObjectOfType<Gun>();
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
        transform.Translate(direction);
        Invoke("DestroyAmmo", 5f);
    }

    public void DestroyAmmo()
    {
        gun.ReturnObject(this);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Destroy(other.gameObject);
            DestroyAmmo();
        }   
    }
}