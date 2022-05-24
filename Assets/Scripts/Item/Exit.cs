using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Exit : MonoBehaviour
{
    private PlayerCtrl player;
    public Sprite[] sprite;
    private SpriteRenderer spriteRenderer;
    public bool getKey_1 = false; // 1번 열쇠가 끼워졌는지 확인하는 변수
    public bool getKey_2 = false; // 2번 열쇠
    public bool getKey_3 = false; // 3번 열쇠
    public bool getAllKey = false; // 모든 열쇠
    public float rotSpeed = 0; // 모든 열쇠가 끼워졌을 때 오브젝트의 회전 속도를 제어하는 변수

    void Start()
    {
        var temp = GameObject.Find("Player");
        if (temp != null)
            player = temp.GetComponent<PlayerCtrl>();

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite[0];

        StartCoroutine(CheckKey());
    }

    void Update()
    {
        if (getAllKey == true) // 모든 열쇠가 끼워지면
        {
            rotSpeed += 2.0f;
            transform.Rotate(0, 0, rotSpeed * Time.deltaTime); // 회전 속도를 점점 증가시키고

            if (rotSpeed >= 2000.0f) // 회전 속도가 2000f 이상이면 2000f으로 고정
            {
                rotSpeed = 2000.0f;
            }
        }
    }

    IEnumerator CheckKey()
    {
        while (true)
        {
            if (getKey_1 == true && getKey_2 == false && getKey_3 == false) // O X X
            {
                spriteRenderer.sprite = sprite[1];
            }
            else if (getKey_1 == false && getKey_2 == true && getKey_3 == false) // X O X
            {
                spriteRenderer.sprite = sprite[2];
            }
            else if (getKey_1 == false && getKey_2 == false && getKey_3 == true) // X X O
            {
                spriteRenderer.sprite = sprite[3];
            }
            else if (getKey_1 == true && getKey_2 == true && getKey_3 == false) // O O X
            {
                spriteRenderer.sprite = sprite[4];
            }
            else if (getKey_1 == true && getKey_2 == false && getKey_3 == true) // O X O
            {
                spriteRenderer.sprite = sprite[5];
            }
            else if (getKey_1 == false && getKey_2 == true && getKey_3 == true) // X O O
            {
                spriteRenderer.sprite = sprite[6];
            }
            else if (getKey_1 == true && getKey_2 == true && getKey_3 == true) // O O O
            {
                spriteRenderer.sprite = sprite[7];
            }
            else if (getKey_1 == false && getKey_2 == false && getKey_3 == false) // X X X
            {
                spriteRenderer.sprite = sprite[0];
            }

            if (getKey_1 == true && getKey_2 == true && getKey_3 == true)
            {
                getAllKey = true;
            }

            yield return new WaitForSeconds(0.2f);
        }
    }
}
