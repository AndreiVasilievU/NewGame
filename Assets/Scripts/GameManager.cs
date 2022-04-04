using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Newtonsoft.Json;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    [SerializeField] private Image healthImage;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text levelText;
    [SerializeField] private GameObject inventory;
    [SerializeField] private int maxCoins;
    [SerializeField] private List<GameObject> coinsList;
    [SerializeField] private Transform player;
    
    private Inventory inv;
    private int score;
    private int lvlScore;
    private MyData myData;
    void Start()
    {
        inv = inventory.GetComponent<Inventory>();
        
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        Instance = this;

        if (PlayerPrefs.GetInt(Ids.LOAD_SAVE) == 0)
        {
            healthImage.fillAmount = 1;
            scoreText.text = "SCORE 0";
        }
        else if (PlayerPrefs.GetInt(Ids.LOAD_SAVE) == 1)
        {
            MyData myData2 = new MyData();
            
            string sd = File.ReadAllText(Application.persistentDataPath + Path.PathSeparator + "playerData.json");
            myData2 = JsonConvert.DeserializeObject<MyData>(sd);

            healthImage.fillAmount = myData2.health;
            player.position = new Vector3(myData2.positionX, myData2.positionY, myData2.positionZ);
            score = myData2.score;
            lvlScore = myData2.lvlScore;
            scoreText.text = $"SCORE {score}";
            AddCoinOnLoad(myData2.lvlScore);
            
            for (int i = 0; i < myData2.coins.Length; i++)
            {
                if (myData2.coins[i] == 1)
                {
                    coinsList[i].SetActive(true);
                }
                else
                {
                    coinsList[i].SetActive(false);
                }
            }
        }
        else
        {
            MyData myData3 = new MyData();
            
            string sd = File.ReadAllText(Application.persistentDataPath + Path.PathSeparator + "playerData.json");
            myData3 = JsonConvert.DeserializeObject<MyData>(sd);
            
            healthImage.fillAmount = myData3.health;
            score = myData3.score;
            scoreText.text = $"SCORE {score}";
        }
        
        if (SceneManager.GetActiveScene().name == Ids.LEVEL_1)
        {
            levelText.text = "LEVEL 1";
        }
        else
        {
            levelText.text = "LEVEL 2";
        }

        myData = new MyData();
    }

    public void TakeDamage(float _damage)
    {
        healthImage.fillAmount -= _damage;
        if (healthImage.fillAmount <= 0)
        {
            SceneManager.LoadScene(Ids.MENU);
        }
    }

    public void AddCoin()
    {
        inv.AddToInventory();
        score += 1;
        lvlScore += 1;
        scoreText.text = $"SCORE {score}";
        
        if (lvlScore == maxCoins)
        {
            if (SceneManager.GetActiveScene().name == Ids.LEVEL_1)
            {
                EndLevelSave();
                SceneManager.LoadScene(Ids.LEVEL_2);
            }
            else
            {
                EndLevelSave();
                SceneManager.LoadScene(Ids.LEVEL_1);
            }
        }
    }

    public void AddCoinOnLoad(int lvlScore)
    {
        for (int i = 0; i < lvlScore; i++)
        {
            inv.AddToInventory();
        }
    }

    public void OnSaveButtonPressed()
    {
        myData.health = healthImage.fillAmount;
        myData.positionX = player.position.x;
        myData.positionY = player.position.y;
        myData.positionZ = player.position.z;
        myData.levelName = SceneManager.GetActiveScene().name;
        myData.score = score;
        myData.lvlScore = lvlScore;

        myData.coins = new int[coinsList.Count];
        
        for (int i = 0; i < coinsList.Count; i++)
        {
            if (coinsList[i].activeSelf == true)
            {
                myData.coins[i] = 1;
            }
            else
            {
                myData.coins[i] = 0;
            }
        }

        string serializedData = JsonConvert.SerializeObject(myData);
        File.WriteAllText(Application.persistentDataPath + Path.PathSeparator + "playerData.json", serializedData);
    }

    public void EndLevelSave()
    {
        PlayerPrefs.SetInt(Ids.LOAD_SAVE, 2);
        
        myData.health = healthImage.fillAmount;
        myData.score = score;
        
        string serializedData = JsonConvert.SerializeObject(myData);
        File.WriteAllText(Application.persistentDataPath + Path.PathSeparator + "playerData.json", serializedData);
    }
}
