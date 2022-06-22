using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;

public class LangSelectorAcceptButton : MonoBehaviour
{
    public SpriteRenderer rend;

    public Sprite normal;
    public Sprite pressed;

    // Start is called before the first frame update
    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void OnMouseOver()
    {
        rend.sprite = pressed;

        if (Input.GetMouseButtonDown(0))
        {
            LangSelectorAdvertBanner.banner.HideBannerAd();

            Destroy(LangSelectorAdvertBanner.banner);
            Destroy(LangSelectorAdvertFullscreen.fullscreen);

            SceneManager.LoadScene("dialogue");
            SceneManager.UnloadSceneAsync("title");
        }
    }

    private void OnMouseExit()
    {
        rend.sprite = normal;
    }
}
