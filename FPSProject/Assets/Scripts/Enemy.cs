using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    public float hp;
    private GameObject explosion = null;

    private enum Status
    {
        idle,
        attack,
    }

    private Status currentStatus = Status.idle;

    private float attackDist = 15.0f;

    private Gun gun;

    private Transform playerTransform;
    private Transform _transform;

    private void Start()
    {
        hp = 100.0f;
        explosion = Resources.Load<GameObject>("Prefabs/Explosion/Explosions");
        gun = Resources.Load<GameObject>("Prefabs/Guns/EnemyTurretGun").GetComponent<Gun>();
        gun.gameObject.SetActive(true);
        gun.transform.SetParent(transform);

        _transform = this.gameObject.GetComponent<Transform>();
        playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();

    }

    private void Update()
    {
        IsDead();
    }



    private void IsDead()
    {
        if(hp <= 0)
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            Destroy(other.gameObject);
            hp -= Ammo.damage;
        }
    }
}
