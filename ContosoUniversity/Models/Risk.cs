using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ContosoUniversity.Models
{
    public class Risk
    {
        public int ID { get; set; }

        [StringLength(50)]
        [Display(Name = "Economic Group")]
        public string EconomicGroup
        {
            get; set;
        }

        [StringLength(50)]
        [Display(Name = "Parent Name")]
        public string ParentName
        {
            get; set;
        }

        [Display(Name = "Fleet Number")]
        public int FleetNumber
        {
            get; set;
        }

        [Display(Name = "Credit Line")]
        [DisplayFormat(DataFormatString = "{0:#,###0.00}")]
        public decimal CreditLine
        {
            get; set;
        }

        [StringLength(3)]
        [Display(Name = "Currency")]
        public string Currency
        {
            get; set;
        }

        [Display(Name = "Exchange Rate")]
        public decimal ExchangeRate
        {
            get; set;
        }

        [StringLength(50)]
        [Display(Name = "Obligor")]
        public string Obligor
        {
            get; set;
        }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Expiration Date")]
        public DateTime ExpirationDate { get; set; }

        [Display(Name = "Outstanding Balance")]
        [DisplayFormat(DataFormatString = "{0:#,###0.00}")]
        public decimal OutstandingBalance
        {
            get; set;
        }

        [Display(Name = "Work in Progress")]
        [DisplayFormat(DataFormatString = "{0:#,###0.00}")]
        public decimal WorkProgress
        {
            get; set;
        }

        [Display(Name = "In Flight")]
        [DisplayFormat(DataFormatString = "{0:#,###0.00}")]
        public decimal InFlight
        {
            get; set;
        }

        [Display(Name = "Sum")]
        [DisplayFormat(DataFormatString = "{0:#,###0.00}")]
        public decimal Sum
        {
            get; set;
        }

        [StringLength(50)]
        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }
        public DateTime Created { get; set; }

    }
}
