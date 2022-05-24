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
        if (enemy.state != EnemyCtrl.State.PATROL) // Enemy가 플레이어를 바라보고 있지 않을 때
        {
            if (enemy.dist <= 15) // Enemy와 플레이어의 거리가 15f 이하이면 audio 속도 점점 빠르게
            {
                audio.pitch += 0.01f;
                if (audio.pitch >= 1.5f)
                {
                    audio.pitch = 1.5f;
                }
            }
        }
        else // Enemy와 플레이어의 거리가 15f 초과이면 audio 속도 점점 느리게
        {
            audio.pitch -= 0.01f;
            if (audio.pitch <= 1.0f)
            {
                audio.pitch = 1.0f;
            }
        }
    }
}
