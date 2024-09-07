using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackLogTest : MonoBehaviour
{
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
                // 클릭한 오브젝트의 Collider 확인
                //Debug.Log("Collider가 감지되었습니다.");

                // 태그가 "Player"인지 확인
                if (hit.collider.CompareTag("Player"))
                {
                    Debug.Log("Player 오브젝트가 클릭되었습니다.");
                }
            }
        }
    }
}
