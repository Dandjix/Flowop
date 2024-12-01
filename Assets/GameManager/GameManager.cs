using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private float hBound, vBound;
    public static GameManager Instance;
    
    public float startTime;
    public static float timeElapsed;
    
    
    void Start()
    {
    startTime = Time.time;

        
    }


     private void Awake()
    {
        // Assurez-vous qu'il n'y a qu'une seule instance du GameManager
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optionnel, si vous voulez que GameManager persiste entre les scènes
        }
        else
        {
            Destroy(gameObject);
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Restart();
            //  timeElapsed = Time.time - startTime;
            //  SceneManager.LoadScene("Winner");
        }
        Vector2 joueurPos = transform.position;
        if (Mathf.Abs(joueurPos.x) > hBound || Mathf.Abs(joueurPos.y) > vBound)
        {
            Restart();
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GameOver()
    {
        timeElapsed = Time.time - startTime;
        SceneManager.LoadScene("Winner");
    }
}
