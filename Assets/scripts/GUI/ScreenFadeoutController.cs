using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;

public class ScreenFadeoutController : MonoBehaviour
{
    public SpriteRenderer image;
    static public ScreenFadeoutController current;

    public bool IsAtMaxAlpha => currentAlpha == maxAlpha;
    public static bool IsHigherThanMinAlphaNow;
    public bool IsSlower;

    private const float maxAlpha = 1f;
    private const float minAlpha = 0f;
    private const float rateAlpha = 0.4f;
    private const float slowRateAlpha = 0.2f;
    private float currentAlpha;

    public static bool IsOnLangSelectScreen;

    private const float PreLoadTimerMax = 4f;
    private float PreLoadTimerNow = 0f;
    public static bool IsInPreLoadNow;

    private readonly float[] validAlpha =
    {
        0f, 0.05f, 0.10f, 0.15f,
        0.20f, 0.25f, 0.30f, 0.35f,
        0.40f, 0.45f, 0.50f, 0.55f,
        0.60f, 0.65f, 0.70f, 0.75f,
        0.80f, 0.85f, 0.90f, 0.95f,
        1f
    };

    // Start is called before the first frame update
    private void Start()
    {
        current = this;

        IsOnLangSelectScreen = SceneManager.GetActiveScene().name == "title";

        image = gameObject.GetComponent<SpriteRenderer>();
        currentAlpha = maxAlpha;

        if (IsOnLangSelectScreen)
        {
            PreLoadTimerNow = PreLoadTimerMax;
            IsInPreLoadNow = true;
#if !UNITY_ANDROID
            PreLoadTimerNow = 2f;
#endif
        }
        else
        {
            PreLoadTimerNow = 0f;
            IsInPreLoadNow = false;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (Advertisement.isShowing)
            return;

        if (PreLoadTimerNow > 0f)
        {
            PreLoadTimerNow -= Time.deltaTime;
            return;
        }
        else
        {
            IsInPreLoadNow = false;
        }

        var clr = image.color;
        float closest = UpdateAndApproximateCurrentAlpha();
        image.color = new Color(clr.r, clr.g, clr.b, closest);

        IsHigherThanMinAlphaNow = closest > minAlpha;
    }

    private float UpdateAndApproximateCurrentAlpha()
    {
        var rateCurrent = IsSlower ? slowRateAlpha : rateAlpha;

        currentAlpha -= rateCurrent * Time.deltaTime;
        if (currentAlpha < 0f)
            currentAlpha = minAlpha;
        float closest = validAlpha.OrderBy(x => Mathf.Abs(currentAlpha - x)).First();
        return closest;
    }

    public void Reset()
    {
        Start();
    }
}
