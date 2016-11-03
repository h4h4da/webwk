using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Domain.Concrete
{
    public class ShippingAddress
    {
        [Required(ErrorMessage = "请输入姓名")]
        [Display(Name = "收货人姓名")]
        public string Name { get; set; }

        [Required(ErrorMessage = "请输入省份")]
        [Display(Name = "收货人姓名")]
        public string Province { get; set; }

      


        [Required(ErrorMessage = "请输入城市")]
        [Display(Name = "城市")]
        public string City { get; set; }

        [Required(ErrorMessage = "请输入行政区")]
        [Display(Name = "区")]
        public string District { get; set; }

        [Required(ErrorMessage = "请输入详细地址")]
        [Display(Name = "详细地址")]
        public string DetailAddress { get; set; }

        [Required(ErrorMessage = "请输入邮政编码")]
        [Display(Name = "邮政编码")]
        public String Zip { get; set; }

        [Required(ErrorMessage = "请输入电话号码")]
        [Display(Name = "电话号码")]
        public string Phone { get; set; }

        [Display(Name = "礼品包装")]
        public bool GiftWrap { get; set; }

    }
}
