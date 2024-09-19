using System;

namespace PuthagaUlagam.Common
{
    public static class InputValidation
    {
        public static bool IsValidDateOfPublication(DateTime DateOfPublicattion)
        {
            return DateOfPublicattion < DateTime.Now;
        }

        public static bool IsValidISBN(string SBN)
        {
            if (!int.TryParse(SBN, out _))
            {
                return false;
            }

            return true;
        }

        public static bool IsValidPrice(string Price)
        {
            if (!int.TryParse(Price, out _))
            {
                return false;
            }

            return true;
        }

        public static bool IsValidCount(string Count)
        {
            if (!int.TryParse(Count, out _))
            {
                return false;
            }

            return true;
        }
    }
}