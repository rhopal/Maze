using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinScene : MonoBehaviour
{
    public Image image;
    private float alpha = 1;

    void Start()
    {
        image.enabled = true;
    }

    void Update()
    {
        alpha -= 0.1f * Time.deltaTime; // image를 점점 연하게 함
        if (alpha <= 0) // image가 연해져서 불투명도가 0이 되면
        {
            image.enabled = false; // image 비활성화
        }
        image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
    }
}
