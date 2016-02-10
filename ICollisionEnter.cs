using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MummyDispair
{
    interface ICollisionEnter
    {
        void OnCollisionEnter(Collider other);
    }
}
