using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class TextFadeIN : MonoBehaviour
{
    public Text[] fadeText;
    float fadeOutTime = 0.75f;

    private void Update()
    {
        for (int i = 0; i <= 3; i++)
        {
            // fadeOutTime ���� (�� ������ 0���� ����)
            fadeOutTime -= Time.deltaTime * 0.1f; // ������ ���� ����Ͽ� ������ ����

            // Mathf.Clamp�� ����Ͽ� fadeOutTime�� ������ ���� �ʵ��� ����
            fadeOutTime = Mathf.Clamp(fadeOutTime, 0f, 1f);

            // fadeOutTime�� ����Ͽ� ���İ� ����
            float fadeOutTimeLerp = Mathf.Lerp(0, 1, fadeOutTime);
            fadeText[i].color = new Color(fadeText[i].color.r, fadeText[i].color.g, fadeText[i].color.b, fadeOutTimeLerp);
        }
    }
}
