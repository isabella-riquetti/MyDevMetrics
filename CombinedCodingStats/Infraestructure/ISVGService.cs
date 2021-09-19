using CombinedCodingStats.Model.GitLab;
using System;
using System.Collections.Generic;

namespace CombinedCodingStats.Infraestructure
{
    public interface ISVGService
    {
        string BuildGraph(Dictionary<DateTime, int> activityPerDay, GitLabMetricsOptions options);
    }
}
