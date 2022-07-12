using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControl : MonoBehaviour
{
    public AudioSource normalTrack;
    public AudioSource bossTrack;

    // Start is called before the first frame update
    private void Start()
    {
        normalTrack = GetComponents<AudioSource>()[0];
        bossTrack = GetComponents<AudioSource>()[1];

        if (BattleControl.CurrentBattleType != BattleControl.BattleType.ThirdBoss)
            normalTrack.Play();
        else
            bossTrack.Play();
    }
}
