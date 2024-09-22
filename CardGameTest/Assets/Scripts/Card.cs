using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Card : MonoBehaviour
{
    [Header("Cards Properties")]
    [SerializeField] Material faceDownCard;
    [SerializeField] Material faceUpCard;

    [Header("Rotation Properties")]
    public float rotationSpeed = 100f;  
    private float currentRotation = 0f; 
    public bool isRotating = false;
    public bool isFaceUp = false;

    private void OnMouseDown()
    {
        RotationCard();
    }

    public void RotationCard()
    {
        isRotating = true;
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
        else if(isRotating && isFaceUp)
        {
            float rotationStep = rotationSpeed * Time.deltaTime;
            currentRotation -= rotationStep;
            transform.Rotate(0f, 0f, -rotationStep);

            if (currentRotation <= 0f)
            {
                float excessRotation = -currentRotation;
                transform.Rotate(0f, 0f, excessRotation); 
                isRotating = false; 
                currentRotation = 0f; 
            }
        }


     }

  

    public void Hit()
    {

    }

    public void Miss()
    {

    }


}
