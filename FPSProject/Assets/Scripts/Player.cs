using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float hp;
<<<<<<< HEAD:FPSProject/Assets/Scripts/PlayerController.cs

=======
    [SerializeField] private float ammo;
    [SerializeField] public Gun gunObject;
    
    
>>>>>>> c6b91612faff995566dbc1aeee2d7433bf6efc56:FPSProject/Assets/Scripts/Player.cs
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


    private Camera theCamera;
    private Rigidbody playerRigid;

    private CapsuleCollider playerCollider;

<<<<<<< HEAD:FPSProject/Assets/Scripts/PlayerController.cs
    [SerializeField]
    private GameObject gunObject;
=======
    
    
    
>>>>>>> c6b91612faff995566dbc1aeee2d7433bf6efc56:FPSProject/Assets/Scripts/Player.cs

    // Start is called before the first frame update
    void Start()
    {
        playerCollider = GetComponent<CapsuleCollider>();
        // 게임 오브젝트 중에서 Camera 타입 오브젝트를 찾아서 theCamera에 할당.
        theCamera = FindObjectOfType<Camera>();
        // rigidBody 컴포넌트를 받아와 playerRigid에 할당한다.
        playerRigid = GetComponent<Rigidbody>();
        applySpeed = walkSpeed;
        originPosY = theCamera.transform.localPosition.y;
        applySitPosY = originPosY;
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

    // Ctrl을 눌렀을 시 앉기 수행
    private void TrySit()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Sit();
        }
    }

    // 속도를 앉았을 때 속도로, 위치를 앉았을 때 위치로 변경
    // 다시 Ctrl 입력이 들어오면 걷는 속도, 원래 위치로 되돌린다.
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

    // 보간을 이용해서 부드럽게 앉는 과정을 보일 수 있게 한다.
    IEnumerator SitCoroutine()
    {
        float _posY = theCamera.transform.localPosition.y;
        float _gunPosY = gunObject.transform.localPosition.y;
        int count = 0;

        while(_posY != applySitPosY)
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
        isGround = Physics.Raycast(transform.position, Vector3.down, playerCollider.bounds.extents.y + 0.1f);
        // 대각선, 계단 같은 곳을 무시할 수 있도록 절반에 0.1f를 더한 길이를 레이저로 쏜다.
    }

    private void TryJump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            Jump();
        }
    }

    private void Jump()
    {
        // 앉은 상태에서 점프를 시도하면 자동으로 다시 지면에 닿았을 때 그냥 서있도록 한다.
        if (isSit)
        {
            Sit();
        }

        playerRigid.velocity = transform.up * jumpForce;
                                        //(0, 1, 0)
                                        // 공중으로 띄운다.
    }

    private void TryRun()
    {
        // 앉은 상태에서 달리기를 시도하면 무시한다.
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
        // [Edit] -> [Project Settings] -> [Input Setting]에서 찾아볼 수 있다.
        float _moveDirZ = Input.GetAxisRaw("Vertical");
        // UP : 1, DOWN : -1, none = 0
        // GetAxis를 쓰면 더 부드러운 움직이 구현 되지만
        // 입력즉시 반응이 와야 하므로 GetAxisRaw 함수를 사용했다.

        Vector3 _moveHorizontal = transform.right * _moveDirX;
        Vector3 _moveVertical = transform.forward * _moveDirZ;

        Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized * applySpeed;
        // 움직이는 거리를 1로 조정하기 위해서 normalized를 사용.

        playerRigid.MovePosition(transform.position + _velocity * Time.deltaTime);
        // deltaTime을 이용해서 끊기지 않는 움직임 구현
    }

    private void CharacterRotation()
    {
        float _yRotation = Input.GetAxisRaw("Mouse X");
        Vector3 _characterRotationY = new Vector3(0f, _yRotation, 0f) * camSensitivity;
        playerRigid.MoveRotation(playerRigid.rotation * Quaternion.Euler(_characterRotationY));
        // Euler 관련 부분은 따로 Study 필요.
    }

    private void CamRotation()
    {
        float _xRotation = Input.GetAxisRaw("Mouse Y");
        float _camRotationX = _xRotation * camSensitivity;
        currentCamRotationX -= _camRotationX;
        currentCamRotationX = Mathf.Clamp(currentCamRotationX, -camRotationLimit, camRotationLimit);
        // 너무 갑자기 확확 돌아가지 않도록 임계치 설정

        theCamera.transform.localEulerAngles = new Vector3(currentCamRotationX, 0f, 0f);
        
        // 마찬가지로 Euler 부분은 Study 필요.
    }
}
