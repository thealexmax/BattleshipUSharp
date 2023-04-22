
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class FireButton : UdonSharpBehaviour
{

    public GameObject table;
    public GameObject lettersBtnParent;
    public GameObject numbersBtnParent;

    private TableObject tableScript;

    [SerializeField] private char letter;
    [SerializeField] private int number;

    void Start()
    {
        tableScript = table.GetComponent<TableObject>();
    }

    private void Interact() 
    {
        updateCoords();
        Debug.Log(letter);
        //fireAtCoord(letter, number);
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
    }

    private void fireAtCoord(char letter, int number)
    {
        bool v = tableScript.isTileOccupied(number, letter);
        if (v) 
        {
            Debug.Log("Tile is Busy");
        }
    }
}
