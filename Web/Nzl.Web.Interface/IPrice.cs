namespace Nzl.Web.Interface
{
    using System;
    using Nzl.Web.Core;
    using Nzl.Web.Core.EventArgs;

    /// <summary>
    /// The price interface.
    /// </summary>
    public interface IPrice : IClawer
    {
        /// <summary>
        /// Target price clawed eventhandler.
        /// </summary>
        event EventHandler<PriceClawingEventArgs> TargetPriceAccur;

        /// <summary>
        /// Price changed eventhandler.
        /// </summary>
        event EventHandler<PriceClawingEventArgs> PriceChanged;

        /// <summary>
        /// Price clawing eventhandler.
        /// </summary>
        event EventHandler<PriceClawingEventArgs> PriceClawing;

        /// <summary>
        /// Price clawed eventhandler.
        /// </summary>
        event EventHandler<PriceClawingEventArgs> PriceClawed;
    }
}
