using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingIcon : MonoBehaviour
{
    public LoadingIconComponent iconComponent;
    public SpriteRenderer rend;

    private const float orbShrtDiv = 5f;
    private const float orbLngDiv = 3f;
    private const float rotFact = -200f;

    public enum LoadingIconComponent
    {
        Rapier,
        Junk1,
        Junk2,
        Junk3
    }

    // Start is called before the first frame update
    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        SetSpriteAlpha(0f);
    }

    // Update is called once per frame
    private void Update()
    {
        if (!ScreenFadeoutController.current.IsAtMaxAlpha || !LangSelectorAdvertInit.AdSupported || LangSelectorAdvertInit.Finalized)
        {
            GetComponent<SpriteRenderer>().enabled = false;
            return;
        }

        SetSpriteAlpha(rend.color.a + Time.deltaTime / 1.5f);
        MoveAndRotateIconElement();
    }

    private void MoveAndRotateIconElement()
    {
        var t = Time.realtimeSinceStartup * 1.5f;

        switch (iconComponent)
        {
            case LoadingIconComponent.Rapier:
                transform.Rotate(0, 0, rotFact * Time.deltaTime);

                //transform.position = new Vector3(Mathf.Sin(t * 1.5f) / 13, Mathf.Cos(t * 1.5f) / 13, 0);
                break;

            case LoadingIconComponent.Junk1:
                transform.Rotate(0, 0, rotFact * Time.deltaTime);
                transform.position = new Vector3(Mathf.Sin(t * 2) / orbLngDiv, Mathf.Cos(t * 1.5f) / orbShrtDiv, 0);
                break;

            case LoadingIconComponent.Junk2:
                transform.Rotate(0, 0, rotFact * Time.deltaTime);
                transform.position = new Vector3(-Mathf.Sin(t * 2) / orbShrtDiv, Mathf.Cos(t * 1.5f) / orbLngDiv, 0);
                break;

            case LoadingIconComponent.Junk3:
                transform.Rotate(0, 0, rotFact * Time.deltaTime);
                transform.position = new Vector3(Mathf.Sin(t * 2) / orbShrtDiv, -Mathf.Cos(t * 1.5f) / orbLngDiv, 0);
                break;
        }
    }

    private void SetSpriteAlpha(float a)
    {
        rend.color = new Color(rend.color.r, rend.color.g, rend.color.b, a);
    }
}
