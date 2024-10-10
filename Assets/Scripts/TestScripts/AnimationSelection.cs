using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationSelection : MonoBehaviour
{
    public GameObject currentCharacter;
    private Animator characterAnimator;
    public CharacterSelection characterSelection;

    public SPUM_AnimationManager animationManager;  // SPUM_AnimationManager 스크립트 참조
    public Button idleButton;
    public Button moveButton;
    public Button attackButton;
    public Button damagedButton;
    public Button diedButton;
    public Button debuffedButton;



    void Start()
    {
        idleButton.onClick.AddListener(() => PlayAnimation("IDLE"));
        moveButton.onClick.AddListener(() => PlayAnimation("MOVE"));
        attackButton.onClick.AddListener(() => PlayAnimation("ATTACK"));
        damagedButton.onClick.AddListener(() => PlayAnimation("DAMAGED"));
        diedButton.onClick.AddListener(() => PlayAnimation("DEATH"));
        debuffedButton.onClick.AddListener(() => PlayAnimation("DEBUFF"));
    }

    void PlayAnimation(string animationState)
    {
        currentCharacter = characterSelection.GetCurrentCharacter();

        if (currentCharacter != null)
        {
            // charn의 자식 오브젝트인 UnitRoot에 접근하여 애니메이터를 가져옴
            Transform unitRoot = currentCharacter.transform.Find("UnitRoot");
            if (unitRoot != null)
            {
                characterAnimator = unitRoot.GetComponent<Animator>();
            }
        }

        if (animationState == "MOVE")
        {
            characterAnimator.SetBool("1_Move", true);
        }
        // Debuff 상태로 가기 위해 '5_Debuff' 파라미터를 true로 설정
        else if (animationState == "DEBUFF")
        {
            characterAnimator.SetBool("5_Debuff", true);
        }
        else
        {
            characterAnimator.SetBool("1_Move", false);
            characterAnimator.SetBool("5_Debuff", false);
        }

        if (characterAnimator != null)
        {
            // 애니메이터 상태 변경
            characterAnimator.Play(animationState, 0, 0f);
        }
        else
        {
            Debug.LogError("Animator is not assigned or found.");
        }
    }
}
