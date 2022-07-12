using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextBackgroundSelector : MonoBehaviour
{
    public SpriteRenderer renderer;

    public Sprite tutorialPicLeft;
    public Sprite tutorialPicRight;
    public Sprite defaultBg;

    // Start is called before the first frame update
    private void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        renderer.sprite = defaultBg;

#if UNITY_ANDROID //touch controls tutorial is only relevant to touch devices. I might do same kinda tutor for mouse on PC, but idk
        if (TextScene.CurrentSceneType != TextPool.SceneType.First)
            return;

        if (TextScene.current.TutorialIsShowingLeftSide)
            renderer.sprite = tutorialPicLeft;
        else if (TextScene.current.TutorialIsShowingRightSide)
            renderer.sprite = tutorialPicRight;
        else
            renderer.sprite = defaultBg;
#else //PC control manual was planned to be different but is for now the same; however that may change, hence the double
        if (TextScene.CurrentSceneType != TextPool.SceneType.First)
            return;

        if (TextScene.current.TutorialIsShowingLeftSide)
            renderer.sprite = tutorialPicLeft;
        else if (TextScene.current.TutorialIsShowingRightSide)
            renderer.sprite = tutorialPicRight;
        else
            renderer.sprite = defaultBg;
#endif
    }
}
