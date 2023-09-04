using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UnlockUnitBakeryController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI bakeryText;
    [SerializeField] private int maxStoredProductCount;
    [SerializeField] private ProductType productType;
    private int storedProductCount;


    [SerializeField] private int useProductInSeconds = 10;
    [SerializeField] private Transform coinTransform;
    [SerializeField] private GameObject coinGO;
    private float time;

    [SerializeField] private ParticleSystem smokeParticle;

    void Start()
    {
        DisplayProductCount();
    }


    void Update()
    {
        if (storedProductCount > 0)
        {
            time += Time.deltaTime;
            if (time > useProductInSeconds)
            {
                time = 0f;
                UseProduct();
            }
        }
    }

    private void DisplayProductCount()
    {
        bakeryText.text = storedProductCount.ToString() + "/" + maxStoredProductCount.ToString();
        ControlSmokeEffect();
    }

    public ProductType GetNeededProductType()
    {
        return productType;
    }

    public bool StoreProduct()
    {
        if (maxStoredProductCount == storedProductCount)
        {
            return false;
        }
        storedProductCount++;
        DisplayProductCount();
        return true;
    }

    private void UseProduct()
    {
        storedProductCount--;
        DisplayProductCount();
        CreateCoin();
    }

    private void CreateCoin()
    {
        Vector3 position = UnityEngine.Random.insideUnitSphere * 1f;
        Vector3 instantiatePos = coinTransform.position + position;
        Instantiate(coinGO, instantiatePos, Quaternion.identity);
    }

    private void ControlSmokeEffect()
    {
        if (storedProductCount == 0)
        {
            if (smokeParticle.isPlaying)
            {
                smokeParticle.Stop();
            }
        }
        else
        {
            if (!smokeParticle.isPlaying)
            {
                smokeParticle.Play();
            }
        }
    }
}
