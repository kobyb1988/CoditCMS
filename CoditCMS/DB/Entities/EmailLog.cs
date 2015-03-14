using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Entities
{
    class EmailLog
    {
        
        public global::System.Int32 Id { get; set; }
        
        public global::System.DateTime Date { get; set; }
        
        public global::System.Boolean Status { get; set; }
        
        public global::System.String From { get; set; }
        
        public global::System.String To { get; set; }
        
        public global::System.String Subject { get; set; }
        
        public global::System.String Content { get; set; }
        
        public global::System.String Type { get; set; }
        
        public global::System.String Message { get; set;}
        
        public global::System.String StackTrace { get; set; }
        
    }
}
