using System;
using System.Collections.Generic;

namespace CombinedCodingStats.Infraestructure
{
    public interface ISVGService
    {
        string BuildGraph(Dictionary<DateTime, int> activityPerDay, Platform platform, Theme theme, bool animationEnabled = true, bool backgroundEnabled = true);
    }
}
