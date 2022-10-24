using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Service.Bus.Domain.Models
{
    public class QueueMessageModel
    {
        public int ChannelTypesId { get; set; }
        public int PageId { get; set; }
        public int UserId { get; set; }
        public string BlockIdInactivity { get; set; }
        public short InactivityGapInMinutes { get; set; }

    }

}
