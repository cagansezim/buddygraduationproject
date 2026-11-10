using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;
using buddy.AI.ImageClassification;
using Abp.WebApi.Controllers;
using buddy.Controllers;
using System.Collections.Generic;
using Abp.AspNetCore.Mvc.Authorization;
using buddy.Authorization;
using buddy.AI.ImageClassification.Dto;
using System.Linq;

namespace buddy.Web.Controllers
{
    [AbpMvcAuthorize(PermissionNames.ImageClassification)]
    public class ImageClassificationController : buddyControllerBase
    {
        private readonly IImageClassificationAppService _imageClassificationAppService;
        private readonly string _imageDirectory = @"C:\Users\cagan\Desktop"; // Your pet images directory

        public ImageClassificationController(IImageClassificationAppService imageClassificationAppService)
        {
            _imageClassificationAppService = imageClassificationAppService;
        }

        // Index action to display the initial page
        public IActionResult Index()
        {
            return View();
        }

        // Method to classify images from the directory and render a view
        public async Task<IActionResult> ClassifyImagesInDirectory()
        {
            if (!Directory.Exists(_imageDirectory))
            {
                return View("Error", "Image directory does not exist.");
            }

            var imageFiles = Directory.GetFiles(_imageDirectory, "*.jpg"); // Only JPG images
            var results = new List<ImageClassificationResultDto>();

            foreach (var imagePath in imageFiles)
            {
                // Read the image bytes using System.IO.File
                var imageBytes = await System.IO.File.ReadAllBytesAsync(imagePath);

                // Classify the image
                var classification = await _imageClassificationAppService.ClassifyImageAsync(new ClassifyImageInput
                {
                    ImageBytes = imageBytes
                });

                // Add result with image bytes and classification
                results.Add(new ImageClassificationResultDto
                {
                    ImageBytes = imageBytes,
                    Classification = classification
                });
            }

            // Pass the results directly to the view
            return View("Results", results);
        }

        // Action to handle single image upload and classification
        [HttpPost]
        public async Task<IActionResult> UploadAndClassify(ClassifyImageInputDto input)
        {
            if (input.Image == null || input.Image.Length == 0)
            {
                return View("Error", "Please upload a valid image.");
            }

            using (var memoryStream = new MemoryStream())
            {
                await input.Image.CopyToAsync(memoryStream);
                var imageBytes = memoryStream.ToArray();

                var classification = await _imageClassificationAppService.ClassifyImageAsync(new ClassifyImageInput
                {
                    ImageBytes = imageBytes
                });

                var result = new ImageClassificationResultDto
                {
                    ImageBytes = imageBytes,
                    Classification = classification
                };

                // Pass the result directly to the view
                return View("SingleResult", result);
            }
        }
    }
}