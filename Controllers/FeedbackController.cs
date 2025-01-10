using System.Text.Json;
using backend.Model;
using backend.Repositories;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace backend.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackRepository _feedbackRepository;
        public FeedbackController(IFeedbackRepository feedbackRepository)
        {
            _feedbackRepository = feedbackRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Feedback feedback)
        {
            if (string.IsNullOrWhiteSpace(feedback.TrackingNum))
            {
                return BadRequest(new
                {
                    message = "Tracking number is missing. Please provide a valid tracking number."
                });
            }
            
            if (string.IsNullOrWhiteSpace(feedback.Name))
            {
                return BadRequest(new
                {
                    message = "Name is missing. Please provide a valid name."
                });
            }
            
            if (string.IsNullOrWhiteSpace(feedback.Surname))
            {
                return BadRequest(new
                {
                    message = "Surname is missing. Please provide a valid surname."
                });
            }
            
            if (string.IsNullOrWhiteSpace(feedback.Email))
            {
                return BadRequest(new
                {
                    message = "Email is missing. Please provide a valid email."
                });
            }
            
            if (feedback.Rating < 1 || feedback.Rating > 5)
            {
                return BadRequest(new
                {
                    message = "Rating is invalid."
                });
            }

            Random rand = new Random();
            int provinceId = rand.Next(1, 81);

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync($"https://turkiyeapi.dev/api/v1/provinces/{provinceId}");
                var content = await response.Content.ReadAsStringAsync();

                using (JsonDocument document = JsonDocument.Parse(content))
                {
                    var status = document.RootElement.GetProperty("status").GetString();
                    var data = document.RootElement.GetProperty("data");
                    string provinceName = data.GetProperty("name").GetString();
        
                    var districts = data.GetProperty("districts").EnumerateArray().ToList();
        
                    int randomDistrictIndex = rand.Next(0, districts.Count);
        
                    string districtName = districts[randomDistrictIndex].GetProperty("name").GetString();

                    if (status == "OK")
                    {
                        feedback.Province = provinceName!;
                        feedback.District = districtName!;
                    }
                    else
                    {
                        feedback.Province = "Unknown";
                        feedback.District = "Unknown";
                    }
                }
            }

            var id = await _feedbackRepository.Create(feedback);

            return CreatedAtAction(nameof(Get), new { id = id.ToString() }, feedback);
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var rFeedback = await _feedbackRepository.Get(ObjectId.Parse(id));
            return new JsonResult(rFeedback);
        }

        [HttpGet("getbytracking/{trackingNum}")]
        public async Task<IActionResult> GetByName(string trackingNum)
        {
            var rFeedback = await _feedbackRepository.GetByTracking(trackingNum);
            return new JsonResult(rFeedback);
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var rFeedback = await _feedbackRepository.Delete(id);
            return new JsonResult(rFeedback);
        }

        [HttpGet("fetch")]
        public async Task<IActionResult> GetAll()
        {
            var feedbacks = await _feedbackRepository.GetAll();

            return new JsonResult(feedbacks);
        }
    }
}