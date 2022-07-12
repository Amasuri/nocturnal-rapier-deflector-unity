using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingIcon : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        if(!ScreenFadeoutController.current.IsAtMaxAlpha || !LangSelectorAdvertInit.AdSupported)
        {
            GetComponent<SpriteRenderer>().enabled = false;
            return;
        }

        var t = Time.realtimeSinceStartup;

        transform.Rotate(0, 0, -200f * Time.deltaTime);
        transform.position = new Vector3(Mathf.Sin(t * 1.5f) / 3, Mathf.Cos(t * 1.5f) / 3, 0);
    }
}
