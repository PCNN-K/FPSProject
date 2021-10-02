using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField]
    private GameObject poolingAmmoPrefab;
    private ParticleSystem Flash;
    private ParticleSystem FlashParticle;
    private AudioClip fireSound;
    private AudioClip fireSoundClip;
    private AudioSource firesoundsource;

    public static Queue<Ammo> AmmoPool = new Queue<Ammo>();
    private Camera mainCamera;

    public float fireRate; // 연사 속도
    public float range; // 사정거리(미구현 상태)
    public float reloadTime; // 재장전 속도
    public float currentFireRate;

    public float retroActionForce; // 반동 세기 (미구현 상태)
    public float retroActionFineSightForce; // 정조준 시 반동 세기

    public int carryAmmo; // 현재 들고 있는 총알의 개수
    public int maxAmmo; // 최대로 소유할 수 있는 총알의 개수
    public int magazineAmmo; // 한 탄알집에 들어가는 총알의 개수
    public int currentMagazineAmmo; // 현재 탄알집에 들어가 있는 총알의 개수

    public Vector3 fineSightOriginPosition; // 정조준시 총의 위치

    private void Awake()
    {
        magazineAmmo = 30;
        carryAmmo = magazineAmmo * 5;
        currentMagazineAmmo = magazineAmmo;
        maxAmmo = 200;
        //초기화 로직
        Initialize(30);
    }

    private void Start()
    {
        reloadTime = 2.0f;
        fireRate = 0.3f;
        mainCamera = FindObjectOfType<Camera>();
        Flash = Resources.Load<ParticleSystem>("Prefabs/Particles/Flash");
        FlashParticle = Instantiate(Flash, transform.Find("BulletPos").position, transform.Find("BulletPos").rotation);
        //fireSound = Resources.Load<AudioClip>("Audios/fire_sound");
        //fireSoundClip = Instantiate(fireSound, transform);

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
        if(currentFireRate <= 0)
        {
            currentFireRate = fireRate;
            var ammo = GetObject();
            ammo.transform.position = transform.Find("BulletPos").position;
            ammo.transform.rotation = transform.Find("BulletPos").rotation;

            FlashParticle.Play();
            //PlaySE(fireSoundClip);
            ammo.Shoot(_target);
        }
        
    }

    private void FireRateCalc()
    {
        if(currentFireRate > 0)
        {
            currentFireRate -= Time.deltaTime;
        }
    }

    private void Update()
    {
        FireRateCalc();
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

    private void PlaySE(AudioClip _clip)
    {
        firesoundsource.clip = _clip;
        firesoundsource.Play();
    }
}