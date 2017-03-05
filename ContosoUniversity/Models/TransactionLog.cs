using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ContosoUniversity.Models
{
    public class TransactionLog
    {
        public int ID { get; set; }

        [Display(Name = "Fleet Number")]
        public int FleetNumber
        {
            get; set;
        }

        [Display(Name = "Quotation ID")]
        public int QuotationID
        {
            get; set;
        }

        [Display(Name = "Credit Line Initial")]
        [DisplayFormat(DataFormatString = "{0:#,###0.00}")]
        public decimal CreditLineInitial
        {
            get; set;
        }

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

        [Display(Name = "Quotation Amount")]
        [DisplayFormat(DataFormatString = "{0:#,###0.00}", ApplyFormatInEditMode = true)]
        public decimal QuotationAmount
        {
            get; set;
        }

        [StringLength(50)]
        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }

        public DateTime Created { get; set; }

        [StringLength(10)]
        [Display(Name = "Request Status")]
        public string RequestStatus { get; set; }

    }
}