using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerController : MonoBehaviour
{
    public TextMeshProUGUI descTextTerminal;

    // Start is called before the first frame update
    private void Start()
    {
        descTextTerminal = gameObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    private void Update()
    {
        string minutes = ((int)BattleControl.TimeLeftSecLast / 60).ToString("00");
        string seconds = ((int)BattleControl.TimeLeftSecLast % 60).ToString("00");

        descTextTerminal.text = string.Format("{0}:{1}", minutes, seconds);
    }
}
