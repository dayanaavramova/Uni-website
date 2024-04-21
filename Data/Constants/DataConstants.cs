namespace University.Data.Constants
{
    public class DataConstants
    {
        public const int NameMaxLenght = 100;
        public const int NameMinLenght = 3;

        public const int StudentYearMaxValue = 4;
        public const int StudentYearMinValue = 1;

        public const double ProfRatingMaxValue = 5.0;
        public const double ProfRatingMinValue = 0;

        public const int LectureDurationMaxValue = 4;
        public const int LectureDurationMinValue = 1;

        public const int LectureDetailsMaxLenght = 500;
        public const int LectureDetailsMinLenght = 10;

        public const int EventDescriptionMaxLenght = 500;
        public const int EventDescriptionMinLenght = 10;

        public const string DateFormat = "dd/MM/yyyy HH:mm";

        public const string RequiredErrorMessage = "The field {0} is required.";
        public const string StringLenghtErrorMessage = "The field {0} must be between {1} and {2} characters.";
    }
}
