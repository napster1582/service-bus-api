using Ibang.Platform.Common.Infrastructure.Contracts.MongoDb;
using Ibang.Platform.Common.Infrastructure.Contracts.Sql;
using Microsoft.Bot.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Service.Bus.Domain.Models
{
    public class TransferModel
    {
        public string Origin { get; set; } = "";
        public Activity Activity { get; set; }
        public ChannelTypesContract ChannelTypes { get; set; }
        public ChannelsContract Channels { get; set; }
        public PagesContract Pages { get; set; }
        public IAContract IAContract { get; set; }
        public UsersContract User { get; set; }
        public List<AttributeUserContract> AttributesUser { get; set; }
        //public BlocksContract Block { get; set; }
        public List<BlocksContract> Blocks { get; set; }
        public LiveChatsContract LiveChat { get; set; }
        public LiveChatTypeContract LiveChatType { get; set; }
    }
}
