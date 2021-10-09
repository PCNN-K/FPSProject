using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class UIManager : MonoBehaviour
{
    private Player player;
    [SerializeField]
    private PlayerManager manager;
    private Gun gun;

    public RawImage statusImage;
    public Texture walkStatus;
    public Texture crouchStatus;
    public Texture sprintStatus;

    public Slider hpSlider;
    public Slider ammoSlider;

    private void Start()
    {
        player = manager.GetPlayer.GetComponent<Player>();
        gun = player.GetCurrentGun;
        panelImage = panelObject.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateImage();
        UpdateSlider();

    }

    private void UpdateSlider()
    {
        hpSlider.value = Mathf.Lerp(hpSlider.value, player.GetHP / player.GetMaxHP, Time.deltaTime * 10);
        ammoSlider.value = Mathf.Lerp(ammoSlider.value, gun.GetCurrentMagazineAmmo / gun.GetMagazineAmmo, Time.deltaTime * 10);
    }

    private void UpdateImage()
    {
        if(manager.isRun == true)
        {
            sprintStatus = Resources.Load<Texture>("Images/UI/Stance_Sprint_Icon");
            statusImage.texture = sprintStatus;
        }
        else if(manager.isSit == true)
        {
            crouchStatus = Resources.Load<Texture>("Images/UI/Stance_Crouch_Icon");
            statusImage.texture = crouchStatus;
        }
        else
        {
            walkStatus = Resources.Load<Texture>("Images/UI/Stance_Stand_Icon");
            statusImage.texture = walkStatus;
        }
    }
}
