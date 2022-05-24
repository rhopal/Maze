using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightCtrl : MonoBehaviour
{
	public GameObject player;
    public Exit exit;
    public float rotSpeed = 10.0f; // 회전 속도
    private Quaternion N = Quaternion.Euler(0, 0, 0); // 미리 회전 각을 설정
    private Quaternion S = Quaternion.Euler(0, 180, 0);
    private Quaternion E = Quaternion.Euler(0, 90, 0);
    private Quaternion W = Quaternion.Euler(0, -90, 0);
    private Quaternion NE = Quaternion.Euler(0, 45, 0);
    private Quaternion SE = Quaternion.Euler(0, 135, 0);
    private Quaternion NW = Quaternion.Euler(0, -45, 0);
    private Quaternion SW = Quaternion.Euler(0, -135, 0);

	void Update()
	{
        if (exit.getAllKey == true) return; // 탈출구에 모든 열쇠를 끼웠으면 행동 중지

        Vector3 pos = new Vector3(player.transform.position.x, 0.1f, player.transform.position.z);
		transform.position = pos; // 조명 위치 설정

        if (Input.GetKey(KeyCode.UpArrow)) // 북
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, N, rotSpeed * Time.deltaTime); // 부드러운 회전을 위해 Lerp 함수 사용
        }
        if (Input.GetKey(KeyCode.DownArrow)) // 남
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, S, rotSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.LeftArrow)) // 서
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, W, rotSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.RightArrow)) // 동
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, E, rotSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.RightArrow)) // 북동
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, NE, rotSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.RightArrow)) // 남동
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, SE, rotSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.LeftArrow)) // 북서
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, NW, rotSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.LeftArrow)) // 남서
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, SW, rotSpeed * Time.deltaTime);
        }
    }
}
