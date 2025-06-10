using System;
using Unity.Tutorials.Core.Editor;
using Unity.VisualScripting;
using UnityEngine;

public enum EAchievementCondition
{
     GoldCollect,
     DronKillCount,
     BossKillCount,
     PlayTime,
     Trigger,
}


public class Achievement
{
    // 최종 목적: 자기서술형

    // 데이터
    public readonly string                ID; 
    public readonly string                Name;
    public readonly string                Description;
    public readonly EAchievementCondition Condition;
    public int                            GoalValue;
    public ECurrencyType                  RewardCurrencyType;
    public int                            RewardAmount;

    // 상태
    private int                           _currentValue;
    public int                            CurrentValue  => _currentValue;

    private bool                          _rewardClaimed;
    public bool                           RewardClaimed => _rewardClaimed;

    // 생성자
    public Achievement(string id, string name, string description, EAchievementCondition condition, int goalValue, ECurrencyType rewardCurrencyType, int rewardAmount)
    {
        if (string.IsNullOrEmpty(id))
        {
            throw new Exception("업적 ID는 비어있을 수 없습니다.");
        }
        if (string.IsNullOrEmpty(name))
        {
            throw new Exception("업적 이름은 비어있을 수 없습니다.");
        }
        if (string.IsNullOrEmpty(description))
        {
            throw new Exception("업적 설명은 비어있을 수 없습니다.");
        }
        if (goalValue <= 0)
        {
            throw new Exception("업적 목표 값은 0보다 커야합니다.");
        }
        if (rewardAmount <= 0)
        {
            throw new Exception("업적 보상 값은 0보다 커야합니다.");
        }

        ID = id;
        Name = name;
        Description = description;
        Condition = condition;
        GoalValue = goalValue;
        RewardCurrencyType = rewardCurrencyType;
        RewardAmount = rewardAmount;
    }

    public Achievement(AchievementSO metadata, AchievementSaveData saveData)
    {
        if (string.IsNullOrEmpty(metadata.ID))
        {
            throw new Exception("업적 ID는 비어있을 수 없습니다.");
        }
        if (string.IsNullOrEmpty(metadata.Name))
        {
            throw new Exception("업적 이름은 비어있을 수 없습니다.");
        }
        if (string.IsNullOrEmpty(metadata.Description))
        {
            throw new Exception("업적 설명은 비어있을 수 없습니다.");
        }
        if (metadata.GoalValue <= 0)
        {
            throw new Exception("업적 목표 값은 0보다 커야합니다.");
        }
        if (metadata.RewardAmount <= 0)
        {
            throw new Exception("업적 보상 값은 0보다 커야합니다.");
        }
        if (saveData.CurrentValue <0)
        {
            throw new Exception("업적 진행 값은 0보다 커야합니다.");
        }

        ID = metadata.ID;
        Name = metadata.Name;
        Description = metadata.Description;
        Condition = metadata.Condition;
        GoalValue = metadata.GoalValue;
        RewardCurrencyType = metadata.RewardCurrencyType;
        RewardAmount = metadata.RewardAmount;

        _currentValue = saveData.CurrentValue;
        _rewardClaimed = saveData.RewardClaimed;
    }

    public void Increase(int value)
    {
        if (value <= 0)
        {
            throw new Exception("증가 값은 0보다 커야합니다.");
        }
          
        _currentValue += value;
    }

    public bool CanClaimReward()
    {
        return _rewardClaimed == false && _currentValue >= GoalValue;
    }

    public bool TryClaimReward()
    {
        if(!CanClaimReward())
        {
            return false;
        }

        _rewardClaimed = true;

        return true;
    }
}
