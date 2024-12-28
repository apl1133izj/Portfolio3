using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class typerResultswindow : MonoBehaviour
{
    public string originText;
    public Text resultswindowText;
    GameManager gameManager;
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        resultswindowText = GetComponent<Text>();
        if (resultswindowText == null)
        {
            return;
        }
        originText = resultswindowText.text;
        resultswindowText.text = "" + gameManager.reStartCount;
        StartCoroutine(Typing());
    }
    IEnumerator Typing()
    {
        int typingLength = originText.Length;

        for (int index = 0; index < typingLength; index++)
        {
            resultswindowText.text = originText.Substring(0, index + 1);
            yield return new WaitForSeconds(0.05f);
        }
    }
}