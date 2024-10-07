using UnityEngine;

public class UnitMovement : MonoBehaviour
{
    public Camera mainCamera;
    public float moveSpeed = 500f; // 유닛의 이동 속도
    public GameObject unit; // 유닛을 이동시킬 오브젝트
    public LayerMask tileLayerMask; // 타일 레이어 마스크 (타일이 속한 레이어 설정)
    public LayerMask unitLayerMask;
    public float rayLength = 1000f; // Ray의 길이
    public Color rayColor = Color.red; // Ray 시각화 색상

    private Vector3 targetPosition; // 유닛이 이동할 목표 위치
    private bool isMoving = false; // 유닛이 이동 중인지 여부

    void Start()
    {
        // 처음에는 유닛의 현재 위치를 목표 위치로 설정
        targetPosition = unit.transform.position;
    }

    void Update()
    {
        // 마우스 클릭 감지
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

            Vector2 rayStart = mousePosition;
            Vector2 rayDirection = Vector2.zero; // 2D에서는 방향이 0이 됩니다.

            Debug.DrawLine(rayStart, rayStart + rayDirection * rayLength, rayColor, 1.0f); // Ray 시각화 (1초 동안)

            // Raycast 실행
            RaycastHit2D hit = Physics2D.Raycast(rayStart, rayDirection, rayLength);

            if (hit.collider != null)
            {
                Debug.Log("Hit Object: " + hit.collider.gameObject.name);
                int layerID = hit.collider.gameObject.layer;
                switch(layerID)
                {
                    case 15:  // 유닛 레이어
                        unit = hit.collider.gameObject;
                        break;
                    case 10:  // 타일 레이어 (Tiles)
                        targetPosition = hit.collider.transform.position;
                        isMoving = true;
                        break;

                    
                }
            }
        }

        // 유닛이 목표 위치로 이동 중일 경우
        if (isMoving)
        {
            unit.transform.position = Vector3.MoveTowards(unit.transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // 목표 위치에 도달하면 이동 멈춤
            if (Vector3.Distance(unit.transform.position, targetPosition) < 0.1f)
            {
                unit.transform.position = targetPosition;
                isMoving = false;
            }
        }
    }
}
