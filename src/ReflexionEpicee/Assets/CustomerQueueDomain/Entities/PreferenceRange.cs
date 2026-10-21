using System;
using System.Collections;
using UnityEngine;

namespace Assets.CustomerQueueDomain.Entities
{
    public class PreferenceRange
    {
        public float Min { get; private set; }
        public float Max { get; private set; }

        public PreferenceRange(float min, float max)
        {
            Min = min < 0f ? 0f : min;
            Max = max > 10f ? 10f : max;
        }

        internal string DisplayText()
        {
            return $"{Min} to {Max}";
        }
    }
}