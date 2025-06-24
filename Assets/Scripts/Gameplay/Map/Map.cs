using System;
using UnityEngine;

namespace Gameplay.Map
{
    public class Map : MonoBehaviour
    {
        [Serializable]
        public struct GeoRectInspector
        {
            public GeoCoordInspector Center;
            public float Width;
            public float Height;

            public GeoRect ToGeoRect() => new(Center.ToGeoCoord(), Width, Height);
        }

        [Serializable]
        public struct GeoCoordInspector
        {
            public float Latitude;
            public float Longtitude;

            public GeoCoord ToGeoCoord() => new(Latitude, Longtitude);
        }

        public static Map Instance { get; private set; } // Singleton instance of the Map class

        [SerializeField] private float scale = 100f; // meter/unit
        [SerializeField] private Vector2 size; // Size of the map in units
        [SerializeField] private GeoRectInspector boundingRectangle; // Inspector field for GeoRect

        public float Scale => scale; // Read-only property to access the scale
        public Vector2 Size => new(boundingRectangle.Width / scale, boundingRectangle.Height / scale); // Read-only property to access the size
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Awake()
        {

        }

        void Start()
        {
            if (Instance == null)
                Instance = this; // Set the singleton instance to this object
            else
                Destroy(gameObject); // Destroy this object if another instance already exists
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
