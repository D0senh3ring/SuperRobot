using System;

public class RespawnCountChangedEventArgs : EventArgs
{
    public RespawnCountChangedEventArgs(int maxRespawns, int remainingRespawns)
    {
        this.RemainingRespawns = remainingRespawns;
        this.MaxRespawns = maxRespawns;
    }

    public int RemainingRespawns { get; set; }
    public int MaxRespawns { get; set; }
}
