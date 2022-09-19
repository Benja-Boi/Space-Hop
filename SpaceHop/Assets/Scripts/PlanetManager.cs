using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetManager : MonoBehaviour
{
    private static PlanetManager _instance;
    public static PlanetManager Instance {  get { return _instance; } }

    private Planet[] _planets;
    public Planet[] Planets { get { return _planets; }}

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        _planets = FindObjectsOfType<Planet>();
    }

    private void OnDestroy()
    {
        if (this == _instance) { _instance = null; }
    }
}
