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
        list = new List<Jewelry> 
        {
            new Jewelry { Id = 1, Name = "Neckless" },
            new Jewelry { Id = 2, Name = "bracelet",Weight = 30.4 },
            new Jewelry { Id = 3, Name = "Ring",Weight = 50.0 },
            new Jewelry { Id = 4, Name = "bracelet",Weight = 30.4 },
            new Jewelry { Id = 5, Name = "bracelet",Weight = 30.4 },
            new Jewelry { Id = 6, Name = "bracelet",Weight = 30.4 },
        };
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
    public ActionResult Delete(int id){
        var dJewelry = list.FirstOrDefault(p => p.Id == id);
        if (dJewelry == null) 
            return NotFound("invalid id");
        list.Remove(dJewelry);
        return NoContent();
    }
}
