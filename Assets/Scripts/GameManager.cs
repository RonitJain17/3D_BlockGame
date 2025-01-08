using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class GameManager : MonoBehaviour
{
    // Score
    public int currentScore;
    public int highScore;
    public int tokenCount;
    private int totalTokenCount;

    // Levels
    public int currentLevel = 1;
    public int unlockedLevel;

    // Gui
    public GUISkin skin;
    public Color warningColor;
    public Color deafaultColor;

    // Timer
    public Rect timerRect;
    public float startTime;
    private string currentTime;

    // References
    public GameObject tokenParent;

    private bool completed = false ;
    public bool showWinScreen = false;
    public int winScreenWidth , winScreenHeight;

    void Update(){
        if(!completed){
            startTime -= Time.deltaTime;
            currentTime = string.Format("{0:0.0}",startTime); 

            if(startTime <=0){
                startTime = 0;
                SceneManager.LoadScene("MainMenu");
            }
        }
    }

    void Start(){
        totalTokenCount = tokenParent.transform.childCount;
        if(PlayerPrefs.GetInt("LevelCompleted")>1){
            currentLevel = PlayerPrefs.GetInt("LevelCompleted");
        }

        else{
            currentLevel = 1;
        }
    }

    public void CompleteLevel(){
        showWinScreen = true; 
        completed = true;       
    }

    void LoadNextlevel(){
        Time.timeScale = 1f;
        if(currentLevel < 3){
            currentLevel += 1;
            SaveGame();
            SceneManager.LoadScene(currentLevel);
        }

        else{
            print("You Win");
        }
    }

    void SaveGame(){
        PlayerPrefs.SetInt("LevelCompleted",currentLevel);
        PlayerPrefs.SetInt("Level" + currentLevel.ToString() + "score",currentScore);
    }

    void OnGUI(){
        GUI.skin = skin;

        if(startTime<5f){
            skin.GetStyle("Timer").normal.textColor = warningColor;
        }
        else{
            skin.GetStyle("Timer").normal.textColor = deafaultColor;
        }
        GUI.Label(timerRect,currentTime,skin.GetStyle("Timer"));
        GUI.Label(new Rect(45,100,200,200), tokenCount.ToString() + "/" + totalTokenCount.ToString());

        if(showWinScreen){
            Rect winScreenRect = new Rect(Screen.width/2-(Screen.width*.5f/2), Screen.height/2-(Screen.height*.5f/2), Screen.width*.5f, Screen.height*.5f);
            GUI.Box(winScreenRect,"You Win");

            int gameTime = (int)startTime;
            currentScore = tokenCount*gameTime; 

        if(GUI.Button(new Rect(winScreenRect.x + winScreenRect.width -170, winScreenRect.y + winScreenRect.height -60,150,40),"Continue")){
            LoadNextlevel(); 
            }    

        if(GUI.Button(new Rect(winScreenRect.x + 20, winScreenRect.y + winScreenRect.height -60,100,40),"Quit")){
             SceneManager.LoadScene("MainMenu");
             Time.timeScale = 1f;
            }  

        GUI.Label(new Rect(winScreenRect.x + 20,winScreenRect.y + 40,300,50) ,"Score : " + currentScore.ToString() );
        GUI.Label(new Rect(winScreenRect.x + 20,winScreenRect.y + 70,300,50) ,"Completed Level : " + currentLevel );           
        }
    }
}
