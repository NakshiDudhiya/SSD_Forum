using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SSD_Forum.Models
{
    public class Reply
    {
        [Key]
        public int ReplyId { get; set; }

        [Required]
        [Display(Name ="Content")]
        public string Content { get; set; }

        [Required]
        [Display(Name = "Replied")]
        public string UserWhoReplied { get; set; }

        [Required]
        [Display(Name = "Date Replied")]
        [DataType(DataType.Date)]
        public DateTime DateReplied { get; set; }

        public Thread Thread { get; set; }
    }
}
