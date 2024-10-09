using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelection : MonoBehaviour
{
    public GameObject[] characters;  // 캐릭터 프리팹 배열
    public Transform displayArea;    // 캐릭터를 표시할 위치 (CharacterDisplayArea)
    public Vector3 displayScale = new Vector3(3f, 3f, 3f); // 캐릭터를 표시할 때 적용할 스케일 값
    private GameObject currentCharacter;  // 현재 표시된 캐릭터

    // 캐릭터 선택 버튼이 클릭되면 호출되는 함수
    public void OnSelectCharacter(int index)
    {
        // 기존 캐릭터가 있으면 제거
        if (currentCharacter != null)
        {
            Destroy(currentCharacter);
        }

        // 새로운 캐릭터 인스턴스화
        currentCharacter = Instantiate(characters[index], displayArea.position, Quaternion.identity);
        // 캐릭터의 스케일을 displayScale로 변경
        currentCharacter.transform.localScale = displayScale;
        // 선택한 캐릭터를 표시 영역의 자식 오브젝트로 설정
        //currentCharacter.transform.SetParent(displayArea);
    }
}
