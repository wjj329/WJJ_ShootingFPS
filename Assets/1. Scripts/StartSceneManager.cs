using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class StartSceneManager : MonoBehaviour
{
    public void OnClickStart() 
    {

        SceneManager.LoadScene("2. GameScene"); 
    }
}



