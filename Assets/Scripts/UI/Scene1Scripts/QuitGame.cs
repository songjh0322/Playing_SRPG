using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : MonoBehaviour
{
    public void Quit()
    {
        // Unity �����Ϳ��� ���� ������ Ȯ���ϰ� �޽��� ���
#if UNITY_EDITOR
        // �����Ϳ����� ������ ������� �����Ƿ�, �޽����� ���
        Debug.Log("���� ����");
#else
        Application.Quit();
#endif
    }
}