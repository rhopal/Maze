using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void ToGame() // Title Scene이나 Loss Scene에서 Game Scene으로 Scene 이동
    {
        SceneManager.LoadScene("Game");
        Cursor.lockState = CursorLockMode.Locked; // 마우스 커서 제어 불가능
        Cursor.visible = false; // 마우스 커서를 안 보이게 설정
    }
}
