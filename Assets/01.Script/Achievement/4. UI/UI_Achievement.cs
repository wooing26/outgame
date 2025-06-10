using System;
using System.Collections.Generic;
using UnityEngine;

public class UI_Achievement : MonoBehaviour
{
    [Header("UI_AchievementSlot Prefab")]
    [SerializeField]
    private GameObject               _uiAchievementSlotPrefab;

    [Header("橇府普 积己 何葛")]
    [SerializeField]
    private Transform                _content;

    private List<UI_AchievementSlot> _slots;

    private void Start()
    {
        Init();
        Refresh();

        AchievementManager.Instance.OnDataChanged += Refresh;
    }

    private void Init()
    {
        List<AchievementDTO> achievements = AchievementManager.Instance.Achievements;
        int count = achievements.Count;
        _slots = new List<UI_AchievementSlot>(count);

        Resize(count);
    }

    private void Resize(int dataCount)
    {
        for (int i = 0; i < _content.childCount; i++)
        {
            UI_AchievementSlot achievementSlot = _content.GetChild(i).GetComponent<UI_AchievementSlot>();
            if (achievementSlot == null)
            {
                Destroy(achievementSlot.gameObject);
                continue;
            }

            _slots.Add(achievementSlot);
        }

        int difference = dataCount - _slots.Count;
        if (difference > 0)
        {
            AddSlot(difference);
        }
        else if (difference < 0)
        {
            RemoveSlot(-difference);
        }
    }

    private void AddSlot(int slotCount)
    {
        for (int i = 0; i < slotCount; i++)
        {
            GameObject slotObject = Instantiate(_uiAchievementSlotPrefab, _content);
            UI_AchievementSlot achievementSlot = slotObject.GetComponent<UI_AchievementSlot>();
            if (achievementSlot == null)
            {
                return;
            }
            _slots.Add(achievementSlot);
        }
    }

    private void RemoveSlot(int slotCount)
    {
        int prevCount = _slots.Count;
        for (int i = 0; i < slotCount; i++)
        {
            Destroy(_slots[prevCount - i - 1].gameObject);
        }
    }

    private void Refresh()
    {
        List<AchievementDTO> achievements = AchievementManager.Instance.Achievements;

        for (int i = 0; i < achievements.Count; i++)
        {
            _slots[i].Refresh(achievements[i]);
        }
    }
}
