using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : BaseCharacter
{
    private GameObject explosion = null;
    private GameObject gunObject = null;
    private GameObject instance = null;

    private enum Status
    {
        idle,
        attack,
    }

    private Status currentStatus = Status.idle;

    private float attackDist = 10.0f;

    private Transform playerTransform;
    private Transform _transform;

    Coroutine statusCoroutine = null;

    private void Start()
    {
        hp = 100.0f;
        explosion = Resources.Load<GameObject>("Prefabs/Explosion/Explosions");
        gunObject = Resources.Load<GameObject>("Prefabs/Guns/EnemyTurretGun");
        instance = Instantiate(gunObject, transform);
        myGun = instance.GetComponent<Gun>();
        
        instance.gameObject.SetActive(true);
        instance.transform.SetParent(transform);

        _transform = this.gameObject.GetComponent<Transform>();
        playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
        

        statusCoroutine = StartCoroutine("CheckState");
    }

    private void Update()
    {
        IsDead();
        if(currentStatus == Status.attack)
        {
            Shoot((_transform.position - playerTransform.position).normalized);
        }
    }

    IEnumerator CheckState()
    {
        while(hp > 0)
        {
            yield return new WaitForSeconds(0.2f);

            float dist = Vector3.Distance(_transform.position, playerTransform.position);

            if(dist <= attackDist)
            {
                currentStatus = Status.attack;
            }
            else
            {
                currentStatus = Status.idle;
            }
        }
    }

    protected override void IsDead()
    {
        if(hp <= 0)
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    
}
