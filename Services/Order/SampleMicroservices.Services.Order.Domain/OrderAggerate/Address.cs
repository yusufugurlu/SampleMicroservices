using SampleMicroservices.Services.Order.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleMicroservices.Services.Order.Domain.OrderAggerate
{
    public class Address:ValueObject
    {
        public Address(string province, string distrct, string street, string zipCode, string line)
        {
            Province = province;
            Distrct = distrct;
            Street = street;
            ZipCode = zipCode;
            Line = line;
        }

        public string Province { get;private set; }
        public string Distrct { get; private set; }
        public string Street { get;  private set; }
        public string ZipCode { get; private set; }
        public string Line { get; private set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Province; 
            yield return Distrct; 
            yield return Street; 
            yield return ZipCode;
            yield return Line; 
        }
    }
}
