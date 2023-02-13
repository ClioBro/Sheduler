namespace ProjectShedule.Core.Swipe
{
    public class SwipeHorizontalContentPercentAchivments : SwipeHorizontalContent
    {
        public SwipeHorizontalContentPercentAchivments(double threshold = 200)
        {
            Threshold = threshold;
            RightDisclosurePercenetAchivement = new FloatingSwipePercentAchievement(threshold);
            LeftDisclosurePercenetAchivement = new FloatingSwipePercentAchievement(threshold);
        }

        public FloatingSwipePercentAchievement RightDisclosurePercenetAchivement { get; }
        public FloatingSwipePercentAchievement LeftDisclosurePercenetAchivement { get; }

    }
}
