using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelsRepository;
using ModelsRepository.Models;

namespace Kaizen_Task.Controllers
{
    [Route("api/warehouse")]
    [ApiController]
    public class WarehouseController : CustomBaseController
    {
        public WarehouseController(IConfiguration configuration, IManagerUnit managerUnit) : base(configuration, managerUnit) { }

        [HttpPost]
        [Route("Add")]
         //[Authorize]
        public IActionResult AddWarehouse([FromBody] Warehouse warehouse)
        {
            try
            {
                if (warehouse.Id != 0)
                {
                    _manager.Warehouses.Update(warehouse);
                }
                else
                {
                    _manager.Warehouses.Add(warehouse);
                }
                _manager.Complete();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("item/add")]
         //[Authorize]
        public IActionResult AddItem([FromBody] Item item)
        {
            try
            {
                if (item.Id != 0)
                {
                    _manager.Items.Update(item);
                }
                else
                {
                    _manager.Items.Add(item);
                }
                _manager.Complete();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("GetWarehouses")]
         //[Authorize]
        public IActionResult GetWarehouses([FromQuery] int page, [FromQuery] int pageSize)
        {
            try
            {
                int count = 0;
                List<Warehouse> warehouses = _manager.Warehouses.GetPaged(page, pageSize, out count).ToList();
                return Ok(new { warehouses, count });
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        [HttpGet]
        [Route("GetWarehouse")]
         //[Authorize]
        public IActionResult GetWarehouse([FromQuery] Int64 warehouseId)
        {
            try
            {
                Warehouse warehouse = _manager.Warehouses.GetEntity((warehouse) => warehouse.Id == warehouseId);
                return Ok(new { warehouse });
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("GetItems")]
         //[Authorize]
        public IActionResult GetItems([FromQuery] int page, [FromQuery] int pageSize, [FromQuery] int wherhouseId)
        {
            try
            {
                int count = 0;
                List<Item> items = _manager.Items.GetPaged(page, pageSize, out count, (item) => item.Warehouse.Id == wherhouseId).ToList();
                return Ok(new { items, count });
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("GetItem")]
         //[Authorize]
        public IActionResult GetItem([FromQuery] Int64 itemId)
        {
            try
            {
                Item item = _manager.Items.GetEntity((item) => item.Id == itemId);
                return Ok(new { item });
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("Delete")]
         //[Authorize]
        public IActionResult Delete([FromForm] Int64 warehouseId)
        {
            try
            {
                _manager.Warehouses.DeleteEntity((x) => x.Id == warehouseId);
                _manager.Complete();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        [HttpPost]
        [Route("Item/Delete")]
         //[Authorize]
        public IActionResult DeleteItem([FromForm] Int64 itemId)
        {
            try
            {
                _manager.Items.DeleteEntity((x) => x.Id == itemId);
                _manager.Complete();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
