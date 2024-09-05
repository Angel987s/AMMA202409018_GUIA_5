using AMMA202409018.DTOs.ProductsDTOs;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace AMMA202409018.AppWebMVC.Controllers
{
    public class ProductAMMAController : Controller
    {
        private readonly HttpClient _httpClientAPI;

        public ProductAMMAController(IHttpClientFactory httpClientFactory)
        {
            _httpClientAPI = httpClientFactory.CreateClient("API");
        }

        public async Task<IActionResult> Index(SearchQueryProductAMMADTO searchQueryDTO, int countRow = 0)
        {
            if (searchQueryDTO.SendRowCount == 0)
                searchQueryDTO.SendRowCount = 2;

            if (searchQueryDTO.Take == 0)
                searchQueryDTO.Take = 10;

            var result = new SearchResultProductAMMADTO();

            var response = await _httpClientAPI.PostAsJsonAsync("/product/search", searchQueryDTO);

            if (response.IsSuccessStatusCode)
                result = await response.Content.ReadFromJsonAsync<SearchResultProductAMMADTO>();

            result = result ?? new SearchResultProductAMMADTO();

            if (result.CountRow == 0 && searchQueryDTO.SendRowCount == 1)
                result.CountRow = countRow;

            ViewBag.CountRow = result.CountRow;
            searchQueryDTO.SendRowCount = 0;
            ViewBag.SearchQuery = searchQueryDTO;

            return View(result);
        }

        public async Task<IActionResult> Details(int id)
        {
            if (id <= 0) return BadRequest("ID inválido");

            var result = new GetIdResultProductAMMADTO();

            try
            {
                var response = await _httpClientAPI.GetAsync($"/product/{id}");

                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadFromJsonAsync<GetIdResultProductAMMADTO>();
                }

                return View(result ?? new GetIdResultProductAMMADTO());
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Error al obtener los detalles: {ex.Message}";
                return View(new GetIdResultProductAMMADTO());
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateProductAMMADTO createProductDTO)
        {
            if (!ModelState.IsValid)
            {
                return View(createProductDTO);
            }

            try
            {
                var response = await _httpClientAPI.PostAsJsonAsync("/product", createProductDTO);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }

                ViewBag.Error = "Error al intentar guardar el registro";
                return View(createProductDTO);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(createProductDTO);
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (id <= 0) return BadRequest("ID inválido");

            var result = new GetIdResultProductAMMADTO();

            try
            {
                var response = await _httpClientAPI.GetAsync($"/product/{id}");

                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadFromJsonAsync<GetIdResultProductAMMADTO>();
                }

                return View(new EditProductAMMADTO(result ?? new GetIdResultProductAMMADTO()));
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Error al obtener los detalles para edición: {ex.Message}";
                return View(new EditProductAMMADTO());
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditProductAMMADTO editProductDTO)
        {
            if (id <= 0) return BadRequest("ID inválido");

            try
            {
                var response = await _httpClientAPI.PutAsJsonAsync("/product", editProductDTO);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }

                ViewBag.Error = "Error al intentar editar el registro";
                return View(editProductDTO);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(editProductDTO);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return BadRequest("ID inválido");

            var result = new GetIdResultProductAMMADTO();

            try
            {
                var response = await _httpClientAPI.GetAsync($"/product/{id}");

                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadFromJsonAsync<GetIdResultProductAMMADTO>();
                }

                return View(result ?? new GetIdResultProductAMMADTO());
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Error al obtener el registro para eliminar: {ex.Message}";
                return View(new GetIdResultProductAMMADTO());
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, GetIdResultProductAMMADTO productDTO)
        {
            if (id <= 0) return BadRequest("ID inválido");

            try
            {
                var response = await _httpClientAPI.DeleteAsync($"/product/{id}");

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }

                ViewBag.Error = "Error al intentar eliminar el registro";
                return View(productDTO);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(productDTO);
            }
        }
    }
}
