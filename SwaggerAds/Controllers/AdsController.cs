using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SwaggerAds.Data;
using SwaggerAds.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;

namespace SwaggerAds.Controllers;

[ApiController]
[Route("/")]
[EnableCors("AllowAl")]

public class AdsController : ControllerBase
{
    private readonly ApplicationDbContext _dbContext;
    public AdsController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    // READ ALL ///////////////////////////////////////////////////////
    /// <summary>
    /// Retrieve ALL adverts from the database
    /// </summary>
    /// <returns>
    /// A full list of ALL adverts
    /// </returns>
    /// <remarks>
    /// Example end point: GET /api/Ads
    /// </remarks>
    [HttpGet(Name = "GetAds")]
    public async Task<ActionResult<List<Ads>>> GetAll()
    {
        return Ok(await _dbContext.AdContext.ToListAsync());
    }

    // GET ONE ///////////////////////////////////////////////////////

    /// <summary>
    /// Retrieve one advert
    /// </summary>
    /// <returns>
    /// One advert
    /// </returns>
    /// <remarks>
    /// Eaxmple end point: Get /api/Ads?id=4
    /// </remarks>
    [Authorize(Roles = "Admin, User")]
    [HttpGet("{id}", Name = "GetAd")]
    public async Task<ActionResult<Ads>> GetAd(int id)
    {
        var advert = await _dbContext.AdContext.FindAsync(id);

        if(advert == null){
            return BadRequest("Advert not found");
        }
        return Ok(advert);
    }

    // POST ONE ///////////////////////////////////////////////////////

    /// <summary>
    /// Create one advert
    /// </summary>
    /// <returns>
    /// Nada
    /// </returns>
    /// <remarks>
    /// Eaxmple end point: Post /api/Ads
    /// </remarks>
    [Authorize(Roles ="Admin, User")]
    [HttpPost(Name = "CreateAd")]
    public async Task<ActionResult<Ads>> CreateAd(Ads newAd)
    {
         _dbContext.AdContext.Add(newAd);
         _dbContext.SaveChanges();
         return Ok(newAd);

    }

    // DELETE ONE ///////////////////////////////////////////////////////

    /// <summary>
    /// Delete one advert
    /// </summary>
    /// <returns>
    /// Nada
    /// </returns>
    /// <remarks>
    /// Eaxmple end point: Get /api/Ads?id=4
    /// </remarks>
    /// 

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}",Name = "DeleteAd")]
    public async Task<ActionResult<Ads>> DeleteAd(int id)
    {
        var adToDelete = _dbContext.AdContext.First(x => x.Id == id);
        if(adToDelete == null)
        {
            return BadRequest("Advert not found");
        }
        _dbContext.AdContext.Remove(adToDelete);
        _dbContext.SaveChanges();
        return Ok("Deleted "+adToDelete);   
    }
    [Authorize(Roles = "Admin")]
    [HttpPut("{id}",Name = "Update")]
    public async Task<IActionResult> UpateAdvert(int id, Ads advert)
    {
        var advertToUpdate = _dbContext.AdContext.First(x => x.Id == id);
        if(advertToUpdate == null)
        {
            return BadRequest("Advert not found");
        }
        advertToUpdate.Description = advert.Description;
        advertToUpdate.ImageUrl = advert.ImageUrl;
        advertToUpdate.Price = advert.Price;
        advertToUpdate.SellDAte = advert.SellDAte;
        advertToUpdate.Title = advert.Title;
        await _dbContext.SaveChangesAsync();
        return Ok(advertToUpdate);
    }
    [Authorize(Roles = "Admin")]
    [HttpPatch]
    [Route("id")]
    public async Task<ActionResult<Ads>> PatchAdd(Microsoft.AspNetCore.JsonPatch.JsonPatchDocument advert, int id)
    {
        var advertToUpdate = await _dbContext.AdContext.FindAsync(id);

        if(advertToUpdate == null)
        {
            return BadRequest("Advert not found"); 
        }

        advert.ApplyTo(advertToUpdate);
        await _dbContext.SaveChangesAsync();

        return Ok(await _dbContext.AdContext.ToListAsync());   
    }
} 