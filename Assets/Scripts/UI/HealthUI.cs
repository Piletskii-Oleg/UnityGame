using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private HealthData healthData;

    [SerializeField] private float chipSpeed;
    private float timePassed;

    [SerializeField] private Image frontHealthBar;
    [SerializeField] private Image backHealthBar;

    private Coroutine updateCoroutine;

    public void UpdateUI()
    {
        if (updateCoroutine != null)
        {
            StopCoroutine(updateCoroutine);
        }

        updateCoroutine = StartCoroutine(UpdateHealthUI());
    }

    private IEnumerator UpdateHealthUI()
    {
        float fillFrontAtStart = frontHealthBar.fillAmount;
        float fillBackAtStart = backHealthBar.fillAmount;

        float healthRatio = healthData.currentHealth / healthData.maxHealth;

        if (fillBackAtStart > healthRatio)
        {
            frontHealthBar.fillAmount = healthRatio;
            backHealthBar.color = Color.red;

            while (Mathf.Abs(backHealthBar.fillAmount - healthRatio) > Mathf.Epsilon)
            {
                FillHealthBar(backHealthBar, fillBackAtStart, healthRatio);
                yield return new WaitForEndOfFrame();
            }
        }
        else if (fillFrontAtStart < healthRatio)
        {
            backHealthBar.fillAmount = healthRatio;
            backHealthBar.color = Color.green;

            while (Mathf.Abs(frontHealthBar.fillAmount - healthRatio) > Mathf.Epsilon)
            {
                FillHealthBar(frontHealthBar, fillFrontAtStart, healthRatio);
                yield return new WaitForEndOfFrame();
            }
        }

        timePassed = 0;
    }

    private void FillHealthBar(Image healthBar, float startingValue, float currentValue)
    {
        timePassed += Time.deltaTime;

        float addPercent = timePassed / chipSpeed;
        addPercent *= addPercent;

        healthBar.fillAmount = Mathf.Lerp(startingValue, currentValue, addPercent);
    }
}
