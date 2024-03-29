﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeePay.Core.Application.DTOs
{
    public class FeeTypeViewModel : BaseViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(49)]
        [DisplayName("Name")]
        public string Name { get; set; }

        [Required]
        [StringLength(49)]
        [DisplayName("Code")]
        public string Code { get; set; }

        [StringLength(349)]
        [DisplayName("Description")]
        public string Description { get; set; }

    }
}
