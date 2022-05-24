using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerCtrl : MonoBehaviour
{
    public List<Transform> spawnPos;
    private WaitForSeconds ws;
    public Image[] image;
    private Exit exit;
    public Transform cam;
    public Camera camera;
    public Image hp_1;
    public Image hp_2;
    public GameObject flareLight;
    public Light light;
    public GameObject sticky;
    public GameObject rawImage;
    public GameObject minimap;
    public AudioSource audio;
    public AudioClip[] clip;
    public Image hit;
    public Image heal;
    public Image clear;
    public Image die;
	private float speed = 7.0f;
    private float flareDur = 0;
    private float color = 1.0f;
    private float xRot = 60.0f;
    private float alpha = 0;
    private float dieAlpha = 0;
    public int hp = 2;
    private int temp;
    private int spawnIdx;
    private int flareIdx;
    public bool haveKey_1;
    public bool haveKey_2;
    public bool haveKey_3;
    private bool useFlare = false;
    private bool isDie = false;
    public string[] item;
    private Vector3 tempPos;

    void Start()
    {
        haveKey_1 = false;
        haveKey_2 = false;
        haveKey_3 = false;
        item = new string[3];
        ws = new WaitForSeconds(0.2f);
        image[0].enabled = false;
        image[1].enabled = false;
        image[2].enabled = false;
        exit = GameObject.Find("Exit").GetComponent<Exit>();
        cam = GameObject.Find("Camera").GetComponent<Transform>();
        audio = GetComponent<AudioSource>();
        temp = hp;
        hit.enabled = false;
        heal.enabled = false;
        clear.enabled = false;
        die.enabled = false;

        var group = GameObject.Find("PlayerSpawnPos");
        if (group != null)
        {
            group.GetComponentsInChildren<Transform>(spawnPos);
            spawnPos.RemoveAt(0);
        }
        spawnIdx = Random.Range(0, spawnPos.Count);
        transform.position = spawnPos[spawnIdx].position; // 플레이어 스폰 위치를 랜덤으로 설정

        StartCoroutine(CheckHP());
        StartCoroutine(PlayerHit());
    }

    void Update()
    {
        if (exit.getAllKey == true)
        {
            StopAllCoroutines();
            return;
        } // 탈출구에 모든 열쇠를 끼우면 모든 행동 중지

        if (isDie == true) // 사망하면
        {
            die.enabled = true; // 검은 화면을 나타내는 image를 활성화시키고
            dieAlpha += 0.5f * Time.deltaTime;
            die.color = new Color(die.color.r, die.color.b, die.color.b, dieAlpha); // 점점 진해지게
            if (dieAlpha >= 1.0f) // image가 완전히 불투명하게 되면
            {
                SceneManager.LoadScene("Lose"); // Scene 변경 후
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true; // 마우스 커서 보이게 설정
            }
            return;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1)) // 아이템 슬롯 1번을 눌렀을 때
        {
            switch (item[0])
            {
                case "Key_1": // 열쇠 사용 불가능일 경우 Beep 소리 재생할 것
                    break;
                case "Key_2":
                    break;
                case "Key_3":
                    break;
                case "FAK":
                    if (hp < 2) // 체력이 2 미만일 경우에만
                    {
                        audio.PlayOneShot(clip[0]); // 체력 회복 소리 재생하고
                        hp++; // 체력을 1 회복시키며
                        image[0].enabled = false; // 해당 슬롯 이미지를 비활성화,
                        item[0] = null; // 해당 아이템 배열 제거
                    }
                    break;
                case "Map":
                    if (rawImage.activeSelf == false) // 미니맵 image가 비활성화 상태이면
                    {
                        minimap.SetActive(true); // 미니맵 카메라를 활성화시키고
                        rawImage.SetActive(true); // 미니맵 image도 활성화시킴
                    }
                    else // 미니맵 image가 활성화 상태이면
                    {
                        minimap.SetActive(false); // 카메라를 비활성화시키고
                        rawImage.SetActive(false); // image도 비활성화시킴
                    }
                    break;
                case "Flare":
                    audio.PlayOneShot(clip[1]); // 조명탄 소리 재생
                    useFlare = true; // 조명탄 애니메이션 재생을 위한 변수
                    flareIdx = 1; // 해당 아이템 슬롯이 몇 번째 슬롯인지 확인하기 위한 변수
                    break;
                case "Trap":
                    Instantiate(sticky, new Vector3(transform.position.x, -0.4f, transform.position.z), transform.rotation); // 플레이어 위치에 덫 생성
                    image[0].enabled = false;
                    item[0] = null;
                    break;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            switch (item[1])
            {
                case "Key_1":
                    break;
                case "Key_2":
                    break;
                case "Key_3":
                    break;
                case "FAK":
                    if (hp < 2)
                    {
                        audio.PlayOneShot(clip[0]);
                        hp++;
                        image[1].enabled = false;
                        item[1] = null;
                    }
                    break;
                case "Map":
                    if (rawImage.activeSelf == false)
                    {
                        minimap.SetActive(true);
                        rawImage.SetActive(true);
                    }
                    else
                    {
                        minimap.SetActive(false);
                        rawImage.SetActive(false);
                    }
                    break;
                case "Flare":
                    audio.PlayOneShot(clip[1]);
                    useFlare = true;
                    flareIdx = 2;
                    break;
                case "Trap":
                    Instantiate(sticky, new Vector3(transform.position.x, 0, transform.position.z), transform.rotation);
                    image[1].enabled = false;
                    item[1] = null;
                    break;
            }
        } // 아이템 슬롯 2번이 눌렸을 때
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            switch (item[2])
            {
                case "Key_1":
                    break;
                case "Key_2":
                    break;
                case "Key_3":
                    break;
                case "FAK":
                    if (hp < 2)
                    {
                        audio.PlayOneShot(clip[0]);
                        hp++;
                        image[2].enabled = false;
                        item[2] = null;
                    }
                    break;
                case "Map":
                    if (rawImage.activeSelf == false)
                    {
                        minimap.SetActive(true);
                        rawImage.SetActive(true);
                    }
                    else
                    {
                        minimap.SetActive(false);
                        rawImage.SetActive(false);
                    }
                    break;
                case "Flare":
                    audio.PlayOneShot(clip[1]);
                    useFlare = true;
                    flareIdx = 3;
                    break;
                case "Trap":
                    Instantiate(sticky, new Vector3(transform.position.x, 0, transform.position.z), transform.rotation);
                    image[2].enabled = false;
                    item[2] = null;
                    break;
            }
        } // 3번이 눌렸을 때

        if (useFlare == true)
        {
            flareDur += Time.deltaTime;
            if (flareDur <= 10.0f)
            {
                flareLight.SetActive(true);
            }
            else // 조명탄 지속 시간이 10초를 넘으면
            {
                color -= 0.01f; // Point Light를 점점 어둡게 하고
                if (color <= 0)
                {
                    flareLight.SetActive(false); // Point Light를 비활성화시킴
                    useFlare = false;
                }
                light.color = new Color(color, color, color);
            }

            switch (flareIdx)
            {
                case 1: // 1번 슬롯에서 조명탄을 사용했으면
                    image[0].fillAmount -= 0.1f * Time.deltaTime; // image 속성값을 원래 상태로 돌려놓음
                    if (image[0].fillAmount <= 0)
                    {
                        image[0].enabled = false; // 1번 슬롯 image를 비활성화시킴
                        item[0] = null; // 1번 아이템 배열 제거
                        image[0].type = Image.Type.Simple;
                    }
                    break;
                case 2:
                    image[1].fillAmount -= 0.1f * Time.deltaTime;
                    if (image[1].fillAmount <= 0)
                    {
                        image[1].enabled = false;
                        item[1] = null;
                        image[1].type = Image.Type.Simple;
                    }
                    break;
                case 3:
                    image[2].fillAmount -= 0.1f * Time.deltaTime;
                    if (image[2].fillAmount <= 0)
                    {
                        image[2].enabled = false;
                        item[2] = null;
                        image[2].type = Image.Type.Simple;
                    }
                    break;
            }
        }
    }

    void FixedUpdate() // 이동 담당
    {
        if (exit.getAllKey == true) return;

        transform.Translate(Input.GetAxis("Horizontal") * Time.deltaTime * speed, 0, 0);
        transform.Translate(0, 0, Input.GetAxis("Vertical") * Time.deltaTime * speed);
    }

    IEnumerator CheckHP()
    {
        while (true)
        {
            if (hp == 2)
            {
                hp_1.enabled = true;
                hp_2.enabled = true;
            }
            else if (hp == 1)
            {
                hp_1.enabled = true; // 해당 HP 슬롯의 하트 image를 제거함
                hp_2.enabled = false;
            }
            else
            {
                hp_1.enabled = false;
                hp_2.enabled = false;
                isDie = true;
            }

            yield return ws;
        }
    }

    IEnumerator PlayerHit()
    {
        while (true)
        {
            hit.enabled = false;
            heal.enabled = false;

            if (temp > hp) // HP 감소했을 때
            {
                hit.enabled = true;
            }
            else if (temp < hp) // HP 증가했을 때
            {
                heal.enabled = true;
            }

            temp = hp; 

            yield return new WaitForSeconds(0.1f); // 0.1초 동안 피격 image 활성화
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Exit") // 탈출구 앞에 있을 때
        {
            if (Input.GetKeyDown(KeyCode.Alpha1)) // 1번 슬롯을 눌러 열쇠 사용 시
            {
                switch (item[0])
                {
                    case "Key_1": // 해당 슬롯에 있는 열쇠가 1번 열쇠일 때
                        exit.getKey_1 = true; // 탈출구 변수 제어
                        image[0].enabled = false; // 해당 슬롯 image 비활성화
                        item[0] = null; // 해당 아이템 배열 제거
                        haveKey_1 = false; // 플레이어의 열쇠 소지 유무 변수 제어
                        break;
                    case "Key_2":
                        exit.getKey_2 = true;
                        image[0].enabled = false;
                        item[0] = null;
                        haveKey_2 = false;
                        break;
                    case "Key_3":
                        exit.getKey_3 = true;
                        image[0].enabled = false;
                        item[0] = null;
                        haveKey_3 = false;
                        break;
                }
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                switch (item[1])
                {
                    case "Key_1":
                        exit.getKey_1 = true;
                        image[1].enabled = false;
                        item[1] = null;
                        haveKey_1 = false;
                        break;
                    case "Key_2":
                        exit.getKey_2 = true;
                        image[1].enabled = false;
                        item[1] = null;
                        haveKey_2 = false;
                        break;
                    case "Key_3":
                        exit.getKey_3 = true;
                        image[1].enabled = false;
                        item[1] = null;
                        haveKey_3 = false;
                        break;
                }
            } // 2번 슬롯을 눌러 열쇠 사용 시
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                switch (item[2])
                {
                    case "Key_1":
                        exit.getKey_1 = true;
                        image[2].enabled = false;
                        item[2] = null;
                        haveKey_1 = false;
                        break;
                    case "Key_2":
                        exit.getKey_2 = true;
                        image[2].enabled = false;
                        item[2] = null;
                        haveKey_2 = false;
                        break;
                    case "Key_3":
                        exit.getKey_3 = true;
                        image[2].enabled = false;
                        item[2] = null;
                        haveKey_3 = false;
                        break;
                }
            }
        }
        else if (other.gameObject.name == "Exit_Zone") // 탈출구가 있는 구역에 들어와있을 때
        {
            if (exit.rotSpeed >= 1000.0f) // 탈출구의 회전 속도가 1000f 이상이면
            {
                xRot -= 5.0f * Time.deltaTime;
                if (xRot <= 45.0f)
                {
                    xRot = 45.0f; // 카메라 x축 각도를 45도로 서서히 변경
                }
                cam.localEulerAngles = new Vector3(xRot, cam.localEulerAngles.y, cam.localEulerAngles.y);
                camera.fieldOfView -= 10.0f * Time.deltaTime; // 카메라가 탈출구를 점점 확대하도록 함
                if (camera.fieldOfView <= 10)
                {
                    clear.enabled = true; // 하얀 image를 활성화시키고
                    alpha += 0.5f * Time.deltaTime; // 그 image를 점점 진하게 함
                    if (alpha >= 1.0f) // image의 불투명도가 완전히 진해졌을 경우
                    {
                        SceneManager.LoadScene("Win"); // Scene 변경 후
                        Cursor.lockState = CursorLockMode.None;
                        Cursor.visible = true; // 마우스 커서를 보이게 함
                    }
                    clear.color = new Color(clear.color.r, clear.color.g, clear.color.b, alpha);
                }
            }
            else // 탈출구의 회전 속도가 0f 이하이면, 즉 탈출 조건을 달성하지 않았을 경우
            {
                cam.position = new Vector3(8.1952f, 23f, 6.5f); // 카메라의 위치와 각도 고정
                cam.eulerAngles = new Vector3(60, 0, 0);
            }
        }
    }

    private void OnTriggerExit(Collider other) // 탈출구가 있는 구역에서 나가면
    {
        if (other.gameObject.name == "Exit_Zone")
        {
            cam.localPosition = new Vector3(0, 15, 0); // 카메라가 다시 플레이어를 쫓아다니게 변경
            cam.localEulerAngles = new Vector3(90, 0, 0);
        }
    }
}
