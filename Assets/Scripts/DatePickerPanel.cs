using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class DatePickerPanel : MonoBehaviour
{
    [Header("Calander Refrences")]
    [SerializeField] RectTransform Panel;

    [SerializeField] Button PrevYearBtn;
    [SerializeField] Button PrevMonthBtn;
    [SerializeField] TMP_Text DateText;
    [SerializeField] Button NextMonthBtn;
    [SerializeField] Button NextYearBtn;

    [SerializeField] Button SaveBtn;
    [SerializeField] Button CloseBtn;

    [SerializeField] ToggleGroup DayToggleGroup;
    [SerializeField] GameObject DayContainer;
    DayBtn[] DayBtns;

    UnityAction<DateTime> onPanelSaveAction;
    UnityAction onPanelCloseAction;

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
        SaveBtn.onClick.AddListener(OnSaveBtnClicked);
        CloseBtn.onClick.AddListener(OnCloseBtnClicked);

        PrevYearBtn.onClick.AddListener(OnPrevYearBtnClicked);
        PrevMonthBtn.onClick.AddListener(OnPrevMonthBtnClicked);
        NextMonthBtn.onClick.AddListener(OnNextMonthBtnClicked);
        NextYearBtn.onClick.AddListener(OnNextYearBtnClicked);

        DayBtns = DayContainer.GetComponentsInChildren<DayBtn>();
        for (int i = 0; i < DayBtns.Length; i++)
        {
            DayBtns[i].toggle.group = DayToggleGroup;
            DayBtns[i].toggle.onValueChanged.AddListener(OnDayClicked);
        }
    }
    #region UI Events

    void OnPrevYearBtnClicked()
    {
        SelectedDate = SelectedDate.AddYears(-1);
        UpdateDayUI();
    }
    void OnPrevMonthBtnClicked()
    {
        SelectedDate = SelectedDate.AddMonths(-1);
        UpdateDayUI();
    }
    void OnNextMonthBtnClicked()
    {
        SelectedDate = SelectedDate.AddMonths(1);
        UpdateDayUI();
    }
    void OnNextYearBtnClicked()
    {
        SelectedDate = SelectedDate.AddYears(1);
        UpdateDayUI();
    }
    void OnDayClicked(bool b)
    {
        if (b)
        {
            var date = SelectedDate;
            var firstdateofmonth = new DateTime(date.Year, date.Month, 1);
            var firstdayofmonth = firstdateofmonth.DayOfWeek;
            int dayoffset = -(int)firstdayofmonth;

            int i = 0;
            for (; i < DayBtns.Length; i++)
            {
                if(DayBtns[i].toggle.isOn) break;
            }
            var daySelected = i + dayoffset + 1;

            SelectedDate = SelectedDate.AddDays(daySelected - SelectedDate.Day);
        }
    }
    void OnSaveBtnClicked()
    {
        onPanelSaveAction?.Invoke(selectedDate);

        Panel.gameObject.SetActive(false);
    }
    void OnCloseBtnClicked()
    {
        onPanelCloseAction?.Invoke();

        Panel.gameObject.SetActive(false);
    }

    #endregion
    public void Show(DateTime startDateTime,
        UnityAction<DateTime> onPanelSave = null,
        UnityAction onPanelClose = null)
    {
        SetDate(startDateTime);

        onPanelSaveAction = onPanelSave;
        onPanelCloseAction = onPanelClose;

        Panel.gameObject.SetActive(true);
    }

    public void SetDate(DateTime date)
    {
        SelectedDate = date;

        UpdateDayUI();
    }
    void UpdateDayUI()
    {
        var firstdateofmonth = new DateTime(selectedDate.Year, selectedDate.Month, 1);
        var firstdayofmonth = firstdateofmonth.DayOfWeek;
        int dayoffset = -(int)firstdayofmonth;

        int j = selectedDate.Day - dayoffset - 1;
        DayBtns[j].toggle.SetIsOnWithoutNotify(true);

        for (int i = 0; i < DayBtns.Length; i++, dayoffset++)
        {
            var offsetteddate = firstdateofmonth.AddDays(dayoffset);
            var daynum = offsetteddate.Day;
            var canInteract = offsetteddate.Month == selectedDate.Month;
            DayBtns[i].Set(daynum.ToString(), canInteract);
        }
    }
}