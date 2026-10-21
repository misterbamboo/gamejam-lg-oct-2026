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

        // Taste = 10 - abs(Heat - Request): anywhere inside the range is perfect,
        // outside it loses one point per unit of distance from the nearest edge.
        public float TasteOf(float heat)
        {
            var distance = heat < Min ? Min - heat : heat > Max ? heat - Max : 0f;
            return 10f - distance;
        }

        internal string DisplayText()
        {
            return $"{Min} to {Max}";
        }
    }
}