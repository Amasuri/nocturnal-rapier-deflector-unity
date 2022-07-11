using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextBackgroundTutorialIcon : MonoBehaviour
{
    public SpriteRenderer renderer;
    public SpriteRenderer rendererChild;

    public Sprite touchMeIcon;
    public Sprite rapierIcon;

    private readonly Vector3 leftPivot = new Vector3(-1, 0, 10);
    private readonly Vector3 rightPivot = new Vector3(1, 0, 10);

    // Start is called before the first frame update
    private void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        renderer.enabled = false;

        rendererChild = gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
        rendererChild.enabled = false;
    }

    // Update is called once per frame
    private void Update()
    {
#if UNITY_ANDROID
        if (TextScene.CurrentSceneType != TextPool.SceneType.First)
        {
            renderer.enabled = false;
            rendererChild.enabled = false;
            return;
        }

        if(TextScene.current.TutorialIsShowingRightSide)
        {
            renderer.enabled = true;
            rendererChild.enabled = false;
            renderer.sprite = rapierIcon;
            transform.position = rightPivot + new Vector3(Mathf.Cos(Time.realtimeSinceStartup * 2) / 5, Mathf.Sin(Time.realtimeSinceStartup * 2) / 20, 0);
        }
        else if (TextScene.current.TutorialIsShowingLeftSide)
        {
            renderer.enabled = true;
            rendererChild.enabled = true;
            renderer.sprite = touchMeIcon;
            transform.position = leftPivot + new Vector3(0, Mathf.Cos(Time.realtimeSinceStartup * 2) / 10, 0);
            rendererChild.transform.position = leftPivot + new Vector3(0, -0.2f, 0);
        }
        else
        {
            renderer.enabled = false;
            rendererChild.enabled = false;
        }
#else //PC control manual was planned to be different but is for now the same; however that may change, hence the double
        if (TextScene.CurrentSceneType != TextPool.SceneType.First)
        {
            renderer.enabled = false;
            rendererChild.enabled = false;
            return;
        }

        if (TextScene.current.TutorialIsShowingRightSide)
        {
            renderer.enabled = true;
            rendererChild.enabled = false;
            renderer.sprite = rapierIcon;
            transform.position = rightPivot + new Vector3(Mathf.Cos(Time.realtimeSinceStartup * 2) / 5, Mathf.Sin(Time.realtimeSinceStartup * 2) / 20, 0);
        }
        else if (TextScene.current.TutorialIsShowingLeftSide)
        {
            renderer.enabled = true;
            rendererChild.enabled = true;
            renderer.sprite = touchMeIcon;
            transform.position = leftPivot + new Vector3(0, Mathf.Cos(Time.realtimeSinceStartup * 2) / 10, 0);
            rendererChild.transform.position = leftPivot + new Vector3(0, -0.2f, 0);
        }
        else
        {
            renderer.enabled = false;
            rendererChild.enabled = false;
        }
#endif
    }
}
