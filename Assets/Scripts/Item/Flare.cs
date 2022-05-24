using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flare : MonoBehaviour
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
                    player.item[i] = "Flare";
                    switch (i)
                    {
                        case 0:
                            image[i].sprite = sprite;
                            image[i].enabled = true;
                            image[i].type = Image.Type.Filled; // Flare 사용 시 지속 시간을 직관적으로 알려주기 위해
                            image[i].fillMethod = Image.FillMethod.Radial360; // 시계 방향으로 아이템 아이콘이 사라지게끔
                            image[i].fillOrigin = (int)Image.Origin360.Top; // 해당 슬롯의 image 속성값을 변경함
                            image[i].fillClockwise = false;
                            break;
                        case 1:
                            image[i].sprite = sprite;
                            image[i].enabled = true;
                            image[i].type = Image.Type.Filled;
                            image[i].fillMethod = Image.FillMethod.Radial360;
                            image[i].fillOrigin = (int)Image.Origin360.Top;
                            image[i].fillClockwise = false;
                            break;
                        case 2:
                            image[i].sprite = sprite;
                            image[i].enabled = true;
                            image[i].type = Image.Type.Filled;
                            image[i].fillMethod = Image.FillMethod.Radial360;
                            image[i].fillOrigin = (int)Image.Origin360.Top;
                            image[i].fillClockwise = false;
                            break;
                    }
                    Destroy(gameObject);
                    break;
                }
            }
        }
    }
}
