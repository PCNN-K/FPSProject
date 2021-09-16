using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public GameObject gunObject;

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

    private GameObject MainPlayer;

    private CapsuleCollider playerCollider;





    // Start is called before the first frame update
    void Start()
    {
        MainPlayer = GameObject.Find("MainPlayer");
        gunObject = MainPlayer.transform.GetChild(1).gameObject;
        playerCollider = MainPlayer.GetComponent<CapsuleCollider>();
        // ���� ������Ʈ �߿��� Camera Ÿ�� ������Ʈ�� ã�Ƽ� theCamera�� �Ҵ�.
        theCamera = FindObjectOfType<Camera>();
        // rigidBody ������Ʈ�� �޾ƿ� playerRigid�� �Ҵ��Ѵ�.
        playerRigid = MainPlayer.GetComponent<Rigidbody>();
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

    // Ctrl�� ������ �� �ɱ� ����
    private void TrySit()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Sit();
        }
    }

    // �ӵ��� �ɾ��� �� �ӵ���, ��ġ�� �ɾ��� �� ��ġ�� ����
    // �ٽ� Ctrl �Է��� ������ �ȴ� �ӵ�, ���� ��ġ�� �ǵ�����.
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

    // ������ �̿��ؼ� �ε巴�� �ɴ� ������ ���� �� �ְ� �Ѵ�.
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
        // �밢��, ��� ���� ���� ������ �� �ֵ��� ���ݿ� 0.1f�� ���� ���̸� �������� ���.
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
        // ���� ���¿��� ������ �õ��ϸ� �ڵ����� �ٽ� ���鿡 ����� �� �׳� ���ֵ��� �Ѵ�.
        if (isSit)
        {
            Sit();
        }

        playerRigid.velocity = MainPlayer.transform.up * jumpForce;
        //(0, 1, 0)
        // �������� ����.
    }

    private void TryRun()
    {
        // ���� ���¿��� �޸��⸦ �õ��ϸ� �����Ѵ�.
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
        // [Edit] -> [Project Settings] -> [Input Setting]���� ã�ƺ� �� �ִ�.
        float _moveDirZ = Input.GetAxisRaw("Vertical");
        // UP : 1, DOWN : -1, none = 0
        // GetAxis�� ���� �� �ε巯�� ������ ���� ������
        // �Է���� ������ �;� �ϹǷ� GetAxisRaw �Լ��� ����ߴ�.

        Vector3 _moveHorizontal = MainPlayer.transform.right * _moveDirX;
        Vector3 _moveVertical = MainPlayer.transform.forward * _moveDirZ;

        Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized * applySpeed;
        // �����̴� �Ÿ��� 1�� �����ϱ� ���ؼ� normalized�� ���.

        playerRigid.MovePosition(MainPlayer.transform.position + _velocity * Time.deltaTime);
        // deltaTime�� �̿��ؼ� ������ �ʴ� ������ ����
    }

    private void CharacterRotation()
    {
        float _yRotation = Input.GetAxisRaw("Mouse X");
        Vector3 _characterRotationY = new Vector3(0f, _yRotation, 0f) * camSensitivity;
        playerRigid.MoveRotation(playerRigid.rotation * Quaternion.Euler(_characterRotationY));
        // Euler ���� �κ��� ���� Study �ʿ�.
    }

    private void CamRotation()
    {
        float _xRotation = Input.GetAxisRaw("Mouse Y");
        float _camRotationX = _xRotation * camSensitivity;
        currentCamRotationX -= _camRotationX;
        currentCamRotationX = Mathf.Clamp(currentCamRotationX, -camRotationLimit, camRotationLimit);
        // �ʹ� ���ڱ� ȮȮ ���ư��� �ʵ��� �Ӱ�ġ ����

        theCamera.transform.localEulerAngles = new Vector3(currentCamRotationX, 0f, 0f);

        // ���������� Euler �κ��� Study �ʿ�.
    }
}
