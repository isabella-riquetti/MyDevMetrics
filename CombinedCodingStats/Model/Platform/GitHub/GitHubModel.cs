namespace CombinedCodingStats.Model.Platform
{
    public class GitHubModel : Platform, IGitHubModel
    {
        public override int ACTIVITY_LEVEL_1 => 1;
        public override int ACTIVITY_LEVEL_2 => 5;
        public override int ACTIVITY_LEVEL_3 => 10;
        public override int ACTIVITY_LEVEL_4 => 15;

        public override int SQUARE_SIZE => 11;
        public override int SQUARE_SPACING => 4;
        public override int SQUARE_DISTANCE => SQUARE_SIZE + SQUARE_SPACING;
        public override int SQUARE_ROUNDING => 2;
    }    
}
