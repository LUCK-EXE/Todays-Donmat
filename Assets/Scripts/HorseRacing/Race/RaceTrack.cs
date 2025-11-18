using UnityEngine;

public class RaceTrack
{
    private const float trackLength = 40f;

    public float GetTrackLength()
    {
        return trackLength;
    }

    public bool IsFinished(float horsePositionX)
    {
        return horsePositionX >= trackLength;
    }
}
