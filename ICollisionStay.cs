using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MummyDispair
{
    interface ICollisionStay
    {
        void OnCollisionStay(Collider other);
    }
}
