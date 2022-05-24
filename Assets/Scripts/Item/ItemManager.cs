using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public List<Transform> itemPos;
    private int number;
    public GameObject[] item;

    void Start()
    {
        var group = GameObject.Find("ItemSpawnPos"); // 아이템을 랜덤으로 스폰시키기 위해
        if (group != null)
        {
            group.GetComponentsInChildren<Transform>(itemPos); // 150개의 위치값을 리스트에 저장
            itemPos.RemoveAt(0);
        }

        for (int i = 0; i < 7; i++) // 아이템 수만큼
        {
            number = Random.Range(0, itemPos.Count);
            switch (i)
            {
                case 0:
                    Instantiate(item[i], itemPos[number].position, itemPos[number].rotation);
                    break;
                case 1:
                    Instantiate(item[i], itemPos[number].position, itemPos[number].rotation);
                    break;
                case 2:
                    Instantiate(item[i], itemPos[number].position, itemPos[number].rotation);
                    break;
                case 3:
                    Instantiate(item[i], itemPos[number].position, itemPos[number].rotation);
                    break;
                case 4:
                    Instantiate(item[i], itemPos[number].position, itemPos[number].rotation); // 랜덤으로
                    break;
                case 5:
                    Instantiate(item[i], itemPos[number].position, itemPos[number].rotation); // 아이템을
                    break;
                case 6:
                    Instantiate(item[i], itemPos[number].position, itemPos[number].rotation); // 스폰하고
                    break;
            }
            itemPos.RemoveAt(number); // 스폰에 사용된 리스트 index를 제거
        }
    }
}
