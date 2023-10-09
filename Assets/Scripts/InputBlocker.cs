using System.Collections.Generic;
using DG.Tweening;

public static class InputBlocker
{
    private static readonly Dictionary<object, int> _lockCounter = new ();
    
    public static void InputLock(object sender, float time = -1)
    {
        if(_lockCounter.ContainsKey(sender) == false)
            _lockCounter.Add(sender, 0);

        _lockCounter[sender]++;

        if (time != -1)
            DOVirtual.DelayedCall(time, () => InputUnlock(sender));
    }

    public static void InputUnlock(object sender)
    {
        if(_lockCounter.ContainsKey(sender) == false)
            return;
        
        if(_lockCounter[sender] <= 0)
            return;

        _lockCounter[sender]--;
    }

    public static bool IsLock()
    {
        foreach (var locker in _lockCounter)
        {
            if (locker.Value > 0)
                return true;
        }

        return false;
    }
}
