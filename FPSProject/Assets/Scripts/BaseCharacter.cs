using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacter : MonoBehaviour
{
    [SerializeField] protected float hp;

    protected Gun myGun;
    protected Vector3 direction;

    protected virtual void IsDead()
    {
        if(hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    protected void OnCollisionEnter(Collision _other)
    {
        if(_other.gameObject.tag == "Bullet")
        {
            hp -= Ammo.damage;
        }
    }

    // 향하는 방향으로 총을 격발한다.
    protected virtual void Shoot(Vector3 _target)
    {
        direction = new Vector3(_target.x, _target.y, _target.z) - transform.position;
        myGun.Fire(direction);
    }
}
