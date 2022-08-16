namespace ProjectManagement.Infrastructure.Validations
{
    public static class Validations
    {
        public static bool Validate<T>(IEnumerable<T> collection)
        {
            if (collection.Any())
            {
                return true;
            }
            return false;
        }
    }
}
