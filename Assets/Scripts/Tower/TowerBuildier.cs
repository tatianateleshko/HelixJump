using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBuildier : MonoBehaviour
{
    [SerializeField]
    private int _levelcount; // количество уровней на башне; сколько платформ сверху вниз
    [SerializeField]
    private float _additionalScale;
    [SerializeField]
    private GameObject _beam; //то на чем будет держатьс€

    [SerializeField]
    private SpawnPlatform _spawnPlatform;
    [SerializeField]
    private FinishPlatform _finishPlatform;
    [SerializeField]
    private Platform [] _platform;

    private float _startAndFinishAdditionalScale = 0.5f; // чтобы сверху и снизу было место дл€ спавна платформы

    public float BeamScaleY => _levelcount / 2f + _startAndFinishAdditionalScale + _additionalScale/2f;

    private void Awake()
    {
        Build();
    }

    private void Build() //функци€ по потройке башни
    {
        GameObject beam = Instantiate(_beam, transform);
        beam.transform.localScale = new Vector3(1, BeamScaleY, 1);

        Vector3 spawnPosition = beam.transform.position;
        spawnPosition.y += beam.transform.localScale.y - _additionalScale; //сама€ верхн€€ точка beam

        SpawnPlatform(_spawnPlatform, ref spawnPosition, beam.transform);

        for (int i = 0; i < _levelcount; i++)
        {
            SpawnPlatform(_platform[Random.Range(0, _platform.Length)], ref spawnPosition,beam.transform);
        }

        SpawnPlatform(_finishPlatform, ref spawnPosition, beam.transform);
    }

    private void SpawnPlatform(Platform platform, ref Vector3 spawnPosition, Transform parent)
    {
        Instantiate(platform, spawnPosition, Quaternion.Euler(0, Random.Range(0, 360), 0), parent);
        spawnPosition.y -= 1;
    }

}
