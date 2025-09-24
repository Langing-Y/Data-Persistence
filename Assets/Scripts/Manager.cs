using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;





#if UNITY_EDITOR
using UnityEditor;
#endif


public class Manager : MonoBehaviour
{
    private TextMeshProUGUI inputText;
    private string textName= "Text_Name";

    private Button startButton;
    private string startBName= "Button_Start";
    
    private Button exitButton;
    private string exitBName = "Button_Quit";


    public static Manager Instance;

    public string playerName;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        

        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene,LoadSceneMode mode)
    {
        Debug.Log($"场景 {scene.name} 加载完成，重新绑定UI引用");
        StartCoroutine(ReBindUI());
    }

    IEnumerator ReBindUI()
    {
        yield return null;

        startButton = GameObject.Find(startBName).GetComponent<Button>();
        exitButton = GameObject.Find(exitBName).GetComponent<Button>();
        inputText = GameObject.Find(textName).GetComponent<TextMeshProUGUI>();
        if (startButton == null)
        {
            Debug.Log("missing 1");
        }

        if (exitButton == null)
        {
            Debug.Log("missing 2");
        }

        if (inputText == null)
        {
            Debug.Log("missing 3");
        }

        startButton.onClick.AddListener(StartGame);
        exitButton.onClick.AddListener(QuitGame);
    }

    public void StartGame()
    {
        Debug.Log("start");
        playerName=inputText.text;
        Debug.Log("player:" + playerName);
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Debug.Log("quit");
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
