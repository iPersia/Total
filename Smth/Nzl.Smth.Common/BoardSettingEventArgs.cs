using System;

/// <summary>
/// 
/// </summary>
public class BoardSettingEventArgs : EventArgs
{
    /// <summary>
    /// 
    /// </summary>
    public BoardSettingEventArgs()
    {
        this.IsShowTop = true;
        this.AutoUpdating = false;
        this.UpdatingInterval = 60;
    }

    /// <summary>
    /// 
    /// </summary>
    public bool IsShowTop
    {
        get;
        set;
    }

    /// <summary>
    /// 
    /// </summary>
    public bool AutoUpdating
    {
        get;
        set;
    }
    /// <summary>
    /// 
    /// </summary>
    public int UpdatingInterval
    {
        get;
        set;
    }

    /// <summary>
    /// 
    /// </summary>
    public object Tag
    {
        get;
        set;
    }
}
