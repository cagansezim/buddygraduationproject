using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using buddy.AI.Analysis.Dto;

namespace buddy.AI.Analysis
{
    public interface IAnalysisAppService
    {
        Task<string> AnalyzeAsync(AnalysisInput input);
    }
}
