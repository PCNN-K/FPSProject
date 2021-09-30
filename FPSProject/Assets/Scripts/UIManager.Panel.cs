using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class UIManager : MonoBehaviour
{
    public GameObject panelObject;
    private Image panelImage;

    private bool checkbool;

    IEnumerator FadeOut()
    {
        Color color = panelImage.color;

        for (int i = 100; i >= 0; i--)
        {
            color.a += Time.deltaTime * 0.01f;

            panelImage.color = color;

            if(panelImage.color.a >= 1)
            {
                checkbool = true;
            }
        }
        yield return null;
    }

    IEnumerator FadeIn()
    {
        Color color = panelImage.color;

        for (int i = 100; i >= 0; i--)
        {
            color.a -= Time.deltaTime * 0.01f;

            panelImage.color = color;

            if(panelImage.color.a <= 0)
            {
                checkbool = true;
            }
        }
        yield return null;
    }
}
