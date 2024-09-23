using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    [SerializeField] Button newGame;
    [SerializeField] Button continueGame;
    [SerializeField] Button exit;


    private void Start()
    {
        newGame.onClick.RemoveAllListeners();
        newGame.onClick.AddListener(() => UIManager.Instance.StartFadeOut());
        newGame.onClick.AddListener(() => ButtonsGone());
        newGame.onClick.AddListener(() => SoundManager.Instance.PlayFlipping());
        newGame.onClick.AddListener(() => GameManager.Instance.StartingLevel());

        continueGame.onClick.RemoveAllListeners();
        continueGame.onClick.AddListener(() => UIManager.Instance.StartFadeOut());
        continueGame.onClick.AddListener(() => ButtonsGone());
        continueGame.onClick.AddListener(() => SoundManager.Instance.PlayFlipping());
        continueGame.onClick.AddListener(() => GameManager.Instance.LoadGame());
        continueGame.onClick.AddListener(() => GameManager.Instance.LoadingSavedGame());

        exit.onClick.RemoveAllListeners();
        exit.onClick.AddListener(() => Application.Quit());


    }



    public void ButtonsGone()
    {
        newGame.gameObject.SetActive(false);
        continueGame.gameObject.SetActive(false);
        exit.gameObject.SetActive(false);    
    }
}
