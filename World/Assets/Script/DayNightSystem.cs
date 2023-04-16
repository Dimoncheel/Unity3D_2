using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightSystem : MonoBehaviour
{
    private const float _FullDayTime = 50f;
    private float _lightingeDeltaAngle = 360f/_FullDayTime;
    [SerializeField]
    private Material daySkybox;
    [SerializeField]
    private Material nightSkybox;
    AudioSource daySound;
    AudioSource nightSound;
    AudioSource dayMusic;
    GameObject lights;

    private Light sun;
    private Light moon;

    private float _dayPhase;
    private float _dayTime;
    void Start()
    {
        AudioSource[] audioSources=this.GetComponents<AudioSource>();
        daySound=audioSources[0];
        nightSound=audioSources[1];
        dayMusic=audioSources[2];
        //Методы Start выполняется в случайном порядке,
        daySound.volume=nightSound.volume=dayMusic.volume=0f;
        lights= GameObject.Find("Lights");

        sun=GameObject.Find("Sun").GetComponent<Light>();
        moon=GameObject.Find("Moon").GetComponent<Light>();

        daySound.Play();
        daySound.Play();

        RenderSettings.skybox = daySkybox;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        lights.transform.Rotate(_lightingeDeltaAngle*Time.deltaTime,0,0);
        daySound.volume = nightSound.volume = GameSettings.AllSoundsDisabled || !GameSettings.EffectsEnabled
            ? 0f
            : GameSettings.MusicVolume;
        dayMusic.volume = GameSettings.AllSoundsDisabled || !GameSettings.MusicEnabled
            ? 0f
            : GameSettings.MusicVolume;
        ProcessDayCycle();
    }
    private void ProcessDayCycle()
    {
        _dayTime += Time.deltaTime;
        _dayTime %= _FullDayTime;
        _dayPhase = _dayTime / _FullDayTime;

        bool isNight = _dayPhase > 0.25 && _dayPhase <= 0.75;
        if (isNight)
        {
           if(RenderSettings.skybox!=nightSkybox) RenderSettings.skybox=nightSkybox;
        }
        else
        {
            if(RenderSettings.skybox!=daySkybox) RenderSettings.skybox=daySkybox;
        }
        float k = Mathf.Abs(Mathf.Cos(_dayPhase * 2f * Mathf.PI));

        RenderSettings.skybox.SetFloat("_Exposure", k * 0.9f + 0.1f);
        RenderSettings.ambientIntensity = isNight ? k / 2f : k;

        sun.intensity = isNight ? 0f : k;
        moon.intensity = isNight ? 5f*k : 0f;

        //RenderSettings.ambientIntensity;
        //RenderSettings.skybox.SetFloat("_Exposure", 1);
    }
}
