using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportEventManagementSystem.Models.EventViewModels
{
    public class EventIndexViewModel
    {
        public List<Event> CreatedEvents { get; set; }
        public List<Event> ParticipatingEvents { get; set; }
    }
}
