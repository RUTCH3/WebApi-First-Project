using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class JewelryController : ControllerBase
{
    private static List<Jewelry> list;
    static JewelryController()
    {
        list =
        [
            new() { Id = 1, Name = "Necklace" ,Weight=20.0},
            new() { Id = 2, Name = "Bracelet",Weight = 30.4 },
            new() { Id = 3, Name = "Ring",Weight = 50.0 },
            new() { Id = 4, Name = "Earrings",Weight = 30.4 },
            new() { Id = 5, Name = "Pendant",Weight = 30.4 },
            new() { Id = 6, Name = "Brooch",Weight = 30.4 },
            new() { Id = 7, Name = "Tiara", Weight = 12.4  },
            new() { Id = 8, Name = "Bangle", Weight = 24.0  },
            new() { Id = 9, Name = "Crown", Weight = 18.9  },
            new() { Id = 10, Name = "Locket", Weight = 20  }
        ];
    }

    [HttpGet]
    public IEnumerable<Jewelry> Get()
    {
        return list;
    }

    [HttpGet("{id}")]
    public ActionResult<Jewelry> Get(int id)
    {
        var jewelry = list.FirstOrDefault(p => p.Id == id);
        if (jewelry == null)
            return BadRequest("invalid id");
        return jewelry;
    }

    [HttpPost]
    public ActionResult Insert(Jewelry newJewelry)
    {
        var maxId = list.Max(p => p.Id);
        newJewelry.Id = maxId + 1;
        list.Add(newJewelry);
        return CreatedAtAction(nameof(Insert), new { id = newJewelry.Id }, newJewelry);
    }

    [HttpPut("{id}")]
    public ActionResult Update(int id, Jewelry newJewelry)
    {
        var oldJewelry = list.FirstOrDefault(p => p.Id == id);
        if (oldJewelry == null)
            return BadRequest("invalid id");
        if (newJewelry.Id != oldJewelry.Id)
            return BadRequest("id mismatch");
        oldJewelry.Name = newJewelry.Name;
        oldJewelry.Weight = newJewelry.Weight;
        return NoContent();
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        var dJewelry = list.FirstOrDefault(p => p.Id == id);
        if (dJewelry == null)
            return NotFound("invalid id");
        list.Remove(dJewelry);
        return NoContent();
    }
}
