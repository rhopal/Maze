using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyCtrl : MonoBehaviour
{
    public enum State
    {
        PATROL,
        TRACE,
        ATTACK
    }
    public State state = State.PATROL;
    private RaycastHit ray;
    private NavMeshAgent agent;
    private Transform playerTr;
    private PlayerCtrl player;
    private AudioSource audio;
    public Exit exit;
    public float speed = 6.0f; // 기본 이동 속도
    public float slowSpeed = 3.0f; // 감속 시 이동 속도
    private float slowDur = 0; // 감속 효과 지속 시간
    public float traceDist = 30.0f;
    public float attackDist = 2.0f;
    public float dist;
    private float attackDelay = 0; // 공격 후 멈춰있는 시간
    private bool isAttack = false; // 공격 후 행동 제어
    private bool isTrapped = false; // 함정 발동 확인
    
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        playerTr = GameObject.Find("Player").GetComponent<Transform>();
        player = GameObject.Find("Player").GetComponent<PlayerCtrl>();
        audio = GetComponent<AudioSource>();
        agent.speed = speed;
    }
    
    void Update()
    {
        if (exit.getAllKey == true) // 탈출구에 열쇠를 다 끼우면 audio 및 행동 정지
        {
            audio.Stop();
            return;
        }

        dist = Vector3.Distance(transform.position, playerTr.position);
        transform.LookAt(playerTr);

        if (isAttack == true) // 공격 후 2초 동안 정지
        {
            attackDelay += Time.deltaTime;
            if (attackDelay >= 2.0f)
            {
                isAttack = false;
                attackDelay = 0;
            }
            return;
        }

        if (Physics.Raycast(transform.position, transform.forward, out ray)) // 항상 플레이어 방향으로 레이 발사
        {
            if (ray.collider.name == "Player")
            {
                if (dist <= attackDist)
                {
                    state = State.ATTACK;
                    agent.speed = 0;
                    player.hp--;
                    isAttack = true;
                }
                else if (dist <= traceDist)
                {
                    state = State.TRACE;
                    if (isTrapped == false)
                    {
                        agent.speed = speed;
                    }
                    else
                    {
                        agent.speed = slowSpeed;
                        slowDur += Time.deltaTime;
                        if (slowDur >= 5.0f)
                            isTrapped = false;
                    }
                    agent.destination = playerTr.position;
                }
            }
            else
            {
                state = State.PATROL;
                if (isTrapped == false)
                {
                    agent.speed = speed;
                }
                else
                {
                    agent.speed = slowSpeed;
                    slowDur += Time.deltaTime;
                    if (slowDur >= 5.0f)
                        isTrapped = false;
                }
                agent.destination = playerTr.position; // 플레이어를 쫓아감
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Trap") // 함정에 걸렸을 경우
        {
            isTrapped = true;
            Destroy(other.gameObject);
        }
    }
}
