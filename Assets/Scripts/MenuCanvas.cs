using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuCanvas : MonoBehaviour
{
    public void OnStartGamePressed()
    {
        PlayerPrefs.SetInt(Ids.LOAD_SAVE, 0);
        SceneManager.LoadScene(Ids.LEVEL_1);
    }

    public void OnLoadGamePressed()
    {
        if (PlayerPrefs.HasKey(Ids.LOAD_SAVE) == null)
        {
            SceneManager.LoadScene(Ids.LEVEL_1);
        }
        else
        {
            PlayerPrefs.SetInt(Ids.LOAD_SAVE, 1);

            MyData myData = new MyData();
            string sd = File.ReadAllText(Application.persistentDataPath + Path.PathSeparator + "playerData.json");
            myData = JsonConvert.DeserializeObject<MyData>(sd);

            SceneManager.LoadScene(myData.levelName);
        }
    }
}
