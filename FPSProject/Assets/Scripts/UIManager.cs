using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text hpDisplayer;
    [SerializeField]
    private Player player;

    public Slider hpSlider;

    // Update is called once per frame
    void Update()
    {
        hpDisplayer.text = "HP : " + player.GetHP;
    }

    private void UpdateHpslider()
    {
        hpSlider.value = Mathf.Lerp(hpSlider.value, player.GetHP / player.GetMaxHP, Time.deltaTime * 10);
    }
}
