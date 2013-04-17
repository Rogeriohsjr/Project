using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SFP
{
    public class AccountPayable
    {
        private DateTime? _datePayment;

        public Int32 Id { get; set; }
        public String Description { get; set; }
        public DateTime MaturityDate { get; set; }
        public Decimal TotalPrice { get; set; }
        public DateTime? DatePayment
        {
            get { return _datePayment; }
            set {
                if(("01/01/1900 00:00:00".Equals(value.ToString())) || (String.IsNullOrWhiteSpace(value.ToString())))
                    this._datePayment = null;
                else

                    this._datePayment = value;
            }
        }
        public Decimal PricePayment { get; set; }
        public String Document { get; set; }
        public String Historic { get; set; }
        public Category Category { get; set; }
        public User User { get; set; }

        public String CategoryDescription
        {
            get
            {
                if (Category == null)
                    return "";
                else
                    return Category.Description;
            }
        }
        public String DatePaymentFormated
        {
            get
            {
                if (_datePayment == null)
                    return "";
                else
                    return _datePayment.Value.ToShortDateString();

            }
        }


    }
}