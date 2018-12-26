using UnityEngine;
using UnityEngine.UI;

public class SudokuEntryView : MonoBehaviour
{
    [SerializeField] private Text _numberText;

    public void SetNumber(int number)
    {
        if (number == 0)
        {
            _numberText.text = "";
        }
        else
        {
            _numberText.text = "" + number;
        }
    }
}