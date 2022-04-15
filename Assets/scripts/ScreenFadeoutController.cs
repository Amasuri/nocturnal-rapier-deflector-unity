using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScreenFadeoutController : MonoBehaviour
{
    public SpriteRenderer image;
    public static bool IsCurrentlyFading;

    private const float maxAlpha = 1f;
    private const float minAlpha = 0f;
    private const float rateAlpha = 0.4f;
    private float currentAlpha;

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
        image = gameObject.GetComponent<SpriteRenderer>();
        currentAlpha = maxAlpha;
    }

    // Update is called once per frame
    private void Update()
    {
        var clr = image.color;
        float closest = UpdateAndApproximateCurrentAlpha();
        image.color = new Color(clr.r, clr.g, clr.b, closest);

        IsCurrentlyFading = closest > minAlpha;
    }

    private float UpdateAndApproximateCurrentAlpha()
    {
        currentAlpha -= rateAlpha * Time.deltaTime;
        if (currentAlpha < 0f)
            currentAlpha = minAlpha;
        float closest = validAlpha.OrderBy(x => Mathf.Abs(currentAlpha - x)).First();
        return closest;
    }
}
