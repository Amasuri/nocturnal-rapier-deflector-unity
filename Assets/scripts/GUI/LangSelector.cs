using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LangSelector : MonoBehaviour
{
    public TextPool.SceneLanguage thisLanguage;
    public SpriteRenderer rend;

    public Sprite active;
    public Sprite inactive;

    // Start is called before the first frame update
    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;

        if (thisLanguage != TextPool.sceneLang)
            rend.sprite = inactive;
        else
            rend.sprite = active;
    }

    // Update is called once per frame
    private void Update()
    {
        if (thisLanguage != TextPool.sceneLang)
            rend.sprite = inactive;

        if (Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene("dialogue");
            SceneManager.UnloadSceneAsync("title");
        }
    }

    private void OnMouseOver()
    {
        transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;

        if (!Input.GetMouseButton(0))
            return;

        //Else if clicked
        TextPool.SetSceneLanguage(thisLanguage);
        rend.sprite = active;
    }

    private void OnMouseExit()
    {
        transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
    }
}
