using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using Domain;

namespace BussinesLayer
{
    public class ClientBL
    {
        public List<ClientDOM> GetClient()                          
        {
            DataClasses1DataContext dc = new DataClasses1DataContext();
            return Mapper.convertToList(dc.Clients.ToList<Client>());
        }
    }
}
