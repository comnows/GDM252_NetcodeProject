using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Fruit
{
    public Sprite fruitPrefab;
    [Range(0f, 100f)]public float spawnChance = 100f;
    [HideInInspector]public double _weight;
}

[Serializable]
public class Slot
{
    public Image[] imageSlots;
}

public class SlotRandomSprite : MonoBehaviour
{
    [SerializeField] private Fruit[] fruits;
    [SerializeField] public Slot[] slots;

    private double accumulatedWeights;
    private System.Random rand = new System.Random();

    public double r;

    // Start is called before the first frame update
    void Start()
    {
        calaculateWeights();
    }

    private void calaculateWeights()
    {
        accumulatedWeights = 0;

        foreach(Fruit fruit in fruits)
        {
            accumulatedWeights += fruit.spawnChance;
            fruit._weight = accumulatedWeights;
        }
    }

    public void showFruit(int index)
    {
        for(int i = 0; i < slots[index].imageSlots.Length; i++)
        {
            slots[index].imageSlots[i].sprite = fruits[getRandomFruitIndex()].fruitPrefab;
        }
    }

    private int getRandomFruitIndex()
    {
        r = rand.NextDouble() * accumulatedWeights;

        for(int fruitIndex = 0; fruitIndex < fruits.Length; fruitIndex++)
        {
            if(fruits[fruitIndex]._weight >= r)
            {
                return fruitIndex;
            }
        }
        return 0;
    }
}
