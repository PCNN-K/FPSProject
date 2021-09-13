using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private GameObject _poolingAmmoPrefab;

    //ammo의 오브젝트 풀
    //Hint : 총알 담는 자료구조는 Queue로 구현!

    Queue<Ammo> poolingObjectQueue = new Queue<Ammo>();

    public void Fire()
    {
        
    }

    public void CreateAmmo()
    {

    }

    private void Shot()
    {

    }

    private void Awake()
    {
        // 오브젝트 풀 초기화 로직

    }

    
    
}
