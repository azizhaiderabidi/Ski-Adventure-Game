using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class WeatherSystem : MonoBehaviour
{
    public enum WeatherType { Summer, Rain, Wind, Snowfall }
    public Image weatherImage;
    public Sprite[] weatherSprites;

    public TextMeshProUGUI weatherText;

    private WeatherType currentWeather;
    private int currentWeatherIndex = 0;

    public float[] weatherDurations = { 10f, 80f, 12f, 15f }; // Summer = 10s, Rain = 8s, Wind = 12s, Snowfall = 15s
    private WeatherType[] weatherSequence = { WeatherType.Summer, WeatherType.Wind, WeatherType.Rain, WeatherType.Snowfall };
    public GameObject[] weatherEffects;

    void Start()
    {
        StartCoroutine(WeatherCycle());
    }

    void UpdateWeatherDisplay()
    {
        if (weatherText != null)
        {
            weatherText.text = currentWeather.ToString();
            SetWeatherEffect(currentWeatherIndex);
            weatherImage.sprite = weatherSprites[currentWeatherIndex];
            //switch (currentWeather)
            //{
            //    case WeatherType.Summer:
            //        break;
            //    case WeatherType.Rain:
            //        SetWeatherEffect()
            //        break;
            //    case WeatherType.Wind:
            //        break;
            //    case WeatherType.Snowfall:
            //        break;
            //}
        }
    }

    void SetWeatherEffect(int weatherIndex)
    {
        for (int i = 0; i < weatherSequence.Length; i++)
        {
            if(i == weatherIndex)
            {
                weatherEffects[i].gameObject.SetActive(true);
            }
            else
            {
                weatherEffects[i].gameObject.SetActive(false);
            }

        }
    }

    IEnumerator WeatherCycle()
    {
        while (true)
        {
            currentWeather = weatherSequence[currentWeatherIndex];
            UpdateWeatherDisplay();
            yield return new WaitForSeconds(weatherDurations[currentWeatherIndex]);

            // Next Weather
            currentWeatherIndex = (currentWeatherIndex + 1) % weatherSequence.Length;
        }
    }

    public void ChangeWeatherManually(WeatherType newWeather)
    {
        StopAllCoroutines();
        currentWeather = newWeather;
        UpdateWeatherDisplay();
        StartCoroutine(WeatherCycle()); // Restart cycle
    }
}
