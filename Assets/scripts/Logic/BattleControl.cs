using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BattleControl : MonoBehaviour
{
    public float TimeLeftSec { get; private set; }

    public TextMeshProUGUI descTextTerminal;

    // Start is called before the first frame update
    private void Start()
    {
        descTextTerminal = gameObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    private void Update()
    {
        descTextTerminal.text = string.Format("Witch: {0}      You: {1}", ScoreCounter.WitchHits, ScoreCounter.RapierHits);
    }
}
