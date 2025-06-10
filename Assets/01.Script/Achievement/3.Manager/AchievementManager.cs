using System;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    public static AchievementManager    Instance;

    [SerializeField]
    private List<AchievementSO>         _metaDatas;

    private List<Achievement>           _achievements;
    public List<AchievementDTO>         Achievements => _achievements.ConvertAll((a) => new AchievementDTO(a));

    private AchievementRepository       _repository;

    public event Action                 OnDataChanged;
    public event Action<AchievementDTO> OnNewAchievementRewarded;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        Init();
    }

    private void Init()
    {
        // 초기화

        _achievements = new List<Achievement>();

        // 레포지토리
        _repository = new AchievementRepository();

        List<AchievementSaveData> saveDatas = _repository.Load();

        foreach (var metaData in _metaDatas)
        {
            // 중복 검사
            Achievement duplicatedAchievement = FindById(metaData.ID);
            if (duplicatedAchievement != null)
            {
                throw new Exception($"업적 ID({metaData.ID})가 중복됩니다.");
            }

            // 데이터 생성
            AchievementSaveData saveData = saveDatas?.Find(a => a.ID == metaData.ID) ?? new AchievementSaveData();
            Achievement achievement = new Achievement(metaData, saveData);
            _achievements.Add(achievement);
        }
        
        
    }

    private Achievement FindById(string id)
    {
        return _achievements.Find((a) => a.ID == id);
    }

    public void Increase(EAchievementCondition condition, int value)
    {
        foreach (var achievement in _achievements)
        {
            if (achievement.Condition == condition)
            {
                bool prevCanClaimReward = achievement.CanClaimReward();

                achievement.Increase(value);

                bool canClaimReward = achievement.CanClaimReward();
                if (prevCanClaimReward == false && canClaimReward == true)
                {
                    // 이때가 바로 새로운 리워드 보상이 가능할 때
                    OnNewAchievementRewarded?.Invoke(new AchievementDTO(achievement));
                }

                _repository.Save(Achievements);
            }
        }

        OnDataChanged?.Invoke();
    }

    public bool TryClaimReward(AchievementDTO achievementDTO)
    {
        Achievement achievement = FindById(achievementDTO.ID);
        if (achievement == null)
        {
            return false;
        }

        if (achievement.TryClaimReward())
        {
            CurrencyManager.Instance.Add(achievement.RewardCurrencyType, achievement.RewardAmount);

            OnDataChanged?.Invoke();

            _repository.Save(Achievements);

            return true;
        }

        return false;
    }
}
