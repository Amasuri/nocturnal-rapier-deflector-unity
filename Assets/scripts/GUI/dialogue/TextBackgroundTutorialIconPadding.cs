using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextBackgroundTutorialIconPadding : MonoBehaviour
{
    public SpriteRenderer renderer;

    public Sprite touchMeIcon;
    public Sprite pressMeIcon;

    // Start is called before the first frame update
    private void Start()
    {
#if UNITY_ANDROID
        renderer = GetComponent<SpriteRenderer>();
        renderer.sprite = touchMeIcon;
#else
        renderer = GetComponent<SpriteRenderer>();
        renderer.sprite = pressMeIcon;

#endif
    }

    // Update is called once per frame
    private void Update()
    {
    }
}
