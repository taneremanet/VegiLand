using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProductPlantController : MonoBehaviour
{
    [SerializeField] private ProductData productData;

    private bool isReadyToPick;
    private Vector3 originalScale;

    private BagController bagController;

    void Start()
    {
        isReadyToPick = true;
        originalScale = transform.localScale;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && isReadyToPick)
        {
            //Debug.Log("Player");

            AudioManager.instance.PlayAudio(AudioClipType.grabClip);
            bagController = other.GetComponent<BagController>();
            if (bagController.IsEmptySpace())
            {
                bagController.AddProductToBag(productData);
                isReadyToPick = false;
                StartCoroutine(ProductPicked());
            }
        }
    }

    IEnumerator ProductPicked() // classical way, we used leantween way
    {
        float duration = 1f;
        float timer = 0;

        Vector3 targetScale = originalScale / 3;

        while (timer < duration)
        {
            float t = timer / duration;
            Vector3 newScale = Vector3.Lerp(originalScale, targetScale, t); // original scale to small scale
            transform.localScale = newScale;
            timer += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(5f);

        timer = 0;
        float growBackDuration = 1f;

        while (timer < growBackDuration)
        {
            float t = timer / growBackDuration;
            Vector3 newScale = Vector3.Lerp(targetScale, originalScale, t); // small scale to original scale
            transform.localScale = newScale;
            timer += Time.deltaTime;
            yield return null;
        }

        isReadyToPick = true;

        yield return null;
    }
}
