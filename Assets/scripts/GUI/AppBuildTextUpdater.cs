using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AppBuildTextUpdater : MonoBehaviour
{
    public TextMeshProUGUI textTerminal;

    // Start is called before the first frame update
    private void Start()
    {
        var ver = Application.version;
        textTerminal = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        textTerminal.text = "build " + ver;
    }

    // Update is called once per frame
    private void Update()
    {
    }
}
