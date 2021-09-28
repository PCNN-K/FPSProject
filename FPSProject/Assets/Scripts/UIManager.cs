using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text hpDisplayer;
    public Text ammoDisplayer;
    [SerializeField]
    private Player player;
    private Gun gun;

    public Slider hpSlider;
    public Slider ammoSlider;

    private void Start()
    {
        gun = player.gameObject.transform.Find("Ak-47(clone)").gameObject.GetComponent<Gun>();
    }

    // Update is called once per frame
    void Update()
    {
        hpDisplayer.text = "HP : " + player.GetHP;
        ammoDisplayer.text = "Ammo : " + gun.GetCurrentMagazineAmmo + " / " + gun.GetMagazineAmmo;
        UpdateSlider();
    }

    private void UpdateSlider()
    {
        hpSlider.value = Mathf.Lerp(hpSlider.value, player.GetHP / player.GetMaxHP, Time.deltaTime * 10);
        ammoSlider.value = Mathf.Lerp(ammoSlider.value, gun.GetCurrentMagazineAmmo / gun.GetMagazineAmmo, Time.deltaTime * 10);
    }
}
