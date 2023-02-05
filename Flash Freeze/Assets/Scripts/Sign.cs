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
    //[SerializeField] private GameObject openText;
    //[SerializeField] private GameObject closeText;

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
                

                //openText.SetActive(true);
                //closeText.SetActive(false);
                
            }
            else
            {
                dialogBox.SetActive(true);
                dialogText.text = dialog;

                //openText.SetActive(false);
                //closeText.SetActive(true);
            }
        }



        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            //openText.SetActive(true);

            playerInRange = true;

            spriteRenderer.color = Color.red;

            //openText.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            playerInRange = false;

            spriteRenderer.color = Color.white;

            //openText.SetActive(false);

            //openText.SetActive(false);
            //closeText.SetActive(false);
        }
    }
}
