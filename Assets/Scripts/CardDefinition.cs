using System;
using UnityEngine;
using System.Collections.Generic;

namespace Bartok
{
    /// <summary>
    /// 此类用于存储各点数的牌面信息
    /// </summary>
    public class CardDefinition
    {
        public string face = "";  // 花牌使用的Sprite名称
        public int rank;     // 本张牌的点数（1-13）
        public List<Decorator> pips = new List<Decorator>();
    }
}

