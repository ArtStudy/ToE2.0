using UnityEngine;

namespace Assets.Core.Game.Data.UI
{
    public interface ITextStyle : IBase
    {
        int FontSize { get; set; }
        Font Font { get; set; }
        FontStyle FontStyle { get; set; }
    }
}