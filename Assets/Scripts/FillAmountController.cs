using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FillAmountController : MonoBehaviour
{
    public Image targetImage;
    public float duration = 30f;

    private void Start()
    {
        if (targetImage != null)
        {
            StartCoroutine(DecreaseFillAmount());
        }
    }

    private IEnumerator DecreaseFillAmount()
    {
        float elapsedTime = 0f;
        float startAmount = 1f;
        float endAmount = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            targetImage.fillAmount = Mathf.Lerp(startAmount, endAmount, elapsedTime / duration);
            yield return null;
        }

        targetImage.fillAmount = endAmount; // Ќа вс€кий случай точно устанавливаем в 0
    }
}
