using System.Threading.Tasks;
using buddy.AI.ImageClassification.Dto;

namespace buddy.AI.ImageClassification
{
    public interface IImageClassificationAppService
    {
        Task<string> ClassifyImageAsync(ClassifyImageInput input);
    }
}
