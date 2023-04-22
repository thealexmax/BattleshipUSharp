
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class FireButton : UdonSharpBehaviour
{

    public GameObject table;
    public GameObject lettersBtnParent;
    public GameObject numbersBtnParent;

    // This should be the enemy's table.
    // as of now, for testing, it's going to be my own table.
    private TableObject tableScript;

    // It will default to A1 if the user doesn't imput anything.
    private char letter = 'A';
    private int number = 1;

    void Start()
    {
        tableScript = table.GetComponent<TableObject>();
    }

    private void Interact() 
    {
        updateCoords();
        Debug.Log("Target Letter is: " + letter);
        Debug.Log("Target Number is: " + number);
        fireAtCoord(letter, number);
    }

    private void updateCoords()
    {
        foreach (Transform letterChild in lettersBtnParent.transform) 
        {
            //Debug.Log(letterChild.name.ToCharArray()[0]);
            SelectButton btn = letterChild.gameObject.GetComponent<SelectButton>();
            if (btn.isSelected()) 
            {
                letter = letterChild.name.ToCharArray()[0];
                break;
            }
        }

        foreach (Transform numberChild in numbersBtnParent.transform) 
        {
            SelectButton btn = numberChild.GetComponent<SelectButton>();
            if (btn.isSelected())
            {
                number = int.Parse(numberChild.name);
                break;
            }
        }
    }

    private void fireAtCoord(char letter, int number)
    {
        bool tileStatus = tableScript.isTileOccupied(number, letter);
        if (tileStatus) 
        {
            Debug.Log("Tile is Busy, Shooting tile");
            tableScript.shootShip(number, letter);
        }
    }
}
