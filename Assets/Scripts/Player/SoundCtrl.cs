using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundCtrl : MonoBehaviour
{
    private AudioSource audio;
    public AudioClip[] clip; // 다양한 사운드를 랜덤으로 재생하기 위해 미리 리스트에 저장
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
        if (exit.getAllKey == true) // 탈출구에 모든 열쇠를 끼웠을 경우 사운드 재생 방지
        {
            StopAllCoroutines();
            return;
        }

        if (enemy.state != EnemyCtrl.State.PATROL) // 적이 플레이어를 쫓고 있을 경우 재생 정지
            dur = 0;
        else
            dur += Time.deltaTime;
    }

    IEnumerator PlayAudio()
    {
        while (true)
        {
            if (dur >= 20.0f) // 적이 플레이어를 쫓고 있지 않을 때 그 시간이 20초 이상일 경우
            {
                number = Random.Range(0, clip.Length);
                audio.PlayOneShot(clip[number]);
                dur = 0;
            }

            yield return new WaitForSeconds(0.1f);
        }
    }
}
