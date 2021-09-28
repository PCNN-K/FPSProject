using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField]
    private GameObject poolingAmmoPrefab;

    public static Queue<Ammo> AmmoPool = new Queue<Ammo>();
    private Camera mainCamera;

    private int currentAmmo;
    private int maxAmmo;
    private int magazineAmmo;
    private int currentMagazineAmmo;

    private void Awake()
    {
        magazineAmmo = 30;
        currentAmmo = magazineAmmo;
        currentMagazineAmmo = magazineAmmo;
        maxAmmo = 200;
        //초기화 로직
        Initialize(30);
    }

    private void Start()
    {
        mainCamera = FindObjectOfType<Camera>();
    }

    public int GetCurrentMagazineAmmo
    {
        get
        {
            return currentMagazineAmmo;
        }
    }

    public int GetMagazineAmmo
    {
        get
        {
            return magazineAmmo;
        }
    }

    public void Fire(Vector3 _target)
    {
        var ammo = GetObject();
        ammo.transform.position = transform.position + _target;
        ammo.Shoot(_target.normalized);
    }

    private void Update()
    {
    }

    // 오브젝트 풀에 새로운 오브젝트를 일정 개수만큼 생성해서 Enqueue
    private void Initialize(int _initCount)
    {
        for (int i = 0; i < _initCount; i++)
        {
            AmmoPool.Enqueue(CreateObject());
        }
    }

    // 초기화 과정이나 오브젝트 풀 내에 오브젝트가 다 떨어졌을 경우 새로 만들어서 반환
    private Ammo CreateObject()
    {
        var newAmmoObj = Instantiate(poolingAmmoPrefab).GetComponent<Ammo>();
        newAmmoObj.gameObject.SetActive(false);
        newAmmoObj.transform.SetParent(transform);
        newAmmoObj.OnDestroyCallback = () => ReturnObject(newAmmoObj);
        return newAmmoObj;
    }

    // 오브젝트 풀에서 오브젝트를 받아온다.
    public Ammo GetObject()
    {
        // 풀 내에 존재할 시 Dequeue해서 받아온다.
        if(AmmoPool.Count > 0)
        {
            var ammoObj = AmmoPool.Dequeue();
            ammoObj.transform.SetParent(null);
            ammoObj.gameObject.SetActive(true);
            return ammoObj;
        }
        // 없을 시 CreateNewObject 해서 받아온다.
        else
        {
            var newAmmoObj = CreateObject();
            newAmmoObj.gameObject.SetActive(true);
            newAmmoObj.transform.SetParent(null);
            return newAmmoObj;
        }
    }

    // 오브젝트를 다시 오브젝트 풀에 Enqueue 한다.
    public void ReturnObject(Ammo _obj)
    {
        _obj.gameObject.SetActive(false);
        _obj.transform.SetParent(transform);
        AmmoPool.Enqueue(_obj);
    }

}