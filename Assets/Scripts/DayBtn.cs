using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DayBtn : MonoBehaviour
{
    [SerializeField] Toggle DayToggle;
    [SerializeField] TMP_Text DateText;

    public Toggle toggle => DayToggle;
    public void Set(string text, bool active)
    {
        DateText.text = text;
        toggle.interactable = active;
    }
}