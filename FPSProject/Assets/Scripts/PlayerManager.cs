using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerManager : MonoBehaviour
{
    public GameObject gunObject;
    public List<Gun> _Guns;

    private float walkSpeed = 3f;
    private float runSpeed = 6.8f;
    private float sitSpeed = 1.5f;
    private float applySpeed;

    private float jumpForce = 5.0f;
    private float sitPosY = 0f;
    private float originPosY;
    private float applySitPosY;

    private bool isRun = false;
    private bool isGround = true;
    private bool isSit = false;

    private float camSensitivity = 2.4f;

    private float camRotationLimit = 45f;
    private float currentCamRotationX = 0f;


    public Camera theCamera;
    private Rigidbody playerRigid;

    private GameObject MainPlayer;

    private CapsuleCollider playerCollider;





    // Start is called before the first frame update
    void Start()
    {
        MainPlayer = GameObject.Find("MainPlayer");
        gunObject = MainPlayer.transform.GetChild(1).gameObject;
        playerCollider = MainPlayer.GetComponent<CapsuleCollider>();
        theCamera = FindObjectOfType<Camera>();
        playerRigid = MainPlayer.GetComponent<Rigidbody>();
        applySpeed = walkSpeed;
        originPosY = theCamera.transform.localPosition.y;
        applySitPosY = originPosY;

        // 리스트 초기화 작업
        //_Guns.Add((Resources.Load<GameObject>("Prefabs/Guns/Ak-47")).GetComponent<Gun>());
        //_Guns.Add((Resources.Load<GameObject>("Prefabs/Guns/M4A1 Sopmod")).GetComponent<Gun>());
        //_Guns.Add((Resources.Load<GameObject>("Prefabs/Guns/Skorpion VZ")).GetComponent<Gun>());
        //_Guns.Add((Resources.Load<GameObject>("Prefabs/Guns/UMP-45")).GetComponent<Gun>());
        
    }

    // Update is called once per frame
    void Update()
    {
        IsGround();
        TryJump();
        TryRun();
        TrySit();
        Move();
        CamRotation();
        CharacterRotation();
    }

    private void TrySit()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Sit();
        }
    }

    private void Sit()
    {
        isSit = !isSit;

        if (isSit)
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
        float _gunPosY = gunObject.transform.localPosition.y;
        int count = 0;

        while (_posY != applySitPosY)
        {
            count++;
            _posY = Mathf.Lerp(_posY, applySitPosY, 0.3f);
            _gunPosY = Mathf.Lerp(_gunPosY, applySitPosY - 0.5f, 0.3f);
            theCamera.transform.localPosition = new Vector3(0, _posY, 0);
            gunObject.transform.localPosition = new Vector3(gunObject.transform.localPosition.x, _gunPosY, gunObject.transform.localPosition.z);
            if (count > 15)
                break;
            yield return null;
        }

        theCamera.transform.localPosition = new Vector3(0, applySitPosY, 0);
        gunObject.transform.localPosition = new Vector3(gunObject.transform.localPosition.x, applySitPosY - 0.5f, gunObject.transform.localPosition.z);
    }

    private void IsGround()
    {
        isGround = Physics.Raycast(MainPlayer.transform.position, Vector3.down, playerCollider.bounds.extents.y + 0.1f);
    }

    private void TryJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            Jump();
        }
    }

    private void Jump()
    {
        if (isSit)
        {
            Sit();
        }

        playerRigid.velocity = MainPlayer.transform.up * jumpForce;
        //(0, 1, 0)
    }

    private void TryRun()
    {
        if (isSit)
            return;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Running();
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            RunningCancel();
        }
    }

    private void Running()
    {
        isRun = true;
        applySpeed = runSpeed;
    }

    private void RunningCancel()
    {
        isRun = false;
        applySpeed = walkSpeed;
    }

    private void Move()
    {
        float _moveDirX = Input.GetAxisRaw("Horizontal");
        // -> : 1, <- : -1, none = 0
        // [Edit] -> [Project Settings] -> [Input Setting]
        float _moveDirZ = Input.GetAxisRaw("Vertical");
        // UP : 1, DOWN : -1, none = 0

        Vector3 _moveHorizontal = MainPlayer.transform.right * _moveDirX;
        Vector3 _moveVertical = MainPlayer.transform.forward * _moveDirZ;

        Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized * applySpeed;

        playerRigid.MovePosition(MainPlayer.transform.position + _velocity * Time.deltaTime);
    }

    private void CharacterRotation()
    {
        float _yRotation = Input.GetAxisRaw("Mouse X");
        Vector3 _characterRotationY = new Vector3(0f, _yRotation, 0f) * camSensitivity;
        playerRigid.MoveRotation(playerRigid.rotation * Quaternion.Euler(_characterRotationY));
    }

    private void CamRotation()
    {
        float _xRotation = Input.GetAxisRaw("Mouse Y");
        float _camRotationX = _xRotation * camSensitivity;
        currentCamRotationX -= _camRotationX;
        currentCamRotationX = Mathf.Clamp(currentCamRotationX, -camRotationLimit, camRotationLimit);

        theCamera.transform.localEulerAngles = new Vector3(currentCamRotationX, 0f, 0f);
        gunObject.transform.localEulerAngles = new Vector3(currentCamRotationX, 0f, 0f);
    }
}
