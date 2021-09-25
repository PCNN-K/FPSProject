using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : BaseCharacter
{
    [SerializeField] private PlayerManager _manager;

    RaycastHit hitResult;
    Vector3 target;

    private void Start()
    {
        hp = 100;
        myGun = Instantiate(_manager.GetCurrentGun(),transform);
    }

    private void Update()
    {
        // 마우스 좌클릭이 입력되면 해당 좌표를 받아서 방아쇠를 당긴다.
        if (Input.GetMouseButton(0))
        {
            if (Physics.Raycast(_manager.theCamera.ScreenPointToRay(Input.mousePosition), out hitResult))
            {
                target = new Vector3(hitResult.point.x, hitResult.point.y, hitResult.point.z);
                Shoot(target);
            }
        }
    }

    private void Equip()
    {
        
    }

    protected override void IsDead()
    {
        if(hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
