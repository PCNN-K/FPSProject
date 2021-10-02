using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerManager : MonoBehaviour
{
    public List<Gun> _Guns;

    public bool isRun = false;
    public bool isGround = true;
    public bool isSit = false;

    private float camSensitivity = 2.4f;

    private float camRotationLimit = 45f;
    private float currentCamRotationX = 0f;

    private Rigidbody playerRigid;

    private GameObject MainPlayer;
    private Player player;

    private CapsuleCollider playerCollider;





    // Start is called before the first frame update
    void Start()
    {
        MainPlayer = GameObject.Find("MainPlayer");
        player = MainPlayer.GetComponent<Player>();
        playerCollider = MainPlayer.GetComponent<CapsuleCollider>();
        playerRigid = MainPlayer.GetComponent<Rigidbody>();

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
        TryMove();
        TryCamRotation();
        TryCharRotation();
    }

    public GameObject GetPlayer
    {
        get
        {
            return MainPlayer;
        }
    }

    private void TrySit()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            player.Sit();
        }
    }

    private void IsGround()
    {
        isGround = Physics.Raycast(MainPlayer.transform.position, Vector3.down, playerCollider.bounds.extents.y + 0.1f);
    }

    private void TryJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            player.Jump();
        }
    }

    private void TryRun()
    {
        if (isSit)
            return;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            player.Running();
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            player.RunningCancel();
        }
    }

    private void TryMove()
    {
        float _moveDirX = Input.GetAxisRaw("Horizontal");
        // -> : 1, <- : -1, none = 0
        // [Edit] -> [Project Settings] -> [Input Setting]
        float _moveDirZ = Input.GetAxisRaw("Vertical");
        // UP : 1, DOWN : -1, none = 0

        player.Move(_moveDirX, _moveDirZ);
    }

    private void TryCharRotation()
    {
        float _yRotation = Input.GetAxisRaw("Mouse X");
        Vector3 _characterRotationY = new Vector3(0f, _yRotation, 0f) * camSensitivity;
        player.CharacterRotation(_characterRotationY);
    }

    private void TryCamRotation()
    {
        float _xRotation = Input.GetAxisRaw("Mouse Y");
        float _camRotationX = _xRotation * camSensitivity;
        currentCamRotationX -= _camRotationX;
        currentCamRotationX = Mathf.Clamp(currentCamRotationX, -camRotationLimit, camRotationLimit);

        player.CamRotation(currentCamRotationX);
    }
}
