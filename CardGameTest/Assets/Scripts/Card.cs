using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Card : MonoBehaviour
{
    [Header("Cards Properties")]
    [SerializeField] MeshRenderer faceDownCard;
    public UniqueCard cardType;
    [SerializeField] MeshRenderer cardFacedUp;
    public bool cardFilled = false;

    [Header("Rotation Properties")]
    public float rotationSpeed = 100f;
    private float currentRotation = 0f;
    public bool isRotating = false;
    public bool isFaceUp = false;

    public float scaleFactor = 1.5f; 
    public float duration = 0.2f; 


    public bool cardUsed = false;

    private void OnMouseDown()
    {
        if (!cardUsed)
        {
            cardUsed = true;
            SoundManager.Instance.PlayFlipping();

            RotationCard();
            if (GameManager.Instance.firstPair)
            {
                GameManager.Instance.cardsQueue.Enqueue(this);  
                GameManager.Instance.firstPair = false;
            }
            else
            {
                GameManager.Instance.cardsQueue.Enqueue(this);
                GameManager.Instance.StartCompair();
                GameManager.Instance.firstPair = true;
                PlayerManager.Instance.playerPlays = PlayerManager.Instance.playerPlays + 1;

            }

            StartCoroutine(AnimationDelay(1.2f, true));
        }


    }

    public void RotationCard()
    {
        isRotating = true;
    }

    public void TurnCardFaceDown()
    {
        isRotating = true;
        isFaceUp = false;
        cardUsed = false;   
       

    }
    private void Update()
    {


        if (isRotating && !isFaceUp)
        {

            float rotationStep = rotationSpeed * Time.deltaTime;
            currentRotation += rotationStep;

            transform.Rotate(0f, 0f, rotationStep);

            if (currentRotation >= 180f)
            {

                float excessRotation = currentRotation - 180f;
                transform.Rotate(0f, 0f, -excessRotation);
                isRotating = false;
                currentRotation = 0f;
            }
        }
        else if (isRotating && isFaceUp)
        {
            SoundManager.Instance.PlayFlipping();

            float rotationStep = rotationSpeed * Time.deltaTime;
            currentRotation -= rotationStep;
            transform.Rotate(0f, 0f, -rotationStep);

            if (currentRotation <= 0f)
            {
                float excessRotation = -currentRotation;
                transform.Rotate(0f, 0f, excessRotation);
                isRotating = false;
                currentRotation = 0f;
                cardUsed = false;
            }

        }

    }

    public void TriggerScaleFeedback()
    {
        StartCoroutine(AnimateScale());
    }

    IEnumerator AnimateScale()
    {
        Vector3 originalScale = transform.localScale;
        Vector3 targetScale = originalScale * scaleFactor; 

 
        float time = 0f;
        while (time < duration)
        {
            transform.localScale = Vector3.Lerp(originalScale, targetScale, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.localScale = targetScale;


        time = 0f;
        while (time < duration)
        {
            transform.localScale = Vector3.Lerp(targetScale, originalScale, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.localScale = originalScale;
    }


    public void RotationDelaySeconds(float timer)
    {
      
        StartCoroutine(Returning(timer));


    }

    public void CallTypeCard(UniqueCard card)
    {
        cardType = card;
        cardFacedUp.material = card.CardMaterial;
        cardFilled = true;
    }

    IEnumerator Returning(float timer)
    {
        yield return new WaitForSeconds(timer);
        RotationCard();

        isFaceUp = false;
       // cardUsed = false;
    }

    IEnumerator AnimationDelay(float timer, bool value1)
    {
        yield return new WaitForSeconds(timer);
      
        isFaceUp = value1;
        cardUsed = true;    
    }
}
