using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Random = System.Random;
public class GameConfiguration : MonoBehaviour
{

    private GameObject levelOpened;

    public static GameConfiguration Instance { get; private set; }
    public GameObject ingame;
    public GameObject NextCanvas;
    private int levelNow;
    public Scene Mainmenu;
    public TextMeshProUGUI txtAnswer;

    public Transform parentWords;
    #region Words
    Word[] words = new Word[]
    {
        new Word("MUHAMMET", "Desc"),
        new Word("BAYRAM", "Desc")
    };
    #endregion
    [HideInInspector]
    public List<GameObject> letters;

    private GameObject[] lettersPos;


    void Start()
    {
        letters = new List<GameObject>();
        Initialize();
    }
    void OnDestroy()
    {
        PlayerPrefs.SetInt("Level", levelNow);
    }
    private void Initialize()
    {
        // player level has key
        if (PlayerPrefs.HasKey("Level"))
        {
            levelNow = PlayerPrefs.GetInt("Level");
        }
        else
        {
            levelNow = 0;
            PlayerPrefs.SetInt("Level", levelNow);
        }
        LevelOpened();

        lettersPos = GameObject.FindGameObjectsWithTag("Letters");
        int _index = 0;
        float xOffset = 0;
        for(int i = 0; i < words[levelNow].Name.Length; i++)
        {
            // UI field
            GameObject obj = Instantiate(Resources.Load<GameObject>("Letter"), parentWords);
            RectTransform rT = parentWords.GetComponent<RectTransform>();
            rT.sizeDelta = new Vector2(rT.sizeDelta.x + obj.GetComponent<RectTransform>().sizeDelta.x + 10, rT.sizeDelta.y);
            rT.anchoredPosition = new Vector2(0, rT.anchoredPosition.y);
            obj.GetComponent<Letter>().txtLetter.text = words[levelNow].Name[i].ToString();
            rT = obj.GetComponent<RectTransform>();
            rT.anchoredPosition = new Vector2(xOffset, rT.anchoredPosition.y);
            xOffset += 160;
            letters.Add(obj);
            obj.SetActive(false);
            // UI field end

            
            GameObject lt = Resources.Load<GameObject>("Letters/" + words[levelNow].Name[i]);
            GameObject obj2 = Instantiate(lt, lettersPos[_index].transform.position, lt.transform.rotation);
            obj2.GetComponent<Solution>().index = _index++;
            // GameObject field end
        }
    }

    void Awake()
    {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void StageCompleted()
    {
        ingame.SetActive(false);
        NextCanvas.SetActive(true);
        txtAnswer.text = "Correct !The Answer is :" + words[levelNow].Name + "\n" + words[levelNow].Description;
    }
    public void NextLevel() {
        levelNow += 1;
        LevelOpened();
    }
    
    private void LevelOpened()
    {
        if(levelOpened != null)
        {
            Destroy(levelOpened);
            levelOpened = null;
        }
        levelOpened = Instantiate(Resources.Load<GameObject>("Levels/" + levelNow));
    }

    public void RestartLevel() {
        LevelOpened();
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("Mainmenu");
    }
    public void LastLevelOpen()
    {
        
    }
   
    public static void PlayVoice() {
        AudioListener.volume = 1f;
    }
    public static void Mute() {
        AudioListener.volume = 0f;
    }
}
