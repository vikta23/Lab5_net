using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentApp
{
    public interface ICanParticipateInExchange
    {
        string GetHomeCountry();
        DateTime GetExchangeEndDate();
        bool IsCurrentlyAbroad();
    }
}
