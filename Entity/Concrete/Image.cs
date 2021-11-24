﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTrackingSystem.Entities.Concrete
{
    public class Image
    {
        [Key]
        public int ImageId { get; set; }

        public string ImageName { get; set; }
        public string ImagePath { get; set; }
    }
}
