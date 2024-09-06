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

            Debug.Log("여기까지 진행됨!");

            // Collider가 있는지 확인
            if (hit.collider != null)
            {
                // 클릭한 오브젝트가 Box인지 확인
                if (hit.collider.gameObject == gameObject)
                {
                    Debug.Log("Box clicked!");
                }
            }
        }
    }
}
