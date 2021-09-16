using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    private GameObject poolingAmmoPrefab;
    

    //ammo 의 오브젝트 풀
    //힌트 : 총알 담든 자료구조는 Queue로 구현!

    public static Queue<Ammo> AmmoPool = new Queue<Ammo>();
    private Camera mainCamera;
    

    private void CreateAmmo()
    {
        
    }

    private void Awake()
    {
        //초기화 로직
        
    }

    private void Start()
    {
        mainCamera = FindObjectOfType<Camera>();
        poolingAmmoPrefab = Resources.Load<GameObject>("Prefabs/Ammo/556x45");
        Initialize(30);
    }

    public void Fire()
    {
        
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            RaycastHit shootResult;
            if (Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out shootResult))
            {
                var ammo = GetObject();
                var direction = new Vector3(shootResult.point.x, shootResult.point.y, shootResult.point.z);
                ammo.transform.position = direction.normalized;
                ammo.Shoot(direction.normalized);
            }
        }
    }

    public void DestroyAmmo(Ammo _ammo)
    {
        ReturnObject(_ammo);
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