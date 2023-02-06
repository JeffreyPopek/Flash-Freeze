using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Sign : MonoBehaviour
{
    [SerializeField] private GameObject dialogBox;
    [SerializeField] private TMP_Text dialogText;
    [SerializeField] private string dialog;
    [SerializeField] private bool playerInRange;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && playerInRange) 
        {
            if(dialogBox.activeInHierarchy)
            {
                dialogBox.SetActive(false);      
            }
            else
            {
                dialogBox.SetActive(true);
                dialogText.text = dialog;
            }
        }   
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            playerInRange = true;

            dialogBox.SetActive(true);
            //spriteRenderer.color = Color.red;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            playerInRange = false;

            spriteRenderer.color = Color.white;

            dialogBox.SetActive(false);
        }
    }
}
