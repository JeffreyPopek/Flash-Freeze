using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class Sign : MonoBehaviour
{
    [SerializeField] private GameObject dialogBox;
    [SerializeField] private TMP_Text dialogText;
    [SerializeField] private string dialog;
    [SerializeField] private bool playerInRange;

    [SerializeField] private float writtingSpeed = 50f;
    private IEnumerator coroutine;

    //typing sounds
    [SerializeField] private int frequencyLevel = 2;
    [SerializeField] float maxPitch = 1.0f;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        dialogBox.SetActive(true);

        coroutine = TypeText(dialog, dialogText);
        StartCoroutine(coroutine);

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        dialogBox.SetActive(false);
        StopCoroutine(coroutine);
    }

    private IEnumerator TypeText(string textToType, TMP_Text textLabel)
    {
        float timer = 0;
        int charIndex = 0;

        while (charIndex < textToType.Length)
        {
            timer += Time.deltaTime * writtingSpeed;
            charIndex = Mathf.FloorToInt(timer);
            charIndex = Mathf.Clamp(charIndex, 0, textToType.Length);

            textLabel.text = textToType.Substring(0, charIndex);


            yield return null;
        }

        textLabel.text = textToType;
    }
}
