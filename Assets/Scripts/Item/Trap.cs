using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Trap : MonoBehaviour
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
        if (collision.collider.gameObject.name == "Player")
        {
            for (int i = 0; i < player.item.Length; i++)
            {
                if (player.item[i] == null)
                {
                    player.item[i] = "Trap";
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
