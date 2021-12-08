using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    [SerializeField] private GameObject[] splashPrefabs;
    [SerializeField] private GameObject[] deathFX;
    [SerializeField] private int poolSize = 30;

    private Queue<GameObject> greenSplashPool = new Queue<GameObject>();
    private Queue<GameObject> redSplashPool = new Queue<GameObject>();
    private Queue<GameObject> greenFXPool = new Queue<GameObject>();
    private Queue<GameObject> redFXPool = new Queue<GameObject>();


    private void Awake()
    {
        SpawnSplashes();
        SpawnDeathParticles();
    }

    private void SpawnSplashes()
    {
        for (var i = 0; i < poolSize; i++)
        {
            GameObject splash1 = Instantiate(splashPrefabs[0]);
            GameObject splash2 = Instantiate(splashPrefabs[1]);
            greenSplashPool.Enqueue(splash1);
            redSplashPool.Enqueue(splash2);
            splash1.SetActive(false);
            splash2.SetActive(false);
        }
    }

    private void SpawnDeathParticles()
    {
        for (var i = 0; i < poolSize; i++)
        {
            GameObject particle1 = Instantiate(deathFX[0]);
            GameObject particle2 = Instantiate(deathFX[1]);
            greenFXPool.Enqueue(particle1);
            redFXPool.Enqueue(particle2);
            particle1.SetActive(false);
            particle2.SetActive(false);
        }
    }

    public GameObject GetGreenSplash()
    {        
        GameObject splash = greenSplashPool.Dequeue();
        splash.SetActive(true);
        greenSplashPool.Enqueue(splash);
        return splash;                
    }

    public GameObject GetRedSplash()
    {
        GameObject splash = redSplashPool.Dequeue();
        splash.SetActive(true);
        redSplashPool.Enqueue(splash);
        return splash;
    }

    public GameObject GetGreenDeathFX()
    {
        GameObject particle = greenFXPool.Dequeue();
        particle.SetActive(true);
        greenFXPool.Enqueue(particle);
        return particle;
    }

    public GameObject GetRedDeathFX()
    {
        GameObject particle = redFXPool.Dequeue();
        particle.SetActive(true);
        redFXPool.Enqueue(particle);
        return particle;
    }

}
