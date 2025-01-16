using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChovEvidWpf
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net.Http;
    using System.Text.Json;
    using System.Threading.Tasks;
    using ChovEvidApi.Dto;

    public class ApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public ApiClient(string baseUrl)
        {
            _httpClient = new HttpClient { BaseAddress = new Uri(baseUrl) };
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<List<BreedingStationDto>> GetAllBreedingStations()
        {
            var response = await _httpClient.GetAsync("breedingstation/getAll");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<BreedingStationDto>>(json, _jsonOptions)
                   ?? throw new Exception("Získanie informácií o osobách neúspešné.");
        }


        //public async Task<List<ThemeDtoCsv>> GetThemesAsync(int? year = null, int? stProgramId = null)
        //{
        //    var query = $"api/theme/themesCsv?year={year}&stProgramId={stProgramId}";
        //    var response = await _httpClient.GetAsync(query);
        //    response.EnsureSuccessStatusCode();
        //    var json = await response.Content.ReadAsStringAsync();
        //    return JsonSerializer.Deserialize<List<ThemeDtoCsv>>(json, _jsonOptions);
        //}

        //public async Task<Stream> GetThemesDocx(int id)
        //{
        //    var query = $"api/theme/theme2docx/{id}";
        //    var response = await _httpClient.GetAsync(query);
        //    response.EnsureSuccessStatusCode();
        //    return await response.Content.ReadAsStreamAsync();
        //}
        //public async Task<Stream> GetThemesCsv(int? year = null, int? stProgramId = null)
        //{
        //    var query = $"api/theme/themes2csv?year={year}&stProgramId={stProgramId}";
        //    var response = await _httpClient.GetAsync(query);
        //    response.EnsureSuccessStatusCode();

        //    return await response.Content.ReadAsStreamAsync();
        //}

        //public async Task<List<int>> GetThemeYearsAsync()
        //{
        //    var response = await _httpClient.GetAsync("api/theme/themesyears");
        //    response.EnsureSuccessStatusCode();
        //    var json = await response.Content.ReadAsStringAsync();
        //    return JsonSerializer.Deserialize<List<int>>(json, _jsonOptions);
        //}
    }

}
