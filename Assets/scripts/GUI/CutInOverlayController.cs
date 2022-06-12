using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutInOverlayController : MonoBehaviour
{
    static public bool CutInEnabled;

    // Start is called before the first frame update
    private void Start()
    {
        CutInEnabled = true;
    }

    // Update is called once per frame
    private void Update()
    {
        if(CutInPopupController.WitchBoxArrived)
        {
            CutInEnabled = false;
            Destroy(gameObject);
        }
    }
}
