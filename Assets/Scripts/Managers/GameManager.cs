using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class GameManager : MonoBehaviour
{
    private CharacterA characterA;
    private CharacterB characterB;

    void Start()
    {
        Debug.Log("게임 매니저 생성됨: " + this.gameObject.name);

        // 이름이 "A"인 오브젝트를 찾고, 그 오브젝트에 CharacterA 할당
        GameObject objectA = GameObject.Find("A");
        if (objectA != null)
        {
            characterA = objectA.AddComponent<CharacterA>();
            Debug.Log("CharacterA가 오브젝트 A에 할당되었습니다.");
        }
        else
        {
            Debug.LogWarning("오브젝트 A를 찾을 수 없습니다.");
        }

        // 이름이 "B"인 오브젝트를 찾고, 그 오브젝트에 CharacterB 할당
        GameObject objectB = GameObject.Find("B");
        if (objectB != null)
        {
            characterB = objectB.AddComponent<CharacterB>();
            Debug.Log("CharacterB가 오브젝트 B에 할당되었습니다.");
        }
        else
        {
            Debug.LogWarning("오브젝트 B를 찾을 수 없습니다.");
        }
    }

    // 첫 번째 클릭에서 선택된 오브젝트를 저장할 변수
    private GameObject firstSelectedObject = null;

    void Update()
    {
        // 마우스 좌클릭을 감지
        if (Input.GetMouseButtonDown(0))
        {
            // 마우스 위치에서 Ray 발사
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            // Ray 시각적 디버깅
            Debug.DrawRay(mousePosition, Vector2.zero, Color.red, 1.0f);

            // Collider가 있는지 확인
            if (hit.collider != null)
            {
                // 첫 번째 클릭: CharacterA 오브젝트 선택
                if (firstSelectedObject == null && hit.collider.GetComponent<CharacterA>() != null)
                {
                    firstSelectedObject = hit.collider.gameObject;  // 첫 번째 오브젝트 저장
                    Debug.Log("CharacterA가 선택되었습니다.");
                }
                // 두 번째 클릭: CharacterB 오브젝트 선택
                else if (firstSelectedObject != null && hit.collider.GetComponent<CharacterB>() != null)
                {
                    GameObject secondSelectedObject = hit.collider.gameObject;  // 두 번째 오브젝트 저장
                    Debug.Log("CharacterB가 선택되었습니다.");

                    firstSelectedObject.GetComponent<CharacterA>().attack(secondSelectedObject.GetComponent<CharacterB>());

                    // 두 번의 클릭 완료 후 초기화
                    firstSelectedObject = null;
                }
                // 두 번째 클릭이 CharacterB가 아닌 경우 대기
                else if (firstSelectedObject != null && hit.collider.GetComponent<CharacterB>() == null)
                {
                    Debug.Log("두 번째 클릭은 CharacterB여야 합니다. 다시 시도하세요.");
                }
            }
        }
    }

}
