using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyApp
{
    public class Message : EntityBase
    {
        public Message() : base() 
        {
            this.Time = DateTime.Now;
        }

        [Key]
        public int MessageId { get; set; }

        public string Content { get; set; }
        public DateTime Time { get; set; }

        public int FamilyId { get; set; }
        public Family Family { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
        
    }
}
