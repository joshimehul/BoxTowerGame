using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayComtroller : MonoBehaviour
{

    public static GamePlayComtroller instance;
    public BoxSpawner box_Spawner; //reference to the BoxS{awner script 
    [HideInInspector]  //dont want to be visible is the inspector
    public BoxScript currentBox;  // reference to the boxscript
    public CameraFolllow cameraScript;
    private int moveCount;

    // Start is called before the first frame updat

    void Awake()
    {
        if (instance == null)
        {
            instance = this; //this stands for the object of the current class (singletence)
        }
    }
    void Start()
    {
        box_Spawner.SpawnBox(); //calling the method
    }

    // Update is called once per frame
    void Update()
    {
        DetectInput();
    }
    //for the detecting the input 
    void DetectInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            currentBox.DropBox();
        }
    }

    public void SpawnNewBox()
    {
        Invoke("NewBox", 0.8f);
    }
    void NewBox()
    {
        box_Spawner.SpawnBox(); //spwniong a new box;
    }
    public void MoveCamera()
    {
        moveCount++;
        if(moveCount==3)
        {
            moveCount = 0; //to reset it 
            cameraScript.targetPos.y += 2f;
        }
    }
    public void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

}
