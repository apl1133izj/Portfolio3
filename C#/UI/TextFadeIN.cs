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
            // fadeOutTime 갱신 (더 느리게 0으로 수렴)
            fadeOutTime -= Time.deltaTime * 0.1f; // 적절한 값을 사용하여 느리게 조절

            // Mathf.Clamp를 사용하여 fadeOutTime이 음수로 가지 않도록 보장
            fadeOutTime = Mathf.Clamp(fadeOutTime, 0f, 1f);

            // fadeOutTime을 사용하여 알파값 조정
            float fadeOutTimeLerp = Mathf.Lerp(0, 1, fadeOutTime);
            fadeText[i].color = new Color(fadeText[i].color.r, fadeText[i].color.g, fadeText[i].color.b, fadeOutTimeLerp);
        }
    }
}
