namespace Restaurants.Infrastructure.Authorization
{
    public static class PolicyName
    {
        public const string HasNationality = "HasNationality";
        public const string AtLeast21 = "AtLeast21";
    }

    public static class ApplicationClaimTypes
    {
        public const string Nationality = "Nationality";
        public const string DateOfBirth = "DateOfBirth";
    }
}
