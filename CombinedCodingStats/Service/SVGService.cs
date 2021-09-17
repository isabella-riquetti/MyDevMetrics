using CombinedCodingStats.Infraestructure;

namespace CombinedCodingStats.Service
{
    public class SVGService : ISVGService
    {
        public string GetActivitySquare()
        {
            return "<rect x=\"{0}\" y=\"{1}\" width=\"{2}\" height=\"{2}\" " +
                "r=\"{3}\" rx=\"{3}\" ry=\"{4}\" fill=\"{4}\" stroke=\"none\" style=\"-webkit-tap-highlight-color: rgba(0, 0, 0, 0);\"></rect>";
        }
    }
}
