using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundCtrl : MonoBehaviour
{
    private AudioSource audio;
    public AudioClip[] clip;
    private EnemyCtrl enemy;
    public Exit exit;
    private int number;
    private float dur;

    void Start()
    {
        audio = GetComponent<AudioSource>();
        enemy = GameObject.Find("Enemy").GetComponent<EnemyCtrl>();

        StartCoroutine(PlayAudio());
    }

    void Update()
    {
        if (exit.getAllKey == true)
        {
            StopAllCoroutines();
            return;
        } // 탈출구에 모든 열쇠를 끼우면 audio 재생 중지

        if (enemy.state != EnemyCtrl.State.PATROL) // Enemy가 플레이어를 바라보고 있으면 audio 랜덤 재생 X
            dur = 0;
        else
            dur += Time.deltaTime;
    }

    IEnumerator PlayAudio()
    {
        while (true)
        {
            if (dur >= 20.0f) // Enemy가 플레이어를 바라보고 있지 않을 때 그 시간이 20초 이상이면
            {
                number = Random.Range(0, clip.Length); // AudioClip 중 랜덤으로 한 가지를 골라서
                audio.PlayOneShot(clip[number]); // 그 clip 재생
                dur = 0;
            }

            yield return new WaitForSeconds(0.1f);
        }
    }
}
