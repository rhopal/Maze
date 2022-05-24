using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectCtrl : MonoBehaviour
{
    public Image image;
    private EnemyCtrl enemy;
    public Exit exit;
    private float alpha;

    void Start()
    {
	enemy = GetComponent<EnemyCtrl>();
    }

    void Update()
    {
        if (exit.getAllKey == true) // 탈출구에 열쇠를 다 끼웠을 경우 이펙트 제거 후 정지
        {
            alpha = 0;
            image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
            return;
        }

        if (enemy.state != EnemyCtrl.State.PATROL) // 적이 플레이어를 바라보고 있지 않을 경우 이펙트 점점 연하게
        {
            alpha += 0.3f * Time.deltaTime;
            if (alpha >= 0.6f)
            {
                alpha = 0.6f;
            }
            image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
        }
        else // 적이 플레이어를 바라보고 있으면 이펙트 점점 진하게
        {
            alpha -= 0.3f * Time.deltaTime;
            if (alpha <= 0)
            {
                alpha = 0;
            }
            image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
        }
    }
}
