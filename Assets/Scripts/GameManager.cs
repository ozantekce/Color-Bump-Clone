using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{


    private static GameManager instance;


    private Text currentLevelText, nextLevelText;
    private Image fill;

    private int level;
    private float startDistance, distance;

    private GameObject player, finish, hand;
    private TextMesh levelNo;


    public GameObject breakablePlayer;
    public static GameManager Instance { get => instance; }

    void Awake()
    {
        instance = this;
        currentLevelText = GameObject.Find("CurrentLevelText").GetComponent<Text>();
        nextLevelText = GameObject.Find("NextLevelText").GetComponent<Text>();

        fill = GameObject.Find("Fill").GetComponent<Image>();

        player = FindObjectOfType<PlayerController>().gameObject;

        finish = GameObject.Find("Finish");

        hand = GameObject.Find("Hand");


    }


    private void Start()
    {

        level = PlayerPrefs.GetInt("Level",1);

        currentLevelText.text = level.ToString();
        nextLevelText.text = (level+1).ToString();

        startDistance = finish.transform.position.z - player.transform.position.z;


    }


    private void Update()
    {
        distance = finish.transform.position.z - player.transform.position.z;
        fill.fillAmount = 1 - (distance/startDistance);

    }


    public void RemoveUI()
    {
        hand.SetActive(false);

    }



}
