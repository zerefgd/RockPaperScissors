using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Choices { ROCK, PAPER, SCISSORS, NONE }

public class GameManager : MonoBehaviour
{

    private const string draw = "The game is a draw";
    private const string win = " Wins!";

    private string playerName;
    private string OpponentName;

    Choices playerChoice = Choices.NONE, opponentChoice = Choices.NONE;
    bool hasPlayerSelected, hasOpponentSelected;

    public static GameManager instance;

    [SerializeField]
    private Text winningMessage;

    [SerializeField]
    Animator playerAnimator, opponentAnimator, playerSelectAnimator, oppoentSelectAnimator;

    [SerializeField]
    Image playerChoiceImage, OpponentChoiceImage;

    [SerializeField]
    Sprite rock,paper,scissors;

    [SerializeField]
    GameObject settingsBG;

    [SerializeField]
    Text playerNameText, opponentNameText;

    [SerializeField]
    Toggle toggleAI;
    

    private void Awake()
    {
        instance = this;
        winningMessage.text = "";
        SetPlayerName(GameData.data.playerName);
        SetOpponentName(GameData.data.OpponentName);
        toggleAI.isOn = GameData.data.isOpponentAI;
        playerNameText.text = playerName;
        opponentNameText.text = OpponentName;
    }

    public void Select(bool isPlayer, Choices choice)
    {
        if (isPlayer)
        {
            playerChoice = choice;
            hasPlayerSelected = true;
            if(GameData.data.isOpponentAI)
            {
                Select(false, (Choices)Random.Range(0,3));
            }
        }
        else
        {
            opponentChoice = choice;
            hasOpponentSelected = true;
        }
        if(hasPlayerSelected && hasOpponentSelected)
        {
            DetermineWinner();
        }
    }

    void DetermineWinner()
    {
        SetPlayerName(GameData.data.playerName);
        SetOpponentName(GameData.data.OpponentName);
        if (playerChoice == opponentChoice)
        {
            winningMessage.text = draw;
        }
        else if (playerChoice == Choices.ROCK && opponentChoice == Choices.SCISSORS)
        {
            winningMessage.text =  playerName + win;
        }
        else if (playerChoice == Choices.PAPER && opponentChoice == Choices.ROCK)
        {
            winningMessage.text = playerName + win;
        }
        else if (playerChoice == Choices.SCISSORS && opponentChoice == Choices.PAPER)
        {
            winningMessage.text = playerName +  win;
        }
        else if (playerChoice == Choices.PAPER && opponentChoice == Choices.SCISSORS)
        {
            winningMessage.text = OpponentName + win;
        }
        else if (playerChoice == Choices.SCISSORS && opponentChoice == Choices.ROCK)
        {
            winningMessage.text = OpponentName + win;
        }
        else if (playerChoice == Choices.ROCK && opponentChoice == Choices.PAPER)
        {
            winningMessage.text = OpponentName + win;
        }
        SetAnimations();
    }

    void SetAnimations()
    {
        playerAnimator.Play("Animation_PlayerChoiceSelect");
        opponentAnimator.Play("Animation_OpponentChoiceSelect");

        SetImage();
        
        playerSelectAnimator.Play("Animation_PlayerSelectedImage");
        oppoentSelectAnimator.Play("Animation_OpponentSelectedImage");
    }

    void SetImage()
    {
        SetImage(playerChoiceImage, playerChoice);
        SetImage(OpponentChoiceImage, opponentChoice);
    }

    void SetImage(Image image, Choices choice)
    {
        switch (choice)
        {
            case Choices.ROCK:
                image.sprite = rock;
                break;
            case Choices.PAPER:
                image.sprite = paper;
                break;
            case Choices.SCISSORS:
                image.sprite = scissors;
                break;
            default:
                break;
        }
            
    }

    public void BackToMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void Restart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void Settings()
    {
        playerNameText.text = playerName;
        opponentNameText.text = OpponentName;
        settingsBG.SetActive(true);
    }

    public void SetPlayerName(string name)
    {
        playerName = name;
        GameData.data.playerName = name;
    }

    public void SetOpponentName(string name)
    {
        OpponentName = name;
        GameData.data.OpponentName = name;
    }

    public void ReturnFromSettings()
    {
        settingsBG.SetActive(false);
    }

    public void ChangeAI(bool isAI)
    {
        GameData.data.isOpponentAI = isAI;
    }
}


