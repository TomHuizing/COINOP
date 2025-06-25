using UnityEngine;

namespace Gameplay.Map
{
    public readonly struct RegionStats
    {
        public readonly float Control;
        public readonly float Infrastructure;
        public readonly float InsurgencyPresence;
        public readonly float PopularSupport;
        public readonly float Stability;
        public readonly float IntelCoverage;
        public readonly float EconomicActivity;

        public RegionStats(
            float control,
            float infrastructure,
            float insurgencyPresence,
            float popularSupport,
            float stability,
            float intelCoverage,
            float economicActivity)
        {
            Control = control;
            Infrastructure = infrastructure;
            InsurgencyPresence = insurgencyPresence;
            PopularSupport = popularSupport;
            Stability = stability;
            IntelCoverage = intelCoverage;
            EconomicActivity = economicActivity;
        }

        public RegionStats SetControl(float value) => new
        (
            value,
            Infrastructure,
            InsurgencyPresence,
            PopularSupport,
            Stability,
            IntelCoverage,
            EconomicActivity
        );

        public RegionStats SetInfrastructure(float value) => new
        (
            Control,
            value,
            InsurgencyPresence,
            PopularSupport,
            Stability,
            IntelCoverage,
            EconomicActivity
        );

        public RegionStats SetInsurgencyPresence(float value) => new
        (
            Control,
            Infrastructure,
            value,
            PopularSupport,
            Stability,
            IntelCoverage,
            EconomicActivity
        );

        public RegionStats SetPopularSupport(float value) => new
        (
            Control,
            Infrastructure,
            InsurgencyPresence,
            value,
            Stability,
            IntelCoverage,
            EconomicActivity
        );

        public RegionStats SetStability(float value) => new
        (
            Control,
            Infrastructure,
            InsurgencyPresence,
            PopularSupport,
            value,
            IntelCoverage,
            EconomicActivity
        );

        public RegionStats SetIntelCoverage(float value) => new
        (
            Control,
            Infrastructure,
            InsurgencyPresence,
            PopularSupport,
            Stability,
            value,
            EconomicActivity
        );

        public RegionStats SetEconomicActivity(float value) => new
        (
            Control,
            Infrastructure,
            InsurgencyPresence,
            PopularSupport,
            Stability,
            IntelCoverage,
            value
        );

        public RegionStats AddControl(float value) => new
        (
            Control + value,
            Infrastructure,
            InsurgencyPresence,
            PopularSupport,
            Stability,
            IntelCoverage,
            EconomicActivity
        );

        public RegionStats AddInfrastructure(float value) => new
        (
            Control,
            Infrastructure + value,
            InsurgencyPresence,
            PopularSupport,
            Stability,
            IntelCoverage,
            EconomicActivity
        );

        public RegionStats AddInsurgencyPresence(float value) => new
        (
            Control,
            Infrastructure,
            InsurgencyPresence + value,
            PopularSupport,
            Stability,
            IntelCoverage,
            EconomicActivity
        );

        public RegionStats AddPopularSupport(float value) => new
        (
            Control,
            Infrastructure,
            InsurgencyPresence,
            PopularSupport + value,
            Stability,
            IntelCoverage,
            EconomicActivity
        );

        public RegionStats AddStability(float value) => new
        (
            Control,
            Infrastructure,
            InsurgencyPresence,
            PopularSupport,
            Stability + value,
            IntelCoverage,
            EconomicActivity
        );

        public RegionStats AddIntelCoverage(float value) => new
        (
            Control,
            Infrastructure,
            InsurgencyPresence,
            PopularSupport,
            Stability,
            IntelCoverage + value,
            EconomicActivity
        );

        public RegionStats AddEconomicActivity(float value) => new
        (
            Control,
            Infrastructure,
            InsurgencyPresence,
            PopularSupport,
            Stability,
            IntelCoverage,
            EconomicActivity + value
        );

        public override string ToString() => $"Control: {Control:F2}\nInfrastructure: {Infrastructure:F2}\n" +
                                              $"InsurgencyPresence: {InsurgencyPresence:F2}\nPopularSupport: {PopularSupport:F2}\n" +
                                              $"Stability: {Stability:F2}\nIntelCoverage: {IntelCoverage:F2}\n" +
                                              $"EconomicActivity: {EconomicActivity:F2}";

        public static RegionStats Lerp(RegionStats a, RegionStats b, float t) => new
        (
            Mathf.Lerp(a.Control, b.Control, t),
            Mathf.Lerp(a.Infrastructure, b.Infrastructure, t),
            Mathf.Lerp(a.InsurgencyPresence, b.InsurgencyPresence, t),
            Mathf.Lerp(a.PopularSupport, b.PopularSupport, t),
            Mathf.Lerp(a.Stability, b.Stability, t),
            Mathf.Lerp(a.IntelCoverage, b.IntelCoverage, t),
            Mathf.Lerp(a.EconomicActivity, b.EconomicActivity, t)
        );

        public static RegionStats Random => new
        (
            UnityEngine.Random.Range(0f, 1f), // Control
            UnityEngine.Random.Range(0f, 1f), // Infrastructure
            UnityEngine.Random.Range(0f, 1f), // InsurgencyPresence
            UnityEngine.Random.Range(0f, 1f), // PopularSupport
            UnityEngine.Random.Range(0f, 1f), // Stability
            UnityEngine.Random.Range(0f, 1f), // IntelCoverage
            UnityEngine.Random.Range(0f, 1f)  // EconomicActivity
        );

        public static RegionStats Zero => new
        (
            0f, // Control
            0f, // Infrastructure
            0f, // InsurgencyPresence
            0f, // PopularSupport
            0f, // Stability
            0f, // IntelCoverage
            0f  // EconomicActivity
        );

        public static RegionStats One => new
        (
            1f, // Control
            1f, // Infrastructure
            1f, // InsurgencyPresence
            1f, // PopularSupport
            1f, // Stability
            1f, // IntelCoverage
            1f  // EconomicActivity
        );

        public static RegionStats Half => new
        (
            0.5f, // Control
            0.5f, // Infrastructure
            0.5f, // InsurgencyPresence
            0.5f, // PopularSupport
            0.5f, // Stability
            0.5f, // IntelCoverage
            0.5f  // EconomicActivity
        );

        public static RegionStats operator +(RegionStats left, RegionStats right) => new
        (
            left.Control + right.Control,
            left.Infrastructure + right.Infrastructure,
            left.InsurgencyPresence + right.InsurgencyPresence,
            left.PopularSupport + right.PopularSupport,
            left.Stability + right.Stability,
            left.IntelCoverage + right.IntelCoverage,
            left.EconomicActivity + right.EconomicActivity
        );

        public static RegionStats operator -(RegionStats left, RegionStats right) => new
        (
            left.Control - right.Control,
            left.Infrastructure - right.Infrastructure,
            left.InsurgencyPresence - right.InsurgencyPresence,
            left.PopularSupport - right.PopularSupport,
            left.Stability - right.Stability,
            left.IntelCoverage - right.IntelCoverage,
            left.EconomicActivity - right.EconomicActivity
        );
    }
}
