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

        public async Task<List<DogDto>> GetAllDogs()
        {
            var response = await _httpClient.GetAsync("dog/getAll");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<DogDto>>(json, _jsonOptions)
                   ?? throw new Exception("Získanie informácií o psoch neúspešné.");
        }

        public async Task<Stream> RemoveDogById(int id)
        {
            var query = $"dog/remove/{id}";
            var request = new HttpRequestMessage(HttpMethod.Delete, query);
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStreamAsync();
        }

        public async Task<Stream> ExportBreedingStationsToDocx()
        {
            var response = await _httpClient.GetAsync("BreedingStation/exportToDocx");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStreamAsync();
        }
    }

}
