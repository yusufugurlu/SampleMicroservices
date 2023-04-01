using System;
using System.Collections.Generic;
using System.Text;

namespace SampleMicroservices.Shared.Services
{
    public interface ISharedIdentityService
    {
        public string GetUserId { get; }
    }
}
