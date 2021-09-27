using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : BaseCharacter
{
    [SerializeField] private PlayerManager _manager;

    RaycastHit hitResult;
    Vector3 target;

    public Camera theCamera;
    private Rigidbody playerRigid;

    private float walkSpeed = 3f;
    private float runSpeed = 6.8f;
    private float sitSpeed = 1.5f;
    private float applySpeed;

    private float jumpForce = 5.0f;
    private float sitPosY = 0f;
    private float originPosY;
    private float applySitPosY;

    private void Start()
    {
        hp = 100;
        maxHp = 100;
        myGun = Instantiate(_manager.GetCurrentGun(), transform);
        theCamera = FindObjectOfType<Camera>();
        playerRigid = GetComponent<Rigidbody>();
        applySpeed = walkSpeed;
        originPosY = theCamera.transform.localPosition.y;
        applySitPosY = originPosY;
    }

    private void Update()
    {
        // 마우스 좌클릭이 입력되면 해당 좌표를 받아서 방아쇠를 당긴다.
        if (Input.GetMouseButton(0))
        {
            Shoot(theCamera.transform.forward);
        }
    }

    public void Sit()
    {
        _manager.isSit = !_manager.isSit;

        if (_manager.isSit)
        {
            applySpeed = sitSpeed;
            applySitPosY = sitPosY;
        }
        else
        {
            applySpeed = walkSpeed;
            applySitPosY = originPosY;
        }

        StartCoroutine(SitCoroutine());
    }

    IEnumerator SitCoroutine()
    {
        float _posY = theCamera.transform.localPosition.y;
        float _gunPosY = myGun.gameObject.transform.localPosition.y;
        int count = 0;

        while (_posY != applySitPosY)
        {
            count++;
            _posY = Mathf.Lerp(_posY, applySitPosY, 0.3f);
            _gunPosY = Mathf.Lerp(_gunPosY, applySitPosY - 0.5f, 0.3f);
            theCamera.transform.localPosition = new Vector3(0, _posY, 0);
            myGun.gameObject.transform.localPosition = new Vector3(myGun.gameObject.transform.localPosition.x, _gunPosY, myGun.gameObject.transform.localPosition.z);
            if (count > 15)
                break;
            yield return null;
        }

        theCamera.transform.localPosition = new Vector3(0, applySitPosY, 0);
        myGun.gameObject.transform.localPosition = new Vector3(myGun.gameObject.transform.localPosition.x, applySitPosY - 0.5f, myGun.gameObject.transform.localPosition.z);
    }

    public void Jump()
    {
        if (_manager.isSit)
        {
            Sit();
        }

        playerRigid.velocity = transform.up * jumpForce;
        //(0, 1, 0)
    }

    public void Running()
    {
        _manager.isRun = true;
        applySpeed = runSpeed;
    }

    public void RunningCancel()
    {
        _manager.isRun = false;
        applySpeed = walkSpeed;
    }

    public void Move(float _dirX, float _dirZ)
    {
        Vector3 _moveHorizontal = transform.right * _dirX;
        Vector3 _moveVertical = transform.forward * _dirZ;

        Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized * applySpeed;

        playerRigid.MovePosition(transform.position + _velocity * Time.deltaTime);
    }
    public void CharacterRotation(Vector3 _yRotation)
    {

        playerRigid.MoveRotation(playerRigid.rotation * Quaternion.Euler(_yRotation));
    }

    public void CamRotation(float _CamRotX)
    {
        theCamera.transform.localEulerAngles = new Vector3(_CamRotX, 0f, 0f);
        myGun.gameObject.transform.localEulerAngles = new Vector3(_CamRotX, 0f, 0f);
    }

    public float GetHP
    {
        get {
            return hp;
        }
    }

    public float GetMaxHP
    {
        get
        {
            return maxHp;
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
