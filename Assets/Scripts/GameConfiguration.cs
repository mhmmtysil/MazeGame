using System;
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
    public  bool Paused { get; set; }
    private GameObject levelOpened;

    public static GameConfiguration Instance { get; private set; }
    public GameObject ingame;
    public GameObject NextCanvas;
    public GameObject GameOver;
    private int levelNow;
    public Scene Mainmenu;
    public TextMeshProUGUI txtAnswer;
    public TextMeshProUGUI txtTimer;
    private float timer;
    public float Timer { get => timer; set => txtTimer.text = Convert.ToInt32(value).ToString(); }


    public Transform parentWords;
    #region Words
    Word[] words = new Word[]
    {
        new Word("RABBIT", "There are about 50 known species of rabbits!", 90f),
        new Word("HORSE", "Horse Nails are called HOOF", 90f),
        new Word("TURKEY", "There is a country named TURKEY!",90f),
        new Word("KITTEN", "Kittens Love Play with you! :)", 90f),
        new Word("DOGS", "Dogs have been domestic 9000 years ago!!", 90f),
        new Word("DINOSAUR", "If you see a Dinosaur, please see a DOCTOR!", 90f),
        new Word("TIGER", "Tigers are just BIG CATS! Don't try to Hug them!", 90f),
        new Word("MONKEY", "The Biggest Monkeys are Mandrel Monkeys. ", 90f),
        new Word("PENGUIN", "There are no penguins in the Northern Hemisphere, including the North Pole", 90f),
        new Word("SNAKE", "Snakes are legless but they can crawl and move ", 90f),
        new Word("BUTTERFLY", "Butterfly lives more than one day!", 90f),
        new Word("SPIDER", "There are 43.244 known species of Spiders. Wow!", 90f),
        new Word("ELEPHANT", "Elephants can grow up to 6.5 meters. So Tall!!", 90f),
        new Word("ASIA", "Asian name was first used by Heredot.", 90f),
        new Word("EUROPE", "Europe is a Peninsula!", 90f),
        new Word("AFRICA", "Africa contains 24,4 percent of the land in the world", 90f),
        new Word("ANTARCTICA", "Antarctica is the world's refrigerator!", 90f),
        new Word("NORTHAMERICA", "There are 23 countries in North America", 90f),
        new Word("SOUTHAMERICA", "More than 371.000.000 people lives in South America.", 90f),
        new Word("AUSTRALIA", "The best known animal in Australia is kangaroo!!", 90f),
        new Word("", "", 90f),
        new Word("", ""),
        new Word("", ""),
        new Word("", ""),
        new Word("", ""),
        new Word("", ""),
        new Word("", ""),
        new Word("", ""),
        new Word("", ""),
        new Word("", ""),
        new Word("", ""),
        new Word("", ""),
        new Word("", ""),
        new Word("", ""),
        new Word("", ""),
        new Word("", ""),
        new Word("", ""),
        new Word("", ""),
        new Word("", ""),
        new Word("", ""),
        new Word("", ""),
        new Word("", ""),
        new Word("", ""),
        new Word("", ""),
        new Word("", ""),
        new Word("", ""),
        new Word("", ""),
        new Word("", ""),
        new Word("", ""),
        new Word("", ""),
    };
    #endregion
    [HideInInspector]
    public List<GameObject> letters;
    public List<GameObject> lettersPrefabs;

    private GameObject[] lettersPos;


    void Start()
    {
        if (PlayerPrefs.HasKey("Level"))
        {
            levelNow = PlayerPrefs.GetInt("Level");
        }
        else
        {
            levelNow = 0;
            PlayerPrefs.SetInt("Level", levelNow);
        }
        letters = new List<GameObject>();
        lettersPrefabs = new List<GameObject>();
        LevelOpened();
    }
    void OnDestroy()
    {
        PlayerPrefs.SetInt("Level", levelNow);
    }
    private void Initialize()
    {
        ingame.SetActive(true);
        NextCanvas.SetActive(false);
        GameOver.SetActive(false);
        foreach (GameObject g in letters)
        {
            Destroy(g);
        }
        // player level has key
        letters.Clear();
        foreach (GameObject g in lettersPrefabs)
        {
            Destroy(g);
        }
        lettersPrefabs.Clear();
        RectTransform rT = parentWords.GetComponent<RectTransform>();
        rT.sizeDelta = new Vector2(0, rT.sizeDelta.y);
        lettersPos = GameObject.FindGameObjectsWithTag("Letters");
        float xOffset = 0;
        for(int i = 0; i < words[levelNow].Name.Length; i++)
        {
            // UI field
            GameObject obj = Instantiate(Resources.Load<GameObject>("Letter"), parentWords);
            rT = parentWords.GetComponent<RectTransform>();
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
            GameObject obj2 = Instantiate(lt, lettersPos[i].transform.position, lt.transform.rotation);
            obj2.GetComponent<Solution>().index = i;
            lettersPrefabs.Add(obj2);
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

    void Update()
    {
        if (Paused)
        {
            return;
        }
        timer -= Time.deltaTime;
        Timer = timer;
        if (timer <= 0)
        {
            TimeIsUp();
            timer = 1000;
        }
    }

    public void TimeIsUp()
    {
        SoundConfiguration.Instance.PlayGameOverSound();
        GameOver.SetActive(true);
        ingame.SetActive(false);
        NextCanvas.SetActive(false);

    } 
    public void StageCompleted()
    {
        SoundConfiguration.Instance.PlayEndStageSound();
        ingame.SetActive(false);
        GameOver.SetActive(false);
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
        timer = words[levelNow].Timer;
        levelOpened = Instantiate(Resources.Load<GameObject>("Levels/" + levelNow));
        Initialize();
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
