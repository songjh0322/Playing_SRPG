using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    public Animator attackerAnimator; // 왼쪽 캐릭터의 Animator
    public Animator targetAnimator; // 오른쪽 캐릭터의 Animator

    void Update()
    {
        // 공격 입력 받기 (예: 스페이스바)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(PerformAttack());
        }
    }

    IEnumerator PerformAttack()
    {
        // 공격 애니메이션 실행
        attackerAnimator.SetTrigger("isAttack");

        // 공격 애니메이션이 끝날 때까지 대기
        yield return new WaitForSeconds(attackerAnimator.GetCurrentAnimatorStateInfo(0).length);

        // 상대방 죽음 애니메이션 실행
        targetAnimator.SetTrigger("isDied");
    }
}