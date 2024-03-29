﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySoundCtrl : MonoBehaviour
{
    private AudioSource audio;
    private EnemyCtrl enemy;
    public Exit exit;

    void Start()
    {
        audio = GetComponent<AudioSource>();
        enemy = GameObject.Find("Enemy").GetComponent<EnemyCtrl>();
    }

    void Update()
    {
        if (exit.getAllKey == true) // 탈출구에 열쇠를 다 끼우면 audio 정지
        {
            audio.Stop();
            return;
        }

        if (enemy.state != EnemyCtrl.State.PATROL) // 적이 플레이어를 바라보고 있으면 음량 증가
        {
            audio.volume += 0.1f * Time.deltaTime;
            if (audio.volume >= 0.6f)
            {
                audio.volume = 0.6f;
            }
        }
        else // 적이 플레이어를 바라보고 있지 않으면 음량 감소
        {
            audio.volume -= 0.1f * Time.deltaTime;
            if (audio.volume <= 0.3f)
            {
                audio.volume = 0.3f;
            }
        }
    }
}
