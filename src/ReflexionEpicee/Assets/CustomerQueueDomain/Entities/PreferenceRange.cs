using System;
using System.Collections;
using UnityEngine;

namespace Assets.CustomerQueueDomain.Entities
{
    public class PreferenceRange
    {
        public const float MaxHeat = 10f;

        public float Min { get; private set; }
        public float Max { get; private set; }
        public float Mid => (Min + Max) / 2f;

        public PreferenceRange(float min, float max)
        {
            Min = min < 0f ? 0f : min;
            Max = max > MaxHeat ? MaxHeat : max;
        }

        internal string DisplayText()
        {
            return $"{Min} to {Max}";
        }
    }
}