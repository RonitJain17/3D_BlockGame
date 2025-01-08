using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public int levelToLoad;
    public GameObject padLock;
    private string loadPromt;
    private bool inRange;
    private bool canLoadLevel;
    private int completedLevel;

    void Start(){
        completedLevel = PlayerPrefs.GetInt("Level Completed");

        if(completedLevel ==0){
            completedLevel = 1;
        }

        canLoadLevel =  levelToLoad <= completedLevel ? true : false;

        if(!canLoadLevel){
            Instantiate(padLock, new Vector3(transform.position.x + 2f, 0f, transform.position.z), Quaternion.identity);
        }
    }

    void Update(){
        if(canLoadLevel && Input.GetKeyDown(KeyCode.Space) && inRange){
            SceneManager.LoadScene("Level_" + levelToLoad.ToString());
        }
    }

    void OnTriggerStay(Collider other){
        inRange = true;
        if(canLoadLevel){
            loadPromt = "Space To Load Level " + levelToLoad.ToString();
        }

        else{
            loadPromt = "Level " + levelToLoad.ToString() + " Is Locked";
        }
    }

    void OnTriggerExit(){
        inRange = false;
        loadPromt = "";
    }

    void OnGUI(){
        GUI.Label(new Rect(30,Screen.height * .9f,200,40),loadPromt);
    }

    
}
