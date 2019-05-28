using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraP2 : MonoBehaviour
{
    private bool isShaking = false;
    private float baseX, baseY, baseZ;
    private float intensity;
    private int shakes = 0;

    public static CameraP2 instance = null;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

       // DontDestroyOnLoad(gameObject);

        baseX = transform.position.x;
        baseY = transform.position.y;
        baseZ = transform.position.z;

        intensity = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {

        if (isShaking)
        {
            float randomShakeX = Random.Range(-intensity, intensity);
            float randomShakeY = Random.Range(-intensity, intensity);
            float randomShakeZ = Random.Range(0, intensity * 2);
            transform.position = new Vector3(baseX + randomShakeX, baseY + randomShakeY, baseZ + randomShakeZ);

            shakes--;

            if (shakes <= 0)
            {
                isShaking = false;
                transform.position = new Vector3(baseX, baseY, transform.position.z);
            }
        }

    }

    public void MinorShake(float in_intensity)
    {
        isShaking = true;
        shakes = 10;
        intensity = in_intensity;
    }

    public void LongShake(float in_intensity)
    {
        isShaking = true;
        shakes = 100;
        intensity = in_intensity;
    }
}

// credit: http://synersteel.com/blog/2015/7/6/unity3d-basic-screen-shake