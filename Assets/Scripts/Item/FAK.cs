using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FAK : MonoBehaviour
{
    private PlayerCtrl player;
    public Image[] image = new Image[3];
    public Sprite sprite;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerCtrl>();
        image[0] = GameObject.Find("Item_1").GetComponent<Image>();
        image[1] = GameObject.Find("Item_2").GetComponent<Image>();
        image[2] = GameObject.Find("Item_3").GetComponent<Image>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.name == "Player") // 플레이어와 충돌했을 때
        {
            for (int i = 0; i < player.item.Length; i++) // 플레이어의 아이템 슬롯 중
            {
                if (player.item[i] == null) // 비어있는 슬롯에
                {
                    player.item[i] = "FAK"; // 아이템 추가
                    switch (i)
                    {
                        case 0:
                            image[i].sprite = sprite; // 슬롯에 아이템 이미지를 넣은 다음
                            image[i].enabled = true; // 그 슬롯의 image를 활성화함
                            break;
                        case 1:
                            image[i].sprite = sprite;
                            image[i].enabled = true;
                            break;
                        case 2:
                            image[i].sprite = sprite;
                            image[i].enabled = true;
                            break;
                    }
                    Destroy(gameObject);
                    break;
                }
            }
        }
    }
}
