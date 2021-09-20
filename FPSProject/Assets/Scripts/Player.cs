using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float hp;
    [SerializeField] private float ammo;
    [SerializeField] private PlayerManager _manager;

    private Gun _myGun;

    private void Start()
    {
        _myGun = Instantiate(_manager.GetCurrentGun(),transform);
    }

    private void Equip()
    {
        
    }
}
