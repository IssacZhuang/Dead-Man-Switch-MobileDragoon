﻿using UnityEngine;
using Verse;

namespace WalkerGear
{
    [StaticConstructorOnStartup]
    public static class Resources
    {
        //安全裝置的Icon
        public static readonly Texture2D GetSafetyIcon_Disabled = ContentFinder<Texture2D>.Get("UI/Safety_Disabled");
        public static readonly Texture2D GetSafetyIcon = ContentFinder<Texture2D>.Get("UI/Safety");
        public static readonly Texture2D GetOutIcon = ContentFinder<Texture2D>.Get("UI/GetOffWalker");

        //裝配介面的Icon
        public static readonly Texture2D rotateButton = ContentFinder<Texture2D>.Get("UI/Rotate");
        public static readonly Texture2D rotateOppoButton = ContentFinder<Texture2D>.Get("UI/RotateOppo");

        //彈射裝置的Icon
        public static readonly Texture2D catapultLaunch = ContentFinder<Texture2D>.Get("UI/CatapultLaunch");
        public static readonly Texture2D catapultThrow = ContentFinder<Texture2D>.Get("UI/CatapultThrow");
        public static readonly Texture2D catapultEject = ContentFinder<Texture2D>.Get("UI/CatapultEject");

    }
}
