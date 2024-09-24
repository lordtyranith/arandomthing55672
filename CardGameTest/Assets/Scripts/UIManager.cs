using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>   
{
    [SerializeField] TextMeshProUGUI scoreCounter;
    [SerializeField] TextMeshProUGUI ComboCounter;
    [SerializeField] TextMeshProUGUI movesCounter;
    [SerializeField] TextMeshProUGUI stageLevel;
    [SerializeField] TextMeshProUGUI totalScore;
    [SerializeField] Image blackscreen;
    public float fadeDuration = 5.0f;
    public bool blackScreenOn = true;


    public void UpdatingUI()
    {
        scoreCounter.text = PlayerManager.Instance.playerScore.ToString();
        ComboCounter.text = PlayerManager.Instance.playerCombo.ToString();
        movesCounter.text = PlayerManager.Instance.playerPlays.ToString();
        totalScore.text = (PlayerManager.Instance.playerScore+PlayerManager.Instance.playerTotalScore).ToString();
        stageLevel.text = PlayerManager.Instance.stageLevel.ToString();  
        
    }

    public void StartFadeOut()
    {
        StartCoroutine(FadeOut());
    }

    public void StartFadeIn()
    {
        StartCoroutine(FadeInOut());  
    }
    IEnumerator FadeOut()
    {
        Color originalColor = blackscreen.color; 
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alphaValue = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration); 
            blackscreen.color = new Color(originalColor.r, originalColor.g, originalColor.b, alphaValue); 
            yield return null;
        }

        blackscreen.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
        yield return new WaitForSeconds(0.2f);
        blackscreen.gameObject.SetActive(false);
        blackScreenOn = false;

    }

    IEnumerator FadeInOut()
    {

        blackScreenOn = true;

        blackscreen.gameObject.SetActive(true);

        Color originalColor = blackscreen.color;
        blackscreen.gameObject.SetActive(true);
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alphaValue = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration); 
            blackscreen.color = new Color(originalColor.r, originalColor.g, originalColor.b, alphaValue);
            yield return null;
        }

        blackscreen.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1f);      
        yield return new WaitForSeconds(1f);
        elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alphaValue = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration); 
            blackscreen.color = new Color(originalColor.r, originalColor.g, originalColor.b, alphaValue);
            yield return null;
        }

        blackscreen.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f); 
        blackscreen.gameObject.SetActive(false);
        blackScreenOn = false;



    }
}
