using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // 값을 Inspector 창에서 확인하고 쉽게 수정하기 위해서 다음과 같이 설정
    private float walkSpeed = 3f;

    private float camSensitivity = 2.4f;

    private float camRotationLimit = 45f;
    private float currentCamRotationX = 0f;


    private Camera theCamera;
    private Rigidbody playerRigid;

    // Start is called before the first frame update
    void Start()
    {
        // 게임 오브젝트 중에서 Camera 타입 오브젝트를 찾아서 theCamera에 할당.
        theCamera = FindObjectOfType<Camera>();
        // rigidBody 컴포넌트를 받아와 playerRigid에 할당한다.
        playerRigid = GetComponent<Rigidbody>();
    }



    // Update is called once per frame
    void Update()
    {
        Move();
        CamRotation();
        CharacterRotation();
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

        Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized * walkSpeed;
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
