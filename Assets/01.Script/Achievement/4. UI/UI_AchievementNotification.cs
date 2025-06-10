using System.Collections;
using TMPro;
using UnityEngine;

public class UI_AchievementNotification : MonoBehaviour
{
    public TextMeshProUGUI NameText;
    public TextMeshProUGUI DescriptionText;

    private void Start()
    {
        AchievementManager.Instance.OnNewAchievementRewarded += StartNotification;
        gameObject.SetActive(false);
    }

    private void StartNotification(AchievementDTO achievementDTO)
    {
        NameText.text = achievementDTO.Name;
        DescriptionText.text = achievementDTO.Description;
        gameObject.SetActive(true);

        StartCoroutine(ShowNotificationRoutine());
    }

    private IEnumerator ShowNotificationRoutine()
    {
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }
}
