using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiChargeController : MonoBehaviour
{
    public Animator[] leftAnimators; // 왼쪽 진영 캐릭터들의 Animator 배열
    public Animator[] rightAnimators; // 오른쪽 진영 캐릭터들의 Animator 배열

    public Transform[] leftCharacters; // 왼쪽 진영 캐릭터들의 Transform 배열
    public Transform[] rightCharacters; // 오른쪽 진영 캐릭터들의 Transform 배열

    public float chargeSpeed = 5f; // 돌진 속도
    private bool isCharging = false; // 돌진 상태 확인

    private Vector3[] leftTargetPositions; // 왼쪽 캐릭터들의 목표 위치
    private Vector3[] rightTargetPositions; // 오른쪽 캐릭터들의 목표 위치

    void Start()
    {
        // 초기화: 각 캐릭터가 중앙으로 이동하도록 목표 위치 설정
        leftTargetPositions = new Vector3[leftCharacters.Length];
        rightTargetPositions = new Vector3[rightCharacters.Length];

        for (int i = 0; i < leftCharacters.Length; i++)
        {
            leftTargetPositions[i] = new Vector3(0, leftCharacters[i].position.y, leftCharacters[i].position.z); // 중앙으로 이동
        }

        for (int i = 0; i < rightCharacters.Length; i++)
        {
            rightTargetPositions[i] = new Vector3(0, rightCharacters[i].position.y, rightCharacters[i].position.z); // 중앙으로 이동
        }
    }

    void Update()
    {
        // 돌진 시작 키 입력
        if (Input.GetKeyDown(KeyCode.C)) // C키로 돌진 시작
        {
            StartCharge();
        }

        // 돌진 중이라면 캐릭터 이동
        if (isCharging)
        {
            MoveCharacters();
        }
    }

    void StartCharge()
    {
        // 각 캐릭터의 돌진 애니메이션 트리거 활성화
        foreach (var animator in leftAnimators)
        {
            animator.SetTrigger("isMoving");
        }
        foreach (var animator in rightAnimators)
        {
            animator.SetTrigger("isMoving");
        }

        // 돌진 상태 활성화
        isCharging = true;
    }

    void MoveCharacters()
    {
        bool allCharactersReached = true;

        // 왼쪽 캐릭터 이동
        for (int i = 0; i < leftCharacters.Length; i++)
        {
            leftCharacters[i].position = Vector3.MoveTowards(
                leftCharacters[i].position,
                leftTargetPositions[i],
                chargeSpeed * Time.deltaTime
            );

            // 목표 위치에 도달했는지 확인
            if (Vector3.Distance(leftCharacters[i].position, leftTargetPositions[i]) > 0.1f)
            {
                allCharactersReached = false;
            }
        }

        // 오른쪽 캐릭터 이동
        for (int i = 0; i < rightCharacters.Length; i++)
        {
            rightCharacters[i].position = Vector3.MoveTowards(
                rightCharacters[i].position,
                rightTargetPositions[i],
                chargeSpeed * Time.deltaTime
            );

            // 목표 위치에 도달했는지 확인
            if (Vector3.Distance(rightCharacters[i].position, rightTargetPositions[i]) > 0.1f)
            {
                allCharactersReached = false;
            }
        }

        // 모든 캐릭터가 도착했는지 확인
        if (allCharactersReached)
        {
            EndCharge();
        }
    }

    void EndCharge()
    {
        // 돌진 상태 비활성화
        isCharging = false;

        // Idle 애니메이션으로 복귀
        foreach (var animator in leftAnimators)
        {
            animator.SetTrigger("isIdle");
        }
        foreach (var animator in rightAnimators)
        {
            animator.SetTrigger("isIdle");
        }
    }
}