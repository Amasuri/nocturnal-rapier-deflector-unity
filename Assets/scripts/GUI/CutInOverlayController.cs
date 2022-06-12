using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutInOverlayController : MonoBehaviour
{
    static public bool CutInEnabled;
    public float LastPhraseTimer = 2f;

    // Start is called before the first frame update
    private void Start()
    {
        CutInEnabled = true;
        LastPhraseTimer = 2f;
    }

    // Update is called once per frame
    private void Update()
    {
        if(CutInPopupController.WitchBoxArrived)
        {
            LastPhraseTimer -= Time.deltaTime;
        }
        if(LastPhraseTimer<=0f)
        {
            CutInEnabled = false;
            Destroy(gameObject);
        }
    }
}
