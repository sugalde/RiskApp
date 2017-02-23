using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ContosoUniversity.Models
{
    public class InflightOrder
    {
        public int ID { get; set; }

        [Display(Name = "Log Number")]
        public int LogNumber
        {
            get; set;
        }

        [Display(Name = "Fleet Number")]
        public int FleetNumber
        {
            get; set;
        }

        [Display(Name = "Quotation Number")]
        public int QuotationNumber
        {
            get; set;
        }

        [Display(Name = "Unit Number")]
        public int UnitNumber
        {
            get; set;
        }

        [Display(Name = "Amount Order")]
        public decimal AmountOrder
        {
            get; set;
        }

    }
}