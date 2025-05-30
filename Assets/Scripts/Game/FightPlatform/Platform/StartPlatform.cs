using System;

public class StartPlatform : BasePlatform
{
    public override bool IsCleared => true;
    public override event Action OnPlatformCleared;
}
