using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant_Model.Request
{
    public class TableUpsertRequest
    {
        public int TableNumber { get; set; }
        public int Capacity { get; set; }
        public bool isOccupied { get; set; }

    }
}
