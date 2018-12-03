using System;

public class CheckpointChangedEventArgs : EventArgs
{
    public CheckpointChangedEventArgs(Checkpoint checkpoint)
    {
        this.NewCheckpoint = checkpoint;
    }

    public Checkpoint NewCheckpoint { get; private set; }
}
