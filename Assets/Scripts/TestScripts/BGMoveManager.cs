using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMoveManager : MonoBehaviour
{
    public GameObject bgObject; // BG 오브젝트
    public float moveDistance = -500f; // 이동할 거리
    public float moveSpeed = 2f; // 이동 속도

    // 모든 캐릭터가 배치되었을 때 호출되는 함수
    public void MoveBG()
    {
        if (bgObject != null)
        {
            StartCoroutine(MoveBGCoroutine());
        }
        else
        {
            Debug.LogError("BG 오브젝트가 설정되지 않았습니다.");
        }
    }

    IEnumerator MoveBGCoroutine()
    {
        Vector3 startPosition = bgObject.transform.localPosition;
        Vector3 endPosition = new Vector3(startPosition.x - moveDistance, startPosition.y, startPosition.z); // X축으로 이동

        float elapsedTime = 0;

        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime * moveSpeed;
            bgObject.transform.localPosition = Vector3.Lerp(startPosition, endPosition, elapsedTime);
            yield return null;
        }
    }
}
