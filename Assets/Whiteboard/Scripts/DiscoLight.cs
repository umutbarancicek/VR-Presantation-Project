using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscoLight : MonoBehaviour
{
    [SerializeField] private float changeInterval = 0.5f;
    public AudioSource audioSource;

    private List<Light> _lights;
    private List<Color> _originalLightColors;
    private bool _discoModeEnabled = false;

    // Start is called before the first frame update
    public void Light()
    {
        ToggleDiscoMode();
    }

    private void ToggleDiscoMode()
    {
        _discoModeEnabled = !_discoModeEnabled;

        if (_discoModeEnabled)
        {
            _lights = new List<Light>(FindObjectsOfType<Light>());
            _originalLightColors = new List<Color>();
            foreach (var light in _lights)
            {
                _originalLightColors.Add(light.color); // Store original colors
            }
            InvokeRepeating(nameof(ChangeLightColor), 0f, changeInterval);
            audioSource.Play();
        }
        else
        {
            CancelInvoke(nameof(ChangeLightColor));
            // Restore original colors
            for (int i = 0; i < _lights.Count; i++)
            {
                _lights[i].color = _originalLightColors[i];
            }
            _lights.Clear();
            _originalLightColors.Clear();
            audioSource.Stop();
        }
    }

    private void ChangeLightColor()
    {
        foreach (var light in _lights)
        {
            light.color = new Color(Random.value, Random.value, Random.value);
        }
    }
}