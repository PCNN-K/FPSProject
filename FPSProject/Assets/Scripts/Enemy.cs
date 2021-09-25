using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : BaseCharacter
{
    private GameObject explosion = null;

    private enum Status
    {
        idle,
        attack,
    }

    private Status currentStatus = Status.idle;

    private float attackDist = 15.0f;

    private Transform playerTransform;
    private Transform _transform;

    private void Start()
    {
        hp = 100.0f;
        explosion = Resources.Load<GameObject>("Prefabs/Explosion/Explosions");
        myGun = Resources.Load<GameObject>("Prefabs/Guns/EnemyTurretGun").GetComponent<Gun>();
        if(myGun == null)
        {
            Debug.LogError("No Available Gun");
        }
        myGun.gameObject.SetActive(false);
        myGun.transform.SetParent(null);

        _transform = this.gameObject.GetComponent<Transform>();
        playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();


    }

    private void Update()
    {
        IsDead();
        if(currentStatus == Status.attack)
        {
            Shoot(playerTransform.position);
        }
    }

    IEnumerator CheckState()
    {
        while(hp > 0)
        {
            yield return new WaitForSeconds(0.2f);

            float dist = Vector3.Distance(playerTransform.position, _transform.position);

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
