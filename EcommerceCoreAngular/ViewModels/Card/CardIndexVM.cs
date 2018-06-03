using EcommerceCoreAngular.Paypal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceCoreAngular.ViewModels.Card
{
    public class CardIndexVM
    {
        public CardIndexVM()
        {
            CardProductVMList = new List<CardProductVM>();
            PayOrder = new PaypalOrder();
        }

        public List<CardProductVM> CardProductVMList { get; set; }
        public PaypalOrder PayOrder { get; set; }

        public decimal CardTotalPrice { get; set; }
    }
}
