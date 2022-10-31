using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevelTracker : MonoBehaviour
{
    [Header("Base info")]
    [SerializeField, Tooltip("The name of the character")] private StringVariable nameOfPlayer;
    [SerializeField, Tooltip("The initial player level")] private IntVariable playerStartLevel;

    [Space(2f), Header("Console messages")]
    [SerializeField, Tooltip("Level up message. ^ to player name, # current num"), Multiline] private string levelUpMsg = "^ reached level #!";
    [SerializeField, Tooltip("EXP gain message.  ^ to player name, # current num, ¤ future num"), Multiline] private string expGainMsg = "^ has # out of ¤ EXP";

    private int currentLevel;
    private int currentEXP = 0;
    private int expTilLevelUp = 1;
    private int otherEXPTrackerForFibonacci = 0;

    // Start is called before the first frame update
    void Start()
    {
        currentLevel = playerStartLevel.Value;
    }

    /// <summary>
    /// Gain 1 EXP
    /// </summary>
    public void GainEXP()
    {
        GainEXP(1);
    }

    /// <summary>
    /// Gain an amount of EXP
    /// </summary>
    /// <param name="expToGain"></param>
    public void GainEXP(int expToGain)
    {
        // Adds 1 XP
        currentEXP += expToGain;
        if (currentEXP >= expTilLevelUp)
        {
            // Levels up
            LevelUp();
        }
        else
        {
            // Makes a variable to store an editable version of the expGainMsg
            string expStringToPrint = expGainMsg;

            // Replaces ^ with the player name
            expStringToPrint = ReplaceCharWithString(expStringToPrint, '^', nameOfPlayer.Value);
            // Replaces # with the current EXP amount
            expStringToPrint = ReplaceCharWithString(expStringToPrint, '#', currentEXP.ToString());
            // Replaces ¤ with the EXP amount needed to level up
            expStringToPrint = ReplaceCharWithString(expStringToPrint, '¤', expTilLevelUp.ToString());

            // Logs the edited message to the console
            Debug.Log(expStringToPrint);
        }
    }

    /// <summary>
    /// Levels up the character, and sets EXP for the new level to 0
    /// </summary>
    public void LevelUp()
    {
        // Increase the current level by 1
        currentLevel++;

        // Lowers current EXP by the amount needed to level up if the player has more EXP than needed to level, otherwise set it to 0
        currentEXP = (currentEXP > expTilLevelUp) ? currentEXP - expTilLevelUp : 0;

        // Does fibonacci to increase the amount of EXP needed to level up
        expTilLevelUp += otherEXPTrackerForFibonacci;
        otherEXPTrackerForFibonacci = expTilLevelUp - otherEXPTrackerForFibonacci;

        // Makes a variable to store an editable version of the levelUpMsg
        string levelStringToPrint = levelUpMsg;

        // Replaces ^ with the player name
        levelStringToPrint = ReplaceCharWithString(levelStringToPrint, '^', nameOfPlayer.Value);
        // Replaces # with the current level
        levelStringToPrint = ReplaceCharWithString(levelStringToPrint, '#', currentLevel.ToString());

        // Logs the edited message to the console
        Debug.Log(levelStringToPrint);

        // If there's still enough EXP to level up, call this again
        if (currentEXP >= expTilLevelUp)
        {
            LevelUp();
        }
    }

    /// <summary>
    /// Takes a string, replaces all instances of a certain character with another string
    /// </summary>
    /// <param name="wholeString"></param>
    /// <param name="toBeReplaced"></param>
    /// <param name="toReplace"></param>
    /// <returns></returns>
    private string ReplaceCharWithString(string wholeString, char toBeReplaced, string toReplace)
    {
        string newSentence = "";

        // Goes through every character in the initial string
        for (int i = 0; i < wholeString.Length; i++)
        {
            // If the character is the one to be replaced, replace it
            if (wholeString[i] == toBeReplaced)
            {
                newSentence += toReplace;
            }
            else
            {
                // Otherwise remember the current character
                newSentence += wholeString[i];
            }
        }

        return newSentence;
    }
}
