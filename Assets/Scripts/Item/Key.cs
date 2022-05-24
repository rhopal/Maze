using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Key : MonoBehaviour
{
    private PlayerCtrl player;
    public Image[] image = new Image[3];
    public Sprite sprite;
    public int number; // 1번 열쇠는 number = 1, 2번 열쇠는 number = 2, 3번 열쇠는 number = 3

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerCtrl>();
        image[0] = GameObject.Find("Item_1").GetComponent<Image>();
        image[1] = GameObject.Find("Item_2").GetComponent<Image>();
        image[2] = GameObject.Find("Item_3").GetComponent<Image>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.name == "Player")
        {
            for (int i = 0; i < player.item.Length; i++)
            {
                if (player.item[i] == null)
                {
                    switch (number) // 열쇠의 경우에는 해당 열쇠의 number에 따라 아이템 배열에 다르게 추가
                    {
                        case 1:
                            player.item[i] = "Key_1";
                            player.haveKey_1 = true;
                            break;
                        case 2:
                            player.item[i] = "Key_2";
                            player.haveKey_2 = true;
                            break;
                        case 3:
                            player.item[i] = "Key_3";
                            player.haveKey_3 = true;
                            break;
                    }
                    switch (i)
                    {
                        case 0:
                            image[i].sprite = sprite;
                            image[i].enabled = true;
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
