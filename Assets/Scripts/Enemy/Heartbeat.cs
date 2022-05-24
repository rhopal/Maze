using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heartbeat : MonoBehaviour
{
    private AudioSource audio;
    private EnemyCtrl enemy;

    void Start()
    {
        audio = GetComponent<AudioSource>();
        enemy = GetComponent<EnemyCtrl>();
    }

    void Update()
    {
        if (enemy.state != EnemyCtrl.State.PATROL) // 적이 플레이어를 바라보고 있는 경우
        {
            if (enemy.dist <= 15) // 적과 플레이어의 거리가 가까워지면 심음 빠르게
            {
                audio.pitch += 0.01f;
                if (audio.pitch >= 1.5f)
                {
                    audio.pitch = 1.5f;
                }
            }
        }
        else // 적이 플레이어를 바라보고 있지 않은 경우
        {
            audio.pitch -= 0.01f; // 심음 느리게
            if (audio.pitch <= 1.0f)
            {
                audio.pitch = 1.0f;
            }
        }
    }
}
