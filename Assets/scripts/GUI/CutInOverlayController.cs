using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutInOverlayController : MonoBehaviour
{
    static public bool CutInEnabled;
    public float LastPhraseTimer = 2f;

    public static GameObject refToCutInOverlay;
    public static bool IsAtBattleStart = true;

    // Start is called before the first frame update
    private void Start()
    {
        Reload();
    }

    public void Reload()
    {
        CutInEnabled = true;
        LastPhraseTimer = 2f;

        refToCutInOverlay = gameObject;

        foreach (var item in GetComponentsInChildren<CutInPopupController>())
        {
            item.Reload();
        }
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
            gameObject.SetActive(false);
        }
    }
}
