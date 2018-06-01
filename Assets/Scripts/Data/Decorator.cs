using System;
using UnityEngine;

namespace Bartok
{
    /// <summary>
    /// 存储来自于DeckXML的角码符号的信息
    /// </summary>
    public class Decorator
    {
        public string type;  // 对于花色符号，type=“pip”
        public Vector3 loc;  // sprite在纸片上的位置信息
        public bool flip = false;  // 是否垂直翻转Sprite
        public float scale = 1f;   // Sprite的缩放比例
    }

}

