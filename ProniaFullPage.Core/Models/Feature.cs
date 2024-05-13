using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaFullPage.Core.Models
{
    public class Feature: BaseEntity
    {
        [Required]
        public string Icon { get; set; } = null!;


        [StringLength(20)]
        public string Title { get; set; } = null!;

        [StringLength(100)]
        public string Description { get; set; } = null!;

    }
}
