using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;
using DG.Tweening;

public class ImageEffectController : MonoBehaviour
{
    private PostProcessingProfile profile;

    private float chromaticAberrationIntensity
    {
        get
        {
            return profile.chromaticAberration.settings.intensity;
        }
        set
        {
            var chromaticAberration = profile.chromaticAberration.settings;
            chromaticAberration.intensity = value;
            profile.chromaticAberration.settings = chromaticAberration;
        }
    }

    private float vignetteIntensity
    {
        get
        {
            return profile.vignette.settings.intensity;
        }
        set
        {
            var vignette = profile.vignette.settings;
            vignette.intensity = value;
            profile.vignette.settings = vignette;
        }
    }

    // Use this for initialization
    void Start()
    {
        PostProcessingBehaviour behavoiur = Camera.main.GetComponent<PostProcessingBehaviour>();
        profile = behavoiur.profile;
    }

    public void OnChromatic(float target, float duration)
    {
        float originalIntensity = chromaticAberrationIntensity;
        Sequence sequence = DOTween.Sequence();
        sequence
            .Append(
                DOTween.To(
                    () => chromaticAberrationIntensity,
                    (x) => chromaticAberrationIntensity = x,
                    target, duration / 2))
            .Append(
                DOTween.To(
                    () => chromaticAberrationIntensity,
                    (x) => chromaticAberrationIntensity = x,
                    originalIntensity, duration / 2));
    }

    public void OnMist(float duration)
    {
        float originalIntensity = vignetteIntensity;
        Sequence sequence = DOTween.Sequence();
        sequence
            .Append(
                DOTween.To(
                    () => vignetteIntensity,
                    (x) => vignetteIntensity = x,
                    1.0f, duration / 2))
            .Append(
                DOTween.To(
                    () => vignetteIntensity,
                    (x) => vignetteIntensity = x,
                    originalIntensity, duration / 2));
    }
}
