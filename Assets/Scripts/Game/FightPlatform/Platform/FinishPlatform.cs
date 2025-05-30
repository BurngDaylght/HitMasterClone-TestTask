using System;

public class FinishPlatform : BasePlatform
{
    public override bool IsCleared => true;
    public override event Action OnPlatformCleared;
}
