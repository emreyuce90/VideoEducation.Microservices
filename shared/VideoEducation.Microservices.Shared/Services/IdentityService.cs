using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoEducation.Microservices.Shared.Services {
    public class IdentityService : IIdentityService {
        public Guid UserId => Guid.Parse("c3bac58e-b091-4e46-ae7c-2957e41920a0");

        public string UserName => "Emre Yüce";
    }
}
