using System.Timers;
using System.Threading;
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject obstacle;
    public GameObject coins;
    public Transform spawnPoint;
    public Transform spawnPointEarn;

    int score = 0;
    public TextMeshProUGUI scoreText;

    public GameObject playButton;
    public GameObject player;
    public GameObject particleSystems;

    public GameObject playGameButton;
    public GameObject pauseGameButton;

    public float nextActionTime = 0.0f;
    public float period = 0.1f;

    private Firebase.FirebaseApp app;

    // Start is called before the first frame update
    void Start()
    {
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
        var dependencyStatus = task.Result;
        if (dependencyStatus == Firebase.DependencyStatus.Available) {
            // Create and hold a reference to your FirebaseApp,
            // where app is a Firebase.FirebaseApp property of your application class.
            app = Firebase.FirebaseApp.DefaultInstance;

            // Set a flag here to indicate whether Firebase is ready to use by your app.
            Debug.Log("Firebase is working");
        } else {
            UnityEngine.Debug.LogError(System.String.Format(
            "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
            // Firebase Unity SDK is not safe to use here.
        }
        });
    }

    // Update is called once per frame
    void Update()
    {
        // GameSpeedTime ();
    }

    IEnumerator SpawnObstacles() {
         while (true) {
            float waitTime = Random.Range(0.7f, 2.5f);
            yield return new WaitForSeconds(waitTime);
            Instantiate(obstacle, spawnPoint.position, Quaternion.identity);
         }
    }

    IEnumerator SpawnCoins() {
         while (true) {
            float waitTime = Random.Range(0.2f, 3f);
            yield return new WaitForSeconds(waitTime);
            Instantiate(coins, spawnPointEarn.position, Quaternion.identity);
         }
    }

    void ScoreUp() {
        
        score++;
        scoreText.text = score.ToString();
    }

    public void GameStart() {
        player.SetActive(true); 
        particleSystems.SetActive(true);
        playButton.SetActive(false);
        StartCoroutine ("SpawnObstacles");
        StartCoroutine ("SpawnCoins");
        InvokeRepeating("ScoreUp", 2f, 1f);
        coins.SetActive(true);
        pauseGameButton.SetActive(true);

        Time.timeScale = 1;
    }

    public void GameSpeedTime () {
        if (Time.time > nextActionTime ) {
            nextActionTime += period;
            // Time.timeScale += 0.001f;
        }
    }

    public void NewScean () {
        SceneManager.LoadScene("SampleScene");
    }

    public void PauseGame () {
        // Time.timeScale = 0;
        playGameButton.SetActive(true);
        pauseGameButton.SetActive(false);
    }

    public void PlayGame () {
        // Time.timeScale = time;
        pauseGameButton.SetActive(true);
        playGameButton.SetActive(false);
    }
}
