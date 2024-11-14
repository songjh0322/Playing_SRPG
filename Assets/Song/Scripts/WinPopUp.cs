using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinPopUp : MonoBehaviour
{
    public Image fillImage; // Image 컴포넌트
    public Animator characterAnimator; // Animator 컴포넌트
    public string animationTriggerName = "PlayAnimation"; // 애니메이션 트리거 이름
    public int repeatCount = 3; // 반복 횟수
    public float fillSpeed = 0.5f; // Fill 속도

    private void Start()
    {
        StartCoroutine(AnimateFillAmount());
    }

    private IEnumerator AnimateFillAmount()
    {
        for (int i = 0; i < repeatCount; i++)
        {
            // Fill Amount를 0에서 1로 증가
            float fill = 0;
            while (fill < 1)
            {
                fill += Time.deltaTime * fillSpeed;
                fillImage.fillAmount = fill;
                yield return null;
            }

            // Fill Amount를 1에서 0으로 감소
            fill = 1;
            while (fill > 0)
            {
                fill -= Time.deltaTime * fillSpeed;
                fillImage.fillAmount = fill;
                yield return null;
            }
        }

        // 반복이 완료된 후에만 애니메이션 트리거 실행
        GetAnimation();

    }
    void GetAnimation()
    {
        characterAnimator.SetTrigger(animationTriggerName);
    }
}