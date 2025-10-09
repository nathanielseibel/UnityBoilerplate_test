using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    int coinsCollected = 0;
    public void CollectCoin()
    {
        coinsCollected++;
    }
    public int GetCoinsCollected()
    {
        return coinsCollected;
    }
}
