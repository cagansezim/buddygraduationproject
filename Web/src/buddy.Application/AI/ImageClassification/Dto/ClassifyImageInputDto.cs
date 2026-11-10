using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace buddy.AI.ImageClassification.Dto
{
    public class ClassifyImageInputDto
    {
        public IFormFile Image { get; set; }
    }
}
