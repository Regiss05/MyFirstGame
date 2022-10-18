using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    public float jumpForce;
    bool canJump;    

    int coinsCollection = 0; 
    public TextMeshProUGUI scoreCoinsText;

    public GameObject retryButton;
    // public GameObject pauserGame = pauseGameButton;
    public GameObject pauseButt;

    private void Awake() 
    {
        rb = GetComponent<Rigidbody>();    
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && canJump)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            // Time.timeScale = 1;
        }
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Ground") {
            canJump = true;
        }
    }

    private void OnCollisionExit(Collision collision) {
       if (collision.gameObject.tag == "Ground") {
            canJump = false;
        } 
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Obstacle") {
            
            // Time.timeScale = 0.05f;
            gameObject.SetActive(false);
            retryButton.SetActive(true);
            pauseButt.SetActive(false);
        }

        if (other.gameObject.tag == "Stars") {
            coinsCollection = coinsCollection + 8;
            scoreCoinsText.text = coinsCollection.ToString();
        }
    }
}
