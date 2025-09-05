using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Int
{
    public interface ICore
    {
        string Id { get; set; }

        int Version { get; set; }

        DateTime AddDate { get; set; }

        string AddUser { get; set; }

        DateTime? UpdDate { get; set; }

        string UpdUser { get; set; }
    }
}
