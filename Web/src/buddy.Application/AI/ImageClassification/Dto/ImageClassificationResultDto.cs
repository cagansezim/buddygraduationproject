using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace buddy.AI.ImageClassification.Dto
{
        public class ImageClassificationResultDto
        {
            public byte[] ImageBytes { get; set; } // Keep as byte[] per your instruction
            public string Classification { get; set; }
        }
    }
