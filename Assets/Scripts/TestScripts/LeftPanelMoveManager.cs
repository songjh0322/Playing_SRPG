using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftPanelMoveManager : MonoBehaviour
{
    public GameObject leftPanelObject; // 왼쪽 패널 오브젝트
    public float moveDistance = 500f;  // 이동할 거리 (오른쪽으로 이동)
    public float moveSpeed = 2f;       // 이동 속도

    // 캐릭터 배치 완료 후 왼쪽 패널을 이동시키는 함수
    public void MoveLeftPanel()
    {
        StartCoroutine(MoveLeftPanelCoroutine());
    }

    IEnumerator MoveLeftPanelCoroutine()
    {
        Vector3 startPosition = leftPanelObject.transform.localPosition;
        Vector3 endPosition = new Vector3(startPosition.x + moveDistance, startPosition.y, startPosition.z); // X축으로 이동

        float elapsedTime = 0;

        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime * moveSpeed;
            leftPanelObject.transform.localPosition = Vector3.Lerp(startPosition, endPosition, elapsedTime);
            yield return null;
        }
    }
}
