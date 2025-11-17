using UnityEngine;

[System.Serializable]
public class HorseMovementPattern
{

    [Header("움직임 패턴 파라미터")]
    private float variationRate; // 변동 빠르기
    private float variationAmount; // 변동 세기
    private float patternOffset; // 말마다 다른 변동 시작값 위한 offset

    public HorseMovementPattern()
    {
        variationRate = Random.Range(2.0f, 6.0f);
        variationAmount = Random.Range(3.0f, 5.0f);
        patternOffset = Random.Range(0f, 999f);
    }


    public float GetCurrentPattern()
    {
        // (-0.2, +0.8) 범위의 노이즈 값 생성 (noiseFrequency이 클수록 빠르게 변동함)
        float noise = Mathf.PerlinNoise(Time.time * variationRate, this.patternOffset) - 0.2f;

        // 변동 범위 조정(noiseAmplitude의 값이 클수록 변동 범위가 큼)
        float wobble = noise * variationAmount;

        // 최종 속도
        return wobble;
    }
}
