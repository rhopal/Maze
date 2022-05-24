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
    public float speed = 6.0f; // 평상시 이동 속도
    public float slowSpeed = 3.0f; // 덫을 밟았을 때의 이동 속도
    private float slowDur = 0; // 덫 효과 지속 시간
    public float traceDist = 30.0f;
    public float attackDist = 2.0f;
    public float dist;
    private float attackDelay = 0; // 공격 후 몇 초 동안 가만히 있을 지에 대한 변수
    private bool isAttack = false; // 공격 후 가만히 있을지 말지를 결정하는 변수
    private bool isTrapped = false; // 덫을 밟았는지 확인하는 변수
    
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
        if (exit.getAllKey == true) // 탈출구에 열쇠를 다 끼우면 audio 재생 중지하고 모든 행동 중지
        {
            audio.Stop();
            return;
        }

        dist = Vector3.Distance(transform.position, playerTr.position);
        transform.LookAt(playerTr); // 항상 플레이어을 바라봄으로써 상태를 결정함

        if (isAttack == true) // 공격 후 2초 동안 모든 행동 중지
        {
            attackDelay += Time.deltaTime;
            if (attackDelay >= 2.0f)
            {
                isAttack = false;
                attackDelay = 0;
            }
            return;
        }

        if (Physics.Raycast(transform.position, transform.forward, out ray)) // 항상 플레이어 방향으로 raycast 발사
        {
            if (ray.collider.name == "Player") // raycast가 플레이어에 적중했을 때
            {
                if (dist <= attackDist) // Enemy와 플레이어의 거리가 attackDist 이하이면
                {
                    state = State.ATTACK; // 상태를 ATTACK으로 바꾸고
                    agent.speed = 0; // 제자리에 정지한 후
                    player.hp--; // 플레이어의 체력을 1 감소시킨 다음
                    isAttack = true; // 2초 동안 행동 중지
                }
                else if (dist <= traceDist) // 거리가 traceDist 이하이면
                {
                    state = State.TRACE; // 상태를 TRACE로 바꾸고
                    if (isTrapped == false) // 덫에 걸린 상태가 아니면 이동 속도를 기본 이동 속도로 변경
                    {
                        agent.speed = speed;
                    }
                    else // 덫에 걸린 상태이면 이동 속도를 감소시키고 5초 동안 감소된 이동 속도를 유지
                    {
                        agent.speed = slowSpeed;
                        slowDur += Time.deltaTime;
                        if (slowDur >= 5.0f)
                            isTrapped = false;
                    }
                    agent.destination = playerTr.position; // 플레이어를 쫓아감
                }
            }
            else // 거리가 traceDist 이상이면
            {
                state = State.PATROL; // 상태를 PATROL로 바꾸고
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
        if (other.gameObject.tag == "Trap") // 덫과 충돌하면
        {
            isTrapped = true; // 덫을 밟았는지 확인하는 변수를 true로 변경하고
            Destroy(other.gameObject); // 덫 오브젝트 제거
        }
    }
}
