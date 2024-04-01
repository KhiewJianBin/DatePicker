using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DatePicker : MonoBehaviour
{
    [SerializeField] TMP_Text DateText;
    [SerializeField] Button SelectDateBtn;
    [SerializeField] DatePickerPanel DatePickerPanel;

    DateTime selectedDate;
    DateTime SelectedDate
    {
        get
        {
            return selectedDate;
        }
        set
        {
            selectedDate = value;
            DateText.text = value.ToLongDateString();
        }
    }

    void Awake()
    {
        SelectDateBtn.onClick.AddListener(OnSelectDateBtnClicked);
        SelectedDate = DateTime.UtcNow;
    }

    #region UI Events
    void OnSelectDateBtnClicked()
    {
        DatePickerPanel.Show(selectedDate,ChangeSelectedDate);
    }
    #endregion

    void ChangeSelectedDate(DateTime newDate)
    {
        SelectedDate = newDate;
    }
}