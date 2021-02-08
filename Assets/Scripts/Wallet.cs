using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wallet : MonoBehaviour
{
    [SerializeField] private Text _score;

    public int Score { get; private set; }

    public void AddOne()
    {
        Score++;
        _score.text = Score.ToString();
    }
}
