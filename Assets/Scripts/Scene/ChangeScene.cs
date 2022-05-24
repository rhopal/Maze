using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void ToGame() // 타이틀 scene이나 패배 scene에서 게임 scene으로 가기 위한 함수
    {
        SceneManager.LoadScene("Game");
        Cursor.lockState = CursorLockMode.Locked; // 마우스 커서 제어 불가능
        Cursor.visible = false; // 마우스 커서를 안 보이게 설정
    }
}
