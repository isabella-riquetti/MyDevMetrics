
namespace CombinedCodingStats.Service
{
    public static class SVGConstants
    {
        public static string CanvaOpen = "<svg width=\"{0}\" height=\"{1}\" version=\"1.1\" xmlns=\"http://www.w3.org/2000/svg\" style=\"overflow: hidden; position: relative;\">";

        public static string CanvaClose = "</svg>";

        public static string Background = "<rect x=\"0\" y=\"0\" width=\"{0}\" height=\"{1}\" " +
                        "rx=\"4\" ry=\"4\" fill=\"{2}\" stroke=\"none\"></rect>";

        public static string ActivitySquareOpen = "<rect x=\"{0}\" y=\"{1}\" width=\"{2}\" height=\"{2}\" " +
                "r=\"{3}\" rx=\"{3}\" ry=\"{4}\" fill=\"{4}\" stroke=\"none\" style=\"-webkit-tap-highlight-color: rgba(0, 0, 0, 0);\">";

        public static string ActivitySquareClose = "</rect>";

        public static string Animation = "<animate attributeName=\"fill\" values=\"{0}\" dur=\"{1}s\"/>";

        public static string Month = "<text x=\"{0}\" y=\"{1}\" " +
            "style=\"font-size:{2}px;fill:{3};font-family:{4}\">{5}</text>";

        public static string WeekDays =
            "<text x=\"{0}\" y=\"{1}\" style=\"font-size:{4}px;fill:{5};font-family:{6}\">{7}</text>" +
            "<text x=\"{0}\" y=\"{2}\" style=\"font-size:{4}px;fill:{5};font-family:{6}\">{8}</text>" +
            "<text x=\"{0}\" y=\"{3}\" style=\"font-size:{4}px;fill:{5};font-family:{6}\">{9}</text>";
    }
}
