using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypeWritterEffect : MonoBehaviour
{

    [SerializeField] private float writtingSpeed = 50f;


    public void Run(string textToType, TMP_Text textLabel)
    {
        StartCoroutine(TypeText(textToType, textLabel));
    }

    private IEnumerator TypeText(string textToType, TMP_Text textLabel)
    {
        float timer = 0;
        int charIndex = 0;

        while(charIndex < textToType.Length) 
        {
            timer += Time.deltaTime * writtingSpeed;
            charIndex = Mathf.FloorToInt(timer);
            charIndex = Mathf.Clamp(charIndex, 0, textToType.Length);

            textLabel.text = textToType.Substring(0, charIndex);

            soundPlaying = false;

            yield return null;
        }

        textLabel.text = textToType;
    }
}
