using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportEventManagementSystem.Models.EventViewModels
{
    public class SearchViewModel
    {
        public string param { get; set; }

        public List<Event> results { get; set; }

    }
}
