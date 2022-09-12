using DataAccessRepository.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModelsRepository;
using ModelsRepository.Models;

namespace Kaizen_Task.Controllers
{
    [Route("api/admin")]
    [ApiController]
    public class AdminController : CustomBaseController
    {
        private readonly IHostEnvironment _env;
        private DataContext _context;

        public AdminController(DataContext context, IHostEnvironment env, IConfiguration configuration, IManagerUnit managerUnit) : base(configuration, managerUnit)
        {
            _env = env;
            _context = context;
        }

        [HttpGet]
        [Route("Warehouses/Get")]
        public IActionResult GetWarehouses([FromQuery] int page, [FromQuery] int pageSize)
        {
            try
            {
                int count = 0;
                List<WarehousStatistics> warehouses = _context.Warehouse.Select(x => new WarehousStatistics() { Id = x.Id, warehouse = x.Name, count = x.Items.Count() }).ToList();
                return Ok(new { warehouses });
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("Items/Get")]
        public IActionResult GetItems([FromQuery] int page, [FromQuery] int pageSize, [FromQuery] int Asc)
        {
            try
            {
                int count = 0;
                List<Item> Items;
                if (Asc > 0)
                {
                    Items = _manager.Items.GetOrderdPaged(page, pageSize, out count, x => x.Qty).ToList();
                }
                else
                {
                    Items = _manager.Items.GetOrderdDesPaged(page, pageSize, out count, x => x.Qty).ToList();
                }
                return Ok(new { Items, count });
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }


        [HttpGet]
        [Route("ReadLogs")]
        [Authorize(Roles = "Admin")]
        public IActionResult ReadLogs()
        {
            try
            {

                var items = new List<string>();
                string currentLine = "";
                var path = Path.Combine(_env.ContentRootPath, "DBFiles\\Log_file_2022 - Copy.log");

                using (FileStream fileStream = new FileStream(path, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        while (!reader.EndOfStream)
                        {
                            string line = reader.ReadLine();
                            currentLine = currentLine + line;
                            if (currentLine.EndsWith("]") && !currentLine.EndsWith("INF]"))
                            {
                                items.Add(currentLine.Replace("][", "] \n ["));
                                currentLine = "";
                            }
                        }
                    }
                }

                return Ok(new { items });
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
