using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int GemCount;
    public int CherryCount;
    public int EnemiesKilledCount;

    public static Inventory instance;

    private void Awake()
    {
        if (instance != null)
        {
            return;
        }
        instance = this;
    }

    public void AddGem(int count)
    {
        GemCount += count;
    }

    public void AddCherry(int count)
    {
        CherryCount += count;
    }

    public void Enemykilled(int count)
    {
        EnemiesKilledCount += count;
    }
}
