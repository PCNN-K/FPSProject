using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private GameObject poolingAmmoPrefab;
    

    //ammo 의 오브젝트 풀
    //힌트 : 총알 담든 자료구조는 Queue로 구현!

    public static Queue<Ammo> AmmoPool = new Queue<Ammo>();

    

    private void CreateAmmo()
    {
        // 초기화 로직에서 Ammo를 담은 풀을 만들었으니 필요가 없다고 생각합니다.
    }

    private void Awake()
    {
        //초기화 로직
        Initialize(30);
    }

    public void Fire()
    {
        
    }

    private void Initialize(int _initCount)
    {
        for (int i = 0; i < _initCount; i++)
        {
            AmmoPool.Enqueue(CreateObject());

        }
    }

    private Ammo CreateObject()
    {
        var newAmmoObj = Instantiate(poolingAmmoPrefab).GetComponent<Ammo>();
        newAmmoObj.gameObject.SetActive(false);
        newAmmoObj.transform.SetParent(transform);
        return newAmmoObj;
    }

    public Ammo GetObject()
    {
        if(AmmoPool.Count > 0)
        {
            var ammoObj = AmmoPool.Dequeue();
            ammoObj.transform.SetParent(null);
            ammoObj.gameObject.SetActive(true);
            return ammoObj;
        }
        else
        {
            var newAmmoObj = CreateObject();
            newAmmoObj.gameObject.SetActive(true);
            newAmmoObj.transform.SetParent(null);
            return newAmmoObj;
        }
    }

    public void ReturnObject(Ammo _obj)
    {
        _obj.gameObject.SetActive(false);
        _obj.transform.SetParent(transform);
        AmmoPool.Enqueue(_obj);
    }

}