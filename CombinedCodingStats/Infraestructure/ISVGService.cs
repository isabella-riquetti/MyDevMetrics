using System;
using System.Collections.Generic;

namespace CombinedCodingStats.Infraestructure
{
    public interface ISVGService
    {
        string BuildGraph(Dictionary<DateTime, int> activityPerDay, bool useAnimation, Platform platform, Theme theme);
    }
}
