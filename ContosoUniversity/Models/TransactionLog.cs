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
        public decimal CreditLineInitial
        {
            get; set;
        }

        [Display(Name = "Outstanding Balance")]
        public decimal OutstandingBalance
        {
            get; set;
        }

        [Display(Name = "Work in Progress")]
        public decimal WorkProgress
        {
            get; set;
        }

        [Display(Name = "In Flight")]
        public decimal InFlight
        {
            get; set;
        }

        [Display(Name = "Sum")]
        public decimal Sum
        {
            get; set;
        }

        [Display(Name = "Quotation Amount")]
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