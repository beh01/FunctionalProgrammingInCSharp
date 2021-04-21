using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examples
{
    public struct ValidityDateRange
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public bool IsInEffect(DateTime date)
        {
            return Start.CompareTo(date) <= 0 && End.CompareTo(date) >= 0;
        }
        public void Extend(int days)
        {
            End = End.AddDays(days);
        }
    }

    public class Card
    {
        public string SerialNumber { get; set; }
        public ValidityDateRange Validity { get; set; }
    }

    public class RangeClient
    {
        public static void Test()
        {
            var valid = new ValidityDateRange()
            {
                Start = DateTime.Parse("2021-03-19"),
                End = DateTime.Parse("2021-03-22")
            };
            var card1 = new Card()
            {
                SerialNumber = "123456",
                Validity = valid
            };

            var card2 = new Card()
            {
                SerialNumber = "654321",
                Validity = valid
            };
            card2.Validity.Extend(7);

            Console.WriteLine($"Card {card1.SerialNumber}, validity: {card1.Validity.IsInEffect(DateTime.Parse("2021-03-24"))}");
            Console.WriteLine($"Card {card2.SerialNumber}, validity: {card2.Validity.IsInEffect(DateTime.Parse("2021-03-24"))}");
        }
    }
}