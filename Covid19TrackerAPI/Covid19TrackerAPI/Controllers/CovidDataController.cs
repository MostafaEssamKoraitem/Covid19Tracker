using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Threading.Tasks;

namespace Covid19TrackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CovidDataController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<CovidDataController> _logger;

        public CovidDataController(HttpClient httpClient, ILogger<CovidDataController> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        [HttpGet("national/daily")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Tags("COVID-19 Data", "National Data")]
        public async Task<IActionResult> GetNationalDailyData()
        {
            try
            {
                var apiUrl = "https://api.covidtracking.com/v2/us/daily.json";
                var response = await _httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    return Ok(data);
                }
                else
                {
                    _logger.LogError($"Error fetching national daily data: {response.StatusCode} - {response.ReasonPhrase}");
                    return StatusCode((int)response.StatusCode, response.ReasonPhrase);
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"HttpRequestException in GetNationalDailyData: {ex.Message}");
                return StatusCode(500, "An error occurred while fetching national daily data.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception in GetNationalDailyData: {ex.Message}");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpGet("national/daily/{date}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Tags("COVID-19 Data", "Single Day Data")]
        public async Task<IActionResult> GetSingleDayData(string date)
        {
            try
            {
                var apiUrl = $"https://api.covidtracking.com/v2/us/daily/{date}.json"; //2021-03-07 last day
                var response = await _httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    return Ok(data);
                }
                else
                {
                    _logger.LogError($"Error fetching single day data for {date}: {response.StatusCode} - {response.ReasonPhrase}");
                    return StatusCode((int)response.StatusCode, response.ReasonPhrase);
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"HttpRequestException in GetSingleDayData for {date}: {ex.Message}");
                return StatusCode(500, "An error occurred while fetching single day data.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception in GetSingleDayData for {date}: {ex.Message}");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpGet("states")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Tags("COVID-19 Data", "State Data")]
        public async Task<IActionResult> GetStateData()
        {
            try
            {
                var apiUrl = "https://api.covidtracking.com/v2/states.json";
                var response = await _httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    return Ok(data);
                }
                else
                {
                    _logger.LogError($"Error fetching state data: {response.StatusCode} - {response.ReasonPhrase}");
                    return StatusCode((int)response.StatusCode, response.ReasonPhrase);
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"HttpRequestException in GetStateData: {ex.Message}");
                return StatusCode(500, "An error occurred while fetching state data.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception in GetStateData: {ex.Message}");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpGet("states/{stateCode}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Tags("COVID-19 Data", "State Data by Code")]
        public async Task<IActionResult> GetStateDataByCode(string stateCode)
        {
            try
            {
                var apiUrl = $"https://api.covidtracking.com/v2/states/{stateCode}.json";
                var response = await _httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    return Ok(data);
                }
                else
                {
                    _logger.LogError($"Error fetching data for state {stateCode}: {response.StatusCode} - {response.ReasonPhrase}");
                    return StatusCode((int)response.StatusCode, response.ReasonPhrase);
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"HttpRequestException in GetStateDataByCode for {stateCode}: {ex.Message}");
                return StatusCode(500, "An error occurred while fetching data for the state.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception in GetStateDataByCode for {stateCode}: {ex.Message}");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }
    }
}
